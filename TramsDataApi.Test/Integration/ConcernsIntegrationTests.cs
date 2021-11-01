using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoFixture;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TramsDataApi.DatabaseModels;
using TramsDataApi.Factories;
using TramsDataApi.RequestModels;
using TramsDataApi.ResponseModels;
using TramsDataApi.Test.Utils;
using Xunit;

namespace TramsDataApi.Test.Integration
{
    [Collection("Database")]
    public class ConcernsIntegrationTests : IClassFixture<TramsDataApiFactory>, IDisposable
    {
        
        private readonly HttpClient _client;
        private readonly TramsDbContext _dbContext;
        private readonly Fixture _fixture;
        private readonly RandomGenerator _randomGenerator;

        public ConcernsIntegrationTests(TramsDataApiFactory fixture)
        {
            _client = fixture.CreateClient();
            _client.DefaultRequestHeaders.Add("ApiKey", "testing-api-key");
            _dbContext = fixture.Services.GetRequiredService<TramsDbContext>();
            _fixture = new Fixture();
            _randomGenerator = new RandomGenerator();
        }

        [Fact]
        public async Task CanCreateNewConcernCase()
        {
            var createRequest = Builder<ConcernCaseRequest>.CreateNew()
                .With(c => c.CreatedBy = "12345")
                .With(c => c.Description = "Description for case")
                .With(c => c.CrmEnquiry = "5678")
                .With(c => c.TrustUkprn = "100223")
                .With(c => c.ReasonForReview = "We have concerns")
                .With(c => c.DeEscalation = new DateTime(2022,04,01))
                .With(c => c.Issue = "Here is the issue")
                .With(c => c.CurrentStatus = "Case status")
                .With(c => c.CaseAim = "Here is the aim")
                .With(c => c.DeEscalationPoint = "Point of de-escalation")
                .With(c => c.NextSteps = "Here are the next steps")
                .With(c => c.DirectionOfTravel = "Up")
                .With(c => c.StatusUrn = 1)
                .Build();
            
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://trams-api.com/v2/concerns-cases"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
                Content =  JsonContent.Create(createRequest)
            };

            var caseToBeCreated = ConcernsCaseFactory.Create(createRequest);
            var expectedConcernsCaseResponse = ConcernsCaseResponseFactory.Create(caseToBeCreated);
            
            var expected = new ApiResponseV2<ConcernsCaseResponse>(expectedConcernsCaseResponse);
            
            var response = await _client.SendAsync(httpRequestMessage);
            response.StatusCode.Should().Be(201);
            var result = await response.Content.ReadFromJsonAsync<ApiResponseV2<ConcernsCaseResponse>>();
            
            var createdCase = _dbContext.ConcernsCase.FirstOrDefault(c => c.Urn == result.Data.First().Urn);
            expected.Data.First().Urn = createdCase.Urn;
            
            result.Should().BeEquivalentTo(expected);
            createdCase.Description.Should().BeEquivalentTo(createRequest.Description);
        }

        [Fact]
        public async Task CanGetConcernCaseByUrn()
        {
            SetupTestData("mockUkprn");
            var concernsCase = _dbContext.ConcernsCase.First();
            
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://trams-api.com/v2/concerns-cases/urn/{concernsCase.Urn}"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                }
            };
            
            var expectedConcernsCaseResponse = ConcernsCaseResponseFactory.Create(concernsCase);
            
            var expected = new ApiResponseV2<ConcernsCaseResponse>(expectedConcernsCaseResponse);
            
            var response = await _client.SendAsync(httpRequestMessage);
            
            response.StatusCode.Should().Be(200);
            var result = await response.Content.ReadFromJsonAsync<ApiResponseV2<ConcernsCaseResponse>>();
            result.Should().BeEquivalentTo(expected);
            result.Data.First().Urn.Should().BeEquivalentTo(concernsCase.Urn);
        }
        
        [Fact]
        public async Task CanGetConcernCaseByTrustUkprn()
        {
            var ukprn = "100008";
            SetupTestData(ukprn);
            var concernsCase = _dbContext.ConcernsCase.First();

            var expectedData = new List<ConcernsCase> {concernsCase};
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://trams-api.com/v2/concerns-cases/ukprn/{ukprn}"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                }
            };
            
            var expectedConcernsCaseResponse = ConcernsCaseResponseFactory.Create(concernsCase);
            var expectedPaging = new PagingResponse {Page = 1, RecordCount = expectedData.Count};
            
            var expected = new ApiResponseV2<ConcernsCaseResponse>(new List<ConcernsCaseResponse>{expectedConcernsCaseResponse}, expectedPaging);
            
            var response = await _client.SendAsync(httpRequestMessage);
            
            response.StatusCode.Should().Be(200);
            var result = await response.Content.ReadFromJsonAsync<ApiResponseV2<ConcernsCaseResponse>>();
            result.Should().BeEquivalentTo(expected);
            result.Data.First().Urn.Should().BeEquivalentTo(concernsCase.Urn);
        }
        
        [Fact]
        public async Task CanGetMultipleConcernCasesByTrustUkprn()
        {
            var ukprn = "100008";
            SetupTestData(ukprn);
            SetupTestData(ukprn);
            var concernsCases = _dbContext.ConcernsCase;
            var expectedData = concernsCases.ToList();
            
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://trams-api.com/v2/concerns-cases/ukprn/{ukprn}"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                }
            };
            var expectedPaging = new PagingResponse {Page = 1, RecordCount = expectedData.Count};

            var expectedConcernsCaseResponse = concernsCases.Select(c => ConcernsCaseResponseFactory.Create(c)).ToList();
            
            var expected = new ApiResponseV2<ConcernsCaseResponse>(expectedConcernsCaseResponse, expectedPaging);
            
            var response = await _client.SendAsync(httpRequestMessage);
            var content = await response.Content.ReadFromJsonAsync<ApiResponseV2<ConcernsCaseResponse>>();
            
            response.StatusCode.Should().Be(200);
            content.Should().BeEquivalentTo(expected);
            content.Data.Count().Should().Be(2);
        }


        private void SetupTestData(string trustUkprn)
        {
            var concernsCase = new ConcernsCase
            {
                CreatedAt = _randomGenerator.DateTime(),
                UpdatedAt = _randomGenerator.DateTime(),
                ReviewAt = _randomGenerator.DateTime(),
                ClosedAt = _randomGenerator.DateTime(),
                CreatedBy = _randomGenerator.NextString(3,10),
                Description = _randomGenerator.NextString(3,10),
                CrmEnquiry = _randomGenerator.NextString(3,10),
                TrustUkprn = trustUkprn,
                ReasonAtReview = _randomGenerator.NextString(3,10),
                DeEscalation = _randomGenerator.DateTime(),
                Issue = _randomGenerator.NextString(3,10),
                CurrentStatus = _randomGenerator.NextString(3,10),
                CaseAim = _randomGenerator.NextString(3,10),
                DeEscalationPoint = _randomGenerator.NextString(3,10),
                NextSteps = _randomGenerator.NextString(3,10),
                DirectionOfTravel = _randomGenerator.NextString(3,10),
                StatusUrn = 2,
            };

            _dbContext.ConcernsCase.Add(concernsCase);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.ConcernsCase.RemoveRange(_dbContext.ConcernsCase);
            _dbContext.SaveChanges();
        }
    }
}