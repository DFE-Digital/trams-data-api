using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TramsDataApi.DatabaseModels;
using TramsDataApi.RequestModels;
using Xunit;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using TramsDataApi.ResponseModels;
using TramsDataApi.Factories;

namespace TramsDataApi.Test.Integration
{
    [Collection("Database")]
    public class AcademyTransferProjectIntegrationTests : IClassFixture<TramsDataApiFactory>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly LegacyTramsDbContext _legacyTramsDbContext;
        private readonly TramsDbContext _tramsDbContext;

        private const int numberOfProjectsPerPage = 10;

        public AcademyTransferProjectIntegrationTests(TramsDataApiFactory fixture)
        {
            _client = fixture.CreateClient();
            _client.BaseAddress = new Uri("https://trams-api.com/");
            _legacyTramsDbContext = fixture.Services.GetRequiredService<LegacyTramsDbContext>();
            _tramsDbContext = fixture.Services.GetRequiredService<TramsDbContext>();
        }

        [Fact]
        public async Task CanCreateAnInitialAcademyTransferProject()
        {
            var randomGenerator = new RandomGenerator();

            var createRequest = Builder<AcademyTransferProjectRequest>.CreateNew()
                .With(c => c.OutgoingTrustUkprn = randomGenerator.NextString(8, 8))
                .With(c => c.Benefits = null)
                .With(c => c.Dates = null)
                .With(c => c.Rationale = null)
                .With(c => c.GeneralInformation = null)
                .With(c => c.Features = null)
                .With(c => c.TransferringAcademies =
                    (List<TransferringAcademiesRequest>) Builder<TransferringAcademiesRequest>
                        .CreateListOfSize(5).All()
                        .With(ta => ta.OutgoingAcademyUkprn = randomGenerator.NextString(8, 8))
                        .With(ta => ta.IncomingTrustUkprn = null).Build())
                .Build();

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://trams-api.com/academyTransferProject"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
                Content = JsonContent.Create(createRequest)
            };

            var response = await _client.SendAsync(httpRequestMessage);
            response.StatusCode.Should().Be(201);
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AcademyTransferProjectResponse>(jsonString);

            var createdProject =
                _tramsDbContext.AcademyTransferProjects.FirstOrDefault(atp => atp.Urn.ToString() == result.ProjectUrn);
            createdProject.Should().NotBe(null);
            createdProject.OutgoingTrustUkprn.Should().BeEquivalentTo(createRequest.OutgoingTrustUkprn);
        }

        [Fact]
        public async Task CanCreateAFullAcademyTransferProject()
        {
            var randomGenerator = new RandomGenerator();

            var benefitsRequest = Builder<AcademyTransferProjectBenefitsRequest>.CreateNew()
                .With(b => b.IntendedTransferBenefits = Builder<IntendedTransferBenefitRequest>.CreateNew()
                    .With(i => i.SelectedBenefits = new List<string>()).Build())
                .With(b => b.OtherFactorsToConsider = Builder<OtherFactorsToConsiderRequest>.CreateNew()
                    .With(o => o.ComplexLandAndBuilding = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .With(o => o.FinanceAndDebt = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .With(o => o.HighProfile = Builder<BenefitConsideredFactorRequest>.CreateNew().Build()).Build())
                .Build();

            var datesRequest = Builder<AcademyTransferProjectDatesRequest>.CreateNew()
                .With(d => d.TransferFirstDiscussed =
                    randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.TargetDateForTransfer =
                    randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.HtbDate = randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.HasHtbDate = true)
                .With(d => d.HasTargetDateForTransfer = true)
                .With(d => d.HasTransferFirstDiscussedDate = true)
                .Build();

            var createRequest = Builder<AcademyTransferProjectRequest>.CreateNew()
                .With(c => c.OutgoingTrustUkprn = randomGenerator.NextString(8, 8))
                .With(c => c.Benefits = benefitsRequest)
                .With(c => c.Dates = datesRequest)
                .With(c => c.Rationale = Builder<AcademyTransferProjectRationaleRequest>.CreateNew().Build())
                .With(c => c.GeneralInformation =
                    Builder<AcademyTransferProjectGeneralInformationRequest>.CreateNew().Build())
                .With(c => c.Features = Builder<AcademyTransferProjectFeaturesRequest>.CreateNew().Build())
                .With(c => c.TransferringAcademies =
                    (List<TransferringAcademiesRequest>) Builder<TransferringAcademiesRequest>
                        .CreateListOfSize(5).All()
                        .With(ta => ta.IncomingTrustUkprn = randomGenerator.NextString(8, 8))
                        .With(ta => ta.OutgoingAcademyUkprn = randomGenerator.NextString(8, 8)).Build())
                .Build();

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://trams-api.com/academyTransferProject"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
                Content = JsonContent.Create(createRequest)
            };

            var response = await _client.SendAsync(httpRequestMessage);
            response.StatusCode.Should().Be(201);
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AcademyTransferProjectResponse>(jsonString);

            var createdProject =
                _tramsDbContext.AcademyTransferProjects.FirstOrDefault(atp => atp.Urn.ToString() == result.ProjectUrn);
            createdProject.Should().NotBe(null);
            createdProject.OutgoingTrustUkprn.Should().BeEquivalentTo(createRequest.OutgoingTrustUkprn);
        }

        [Fact]
        public async Task CanUpdateAnAcademyTransferProject()
        {
            var randomGenerator = new RandomGenerator();

            var benefitsRequest = Builder<AcademyTransferProjectBenefitsRequest>.CreateNew()
                .With(b => b.IntendedTransferBenefits = Builder<IntendedTransferBenefitRequest>.CreateNew()
                    .With(i => i.SelectedBenefits = new List<string>()).Build())
                .With(b => b.OtherFactorsToConsider = Builder<OtherFactorsToConsiderRequest>.CreateNew()
                    .With(o => o.ComplexLandAndBuilding = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .With(o => o.FinanceAndDebt = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .With(o => o.HighProfile = Builder<BenefitConsideredFactorRequest>.CreateNew().Build()).Build())
                .Build();

            var datesRequest = Builder<AcademyTransferProjectDatesRequest>.CreateNew()
                .With(d => d.TransferFirstDiscussed =
                    randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.TargetDateForTransfer =
                    randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.HtbDate = randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.HasHtbDate = true)
                .With(d => d.HasTargetDateForTransfer = true)
                .With(d => d.HasTransferFirstDiscussedDate = true)
                .Build();

            var createRequest = Builder<AcademyTransferProjectRequest>.CreateNew()
                .With(c => c.OutgoingTrustUkprn = randomGenerator.NextString(8, 8))
                .With(c => c.Benefits = benefitsRequest)
                .With(c => c.Dates = datesRequest)
                .With(c => c.Rationale = Builder<AcademyTransferProjectRationaleRequest>.CreateNew().Build())
                .With(c => c.GeneralInformation =
                    Builder<AcademyTransferProjectGeneralInformationRequest>.CreateNew().Build())
                .With(c => c.Features = Builder<AcademyTransferProjectFeaturesRequest>.CreateNew().Build())
                .With(c => c.TransferringAcademies =
                    (List<TransferringAcademiesRequest>) Builder<TransferringAcademiesRequest>
                        .CreateListOfSize(5).All()
                        .With(ta => ta.IncomingTrustUkprn = randomGenerator.NextString(8, 8))
                        .With(ta => ta.OutgoingAcademyUkprn = randomGenerator.NextString(8, 8)).Build())
                .Build();

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://trams-api.com/academyTransferProject"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
                Content = JsonContent.Create(createRequest)
            };

            var response = await _client.SendAsync(httpRequestMessage);
            response.StatusCode.Should().Be(201);
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AcademyTransferProjectResponse>(jsonString);

            createRequest.OutgoingTrustUkprn = "14567231";

            var updateRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri($"https://trams-api.com/academyTransferProject/{result.ProjectUrn}"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
                Content = JsonContent.Create(createRequest)
            };

            var updateResponse = await _client.SendAsync(updateRequestMessage);
            updateResponse.StatusCode.Should().Be(200);
            var updateJson = await updateResponse.Content.ReadAsStringAsync();
            var updatedProjectResponse = JsonConvert.DeserializeObject<AcademyTransferProjectResponse>(updateJson);

            var updatedProject =
                _tramsDbContext.AcademyTransferProjects.FirstOrDefault(atp =>
                    atp.Urn.ToString() == updatedProjectResponse.ProjectUrn);
            updatedProject.OutgoingTrustUkprn.Should().Be(createRequest.OutgoingTrustUkprn);
        }

        [Fact]
        public async Task CanUpdateTheSelectedBenefitsOfAnAcademyTransferProject()
        {
            var randomGenerator = new RandomGenerator();

            var benefitsRequest = Builder<AcademyTransferProjectBenefitsRequest>.CreateNew()
                .With(b => b.IntendedTransferBenefits = Builder<IntendedTransferBenefitRequest>.CreateNew()
                    .With(i => i.SelectedBenefits = new List<string>() {"Initial benefit", "other benefit"}).Build())
                .With(b => b.OtherFactorsToConsider = Builder<OtherFactorsToConsiderRequest>.CreateNew()
                    .With(o => o.ComplexLandAndBuilding = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .With(o => o.FinanceAndDebt = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .With(o => o.HighProfile = Builder<BenefitConsideredFactorRequest>.CreateNew().Build()).Build())
                .Build();

            var datesRequest = Builder<AcademyTransferProjectDatesRequest>.CreateNew()
                .With(d => d.TransferFirstDiscussed =
                    randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.TargetDateForTransfer =
                    randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.HtbDate = randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.HasHtbDate = true)
                .With(d => d.HasTargetDateForTransfer = true)
                .With(d => d.HasTransferFirstDiscussedDate = true)
                .Build();

            var createRequest = Builder<AcademyTransferProjectRequest>.CreateNew()
                .With(c => c.OutgoingTrustUkprn = randomGenerator.NextString(8, 8))
                .With(c => c.Benefits = benefitsRequest)
                .With(c => c.Dates = datesRequest)
                .With(c => c.Rationale = Builder<AcademyTransferProjectRationaleRequest>.CreateNew().Build())
                .With(c => c.GeneralInformation =
                    Builder<AcademyTransferProjectGeneralInformationRequest>.CreateNew().Build())
                .With(c => c.Features = Builder<AcademyTransferProjectFeaturesRequest>.CreateNew().Build())
                .With(c => c.TransferringAcademies =
                    (List<TransferringAcademiesRequest>) Builder<TransferringAcademiesRequest>
                        .CreateListOfSize(1).All()
                        .With(ta => ta.IncomingTrustUkprn = randomGenerator.NextString(8, 8))
                        .With(ta => ta.OutgoingAcademyUkprn = randomGenerator.NextString(8, 8)).Build())
                .Build();

            _tramsDbContext.AcademyTransferProjectIntendedTransferBenefits.Count().Should().Be(0);

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://trams-api.com/academyTransferProject"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
                Content = JsonContent.Create(createRequest)
            };

            var response = await _client.SendAsync(httpRequestMessage);
            response.StatusCode.Should().Be(201);
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AcademyTransferProjectResponse>(jsonString);

            createRequest.Benefits.IntendedTransferBenefits.SelectedBenefits = new List<string>()
                {"new initial benefit", "new other benefit"};

            var updateRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri($"https://trams-api.com/academyTransferProject/{result.ProjectUrn}"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
                Content = JsonContent.Create(createRequest)
            };

            var updateResponse = await _client.SendAsync(updateRequestMessage);
            updateResponse.StatusCode.Should().Be(200);
            var updateJson = await updateResponse.Content.ReadAsStringAsync();
            var updatedProjectResponse = JsonConvert.DeserializeObject<AcademyTransferProjectResponse>(updateJson);

            var updatedProject = _tramsDbContext.AcademyTransferProjects
                .Include(atp => atp.AcademyTransferProjectIntendedTransferBenefits)
                .FirstOrDefault(atp => atp.Urn.ToString() == updatedProjectResponse.ProjectUrn);

            updatedProject?.AcademyTransferProjectIntendedTransferBenefits.Count.Should().Be(2);
            _tramsDbContext.AcademyTransferProjectIntendedTransferBenefits.Count().Should().Be(2);

            updatedProject?.AcademyTransferProjectIntendedTransferBenefits
                .ElementAt(0).SelectedBenefit.Should().Be("new initial benefit");
            updatedProject?.AcademyTransferProjectIntendedTransferBenefits
                .ElementAt(1).SelectedBenefit.Should().Be("new other benefit");
        }

        [Fact]
        public async Task CanUpdateTheTransferringAcademiesOfAnAcademyTransferProject()
        {
            var randomGenerator = new RandomGenerator();

            var benefitsRequest = Builder<AcademyTransferProjectBenefitsRequest>.CreateNew()
                .With(b => b.IntendedTransferBenefits = Builder<IntendedTransferBenefitRequest>.CreateNew()
                    .With(i => i.SelectedBenefits = new List<string>()).Build())
                .With(b => b.OtherFactorsToConsider = Builder<OtherFactorsToConsiderRequest>.CreateNew()
                    .With(o => o.ComplexLandAndBuilding = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .With(o => o.FinanceAndDebt = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .With(o => o.HighProfile = Builder<BenefitConsideredFactorRequest>.CreateNew().Build()).Build())
                .Build();

            var datesRequest = Builder<AcademyTransferProjectDatesRequest>.CreateNew()
                .With(d => d.TransferFirstDiscussed =
                    randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.TargetDateForTransfer =
                    randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.HtbDate = randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.HasHtbDate = true)
                .With(d => d.HasTargetDateForTransfer = true)
                .With(d => d.HasTransferFirstDiscussedDate = true)
                .Build();

            var createRequest = Builder<AcademyTransferProjectRequest>.CreateNew()
                .With(c => c.OutgoingTrustUkprn = randomGenerator.NextString(8, 8))
                .With(c => c.Benefits = benefitsRequest)
                .With(c => c.Dates = datesRequest)
                .With(c => c.Rationale = Builder<AcademyTransferProjectRationaleRequest>.CreateNew().Build())
                .With(c => c.GeneralInformation =
                    Builder<AcademyTransferProjectGeneralInformationRequest>.CreateNew().Build())
                .With(c => c.Features = Builder<AcademyTransferProjectFeaturesRequest>.CreateNew().Build())
                .With(c => c.TransferringAcademies =
                    (List<TransferringAcademiesRequest>) Builder<TransferringAcademiesRequest>
                        .CreateListOfSize(2).All()
                        .With(ta => ta.IncomingTrustUkprn = randomGenerator.NextString(8, 8))
                        .With(ta => ta.OutgoingAcademyUkprn = randomGenerator.NextString(8, 8))
                        .Build())
                .Build();

            _tramsDbContext.TransferringAcademies.Count().Should().Be(0);

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://trams-api.com/academyTransferProject"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
                Content = JsonContent.Create(createRequest)
            };

            var response = await _client.SendAsync(httpRequestMessage);
            response.StatusCode.Should().Be(201);
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AcademyTransferProjectResponse>(jsonString);

            createRequest.TransferringAcademies = new List<TransferringAcademiesRequest>()
            {
                new TransferringAcademiesRequest
                {
                    OutgoingAcademyUkprn = "12345678",
                    IncomingTrustUkprn = "12345678",
                    PupilNumbersAdditionalInformation = "pupil info",
                    LatestOfstedReportAdditionalInformation = "ofsted info",
                    KeyStage2PerformanceAdditionalInformation = "ks2",
                    KeyStage4PerformanceAdditionalInformation = "ks4",
                    KeyStage5PerformanceAdditionalInformation = "ks5"
                },
                new TransferringAcademiesRequest
                {
                    OutgoingAcademyUkprn = "87654321",
                    IncomingTrustUkprn = "87654321",
                    PupilNumbersAdditionalInformation = "pupil info 2",
                    LatestOfstedReportAdditionalInformation = "ofsted info 2",
                    KeyStage2PerformanceAdditionalInformation = "ks2 2",
                    KeyStage4PerformanceAdditionalInformation = "ks4 2",
                    KeyStage5PerformanceAdditionalInformation = "ks5 2"
                }
            };

            var updateRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri($"https://trams-api.com/academyTransferProject/{result.ProjectUrn}"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
                Content = JsonContent.Create(createRequest)
            };

            var updateResponse = await _client.SendAsync(updateRequestMessage);
            updateResponse.StatusCode.Should().Be(200);
            var updateJson = await updateResponse.Content.ReadAsStringAsync();
            var updatedProjectResponse = JsonConvert.DeserializeObject<AcademyTransferProjectResponse>(updateJson);

            var updatedProject = _tramsDbContext.AcademyTransferProjects
                .Include(atp => atp.TransferringAcademies)
                .FirstOrDefault(atp => atp.Urn.ToString() == updatedProjectResponse.ProjectUrn);

            updatedProject?.TransferringAcademies.Count.Should().Be(2);
            _tramsDbContext.TransferringAcademies.Count().Should().Be(2);

            var firstCreateAcademy = createRequest.TransferringAcademies.ElementAt(0);
            var secondCreateAcademy = createRequest.TransferringAcademies.ElementAt(1);
           
            updatedProject?.TransferringAcademies.ElementAt(0).IncomingTrustUkprn.Should().Be(firstCreateAcademy.IncomingTrustUkprn);
            updatedProject?.TransferringAcademies.ElementAt(0).OutgoingAcademyUkprn.Should().Be(firstCreateAcademy.OutgoingAcademyUkprn);
            updatedProject?.TransferringAcademies.ElementAt(0).LatestOfstedReportAdditionalInformation.Should().Be(firstCreateAcademy.LatestOfstedReportAdditionalInformation);
            updatedProject?.TransferringAcademies.ElementAt(0).PupilNumbersAdditionalInformation.Should().Be(firstCreateAcademy.PupilNumbersAdditionalInformation);
            updatedProject?.TransferringAcademies.ElementAt(0).KeyStage2PerformanceAdditionalInformation.Should().Be(firstCreateAcademy.KeyStage2PerformanceAdditionalInformation);
            updatedProject?.TransferringAcademies.ElementAt(0).KeyStage4PerformanceAdditionalInformation.Should().Be(firstCreateAcademy.KeyStage4PerformanceAdditionalInformation);
            updatedProject?.TransferringAcademies.ElementAt(0).KeyStage5PerformanceAdditionalInformation.Should().Be(firstCreateAcademy.KeyStage5PerformanceAdditionalInformation);

            updatedProject?.TransferringAcademies.ElementAt(1).IncomingTrustUkprn.Should().Be(secondCreateAcademy.IncomingTrustUkprn);
            updatedProject?.TransferringAcademies.ElementAt(1).OutgoingAcademyUkprn.Should().Be(secondCreateAcademy.OutgoingAcademyUkprn);
            updatedProject?.TransferringAcademies.ElementAt(1).LatestOfstedReportAdditionalInformation.Should().Be(secondCreateAcademy.LatestOfstedReportAdditionalInformation);
            updatedProject?.TransferringAcademies.ElementAt(1).PupilNumbersAdditionalInformation.Should().Be(secondCreateAcademy.PupilNumbersAdditionalInformation);
            updatedProject?.TransferringAcademies.ElementAt(1).KeyStage2PerformanceAdditionalInformation.Should().Be(secondCreateAcademy.KeyStage2PerformanceAdditionalInformation);
            updatedProject?.TransferringAcademies.ElementAt(1).KeyStage4PerformanceAdditionalInformation.Should().Be(secondCreateAcademy.KeyStage4PerformanceAdditionalInformation);
            updatedProject?.TransferringAcademies.ElementAt(1).KeyStage5PerformanceAdditionalInformation.Should().Be(secondCreateAcademy.KeyStage5PerformanceAdditionalInformation);            
        }

        [Fact]
        public async Task CanGetTheFirstPageOfAllAcademyTransferProjects()
        {
            const int numberOfProjects = 20;
            var randomGenerator = new RandomGenerator();
 
            var groups = AddGroups(2);
            var outgoingTrustGroup = groups[0];
            var incomingTrustGroup = groups[1];

            var trusts = AddTrustsFromGroups(groups);

            var academyTransferProjectsToCreate = Builder<AcademyTransferProjects>
                .CreateListOfSize(numberOfProjects)
                .All()
                .With(atp => atp.OutgoingTrustUkprn = outgoingTrustGroup.Ukprn)
                .With(atp => atp.Urn = 0)
                .With(atp => atp.Id = 0)
                .With(atp => atp.TransferringAcademies = Builder<TransferringAcademies>
                    .CreateListOfSize(3)
                    .All()
                    .With(ta => ta.Id = 0)
                    .With(ta => ta.OutgoingAcademyUkprn = randomGenerator.NextString(8, 8))
                    .With(ta => ta.IncomingTrustUkprn = incomingTrustGroup.Ukprn)
                    .With(ta => ta.FkAcademyTransferProjectId = null)
                    .With(ta => ta.PupilNumbersAdditionalInformation = randomGenerator.NextString(0,1000))
                    .With(ta => ta.LatestOfstedReportAdditionalInformation = randomGenerator.NextString(0,1000))
                    .With(ta => ta.KeyStage2PerformanceAdditionalInformation = randomGenerator.NextString(0,1000))
                    .With(ta => ta.KeyStage4PerformanceAdditionalInformation = randomGenerator.NextString(0,1000))
                    .With(ta => ta.KeyStage5PerformanceAdditionalInformation = randomGenerator.NextString(0,1000))
                    .Build()
                )
                .With(atp => atp.AcademyTransferProjectIntendedTransferBenefits =
                    Builder<AcademyTransferProjectIntendedTransferBenefits>
                        .CreateListOfSize(5)
                        .All()
                        .With(benefit => benefit.Id = 0)
                        .Build()
                )
                .Build().ToList();

            _tramsDbContext.AcademyTransferProjects.AddRange(academyTransferProjectsToCreate);
            _tramsDbContext.SaveChanges();

            var indexAcademyTransferProjectRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://trams-api.com/academyTransferProject/"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
            };

            var indexResponse = await _client.SendAsync(indexAcademyTransferProjectRequest);
            indexResponse.StatusCode.Should().Be(200);
            var indexJson = await indexResponse.Content.ReadAsStringAsync();
            var indexProjectResponse =
                JsonConvert.DeserializeObject<List<AcademyTransferProjectSummaryResponse>>(indexJson);

            var expectedResponse = academyTransferProjectsToCreate.OrderByDescending(atp => atp.Id).Take(numberOfProjectsPerPage).Select(atp =>
                new AcademyTransferProjectSummaryResponse
                {
                    ProjectUrn = atp.Urn.ToString(),
                    OutgoingTrustUkprn = outgoingTrustGroup.Ukprn,
                    OutgoingTrustName = outgoingTrustGroup.GroupName,
                    ProjectReference = atp.ProjectReference,                    
                    TransferringAcademies = atp.TransferringAcademies.Select(ta => new TransferringAcademiesResponse
                    {
                        OutgoingAcademyUkprn = ta.OutgoingAcademyUkprn,
                        IncomingTrustUkprn = ta.IncomingTrustUkprn,
                        IncomingTrustName = incomingTrustGroup.GroupName,
                        PupilNumbersAdditionalInformation = ta.PupilNumbersAdditionalInformation,
                        LatestOfstedReportAdditionalInformation = ta.LatestOfstedReportAdditionalInformation,
                        KeyStage2PerformanceAdditionalInformation = ta.KeyStage2PerformanceAdditionalInformation,
                        KeyStage4PerformanceAdditionalInformation = ta.KeyStage4PerformanceAdditionalInformation,
                        KeyStage5PerformanceAdditionalInformation = ta.KeyStage5PerformanceAdditionalInformation
                    }).ToList()
                }).ToList();
            indexProjectResponse.Count().Should().Be(numberOfProjectsPerPage);
            indexProjectResponse.Should().BeEquivalentTo(expectedResponse);
         }


        [Fact]
        public async Task CanGetTheSecondPageOfAllAcademyTransferProjects()
        {
            const int numberOfProjects = 20;
            const int pageNumberRequested = 2;

            var randomGenerator = new RandomGenerator();
 
            var groups = AddGroups(2);
            var outgoingTrustGroup = groups[0];
            var incomingTrustGroup = groups[1];
            var trusts = AddTrustsFromGroups(groups);

            var academyTransferProjectsToCreate = Builder<AcademyTransferProjects>
                .CreateListOfSize(numberOfProjects)
                .All()
                .With(atp => atp.OutgoingTrustUkprn = outgoingTrustGroup.Ukprn)
                .With(atp => atp.Urn = 0)
                .With(atp => atp.Id = 0)
                .With(atp => atp.TransferringAcademies = Builder<TransferringAcademies>
                    .CreateListOfSize(3)
                    .All()
                    .With(ta => ta.Id = 0)
                    .With(ta => ta.OutgoingAcademyUkprn = randomGenerator.NextString(8, 8))
                    .With(ta => ta.IncomingTrustUkprn = incomingTrustGroup.Ukprn)
                    .With(ta => ta.FkAcademyTransferProjectId = null)
                    .With(ta => ta.PupilNumbersAdditionalInformation = randomGenerator.NextString(0,1000))
                    .With(ta => ta.LatestOfstedReportAdditionalInformation = randomGenerator.NextString(0,1000))
                    .With(ta => ta.KeyStage2PerformanceAdditionalInformation = randomGenerator.NextString(0,1000))
                    .With(ta => ta.KeyStage4PerformanceAdditionalInformation = randomGenerator.NextString(0,1000))
                    .With(ta => ta.KeyStage5PerformanceAdditionalInformation = randomGenerator.NextString(0,1000))
                    .Build()
                )
                .With(atp => atp.AcademyTransferProjectIntendedTransferBenefits =
                    Builder<AcademyTransferProjectIntendedTransferBenefits>
                        .CreateListOfSize(5)
                        .All()
                        .With(benefit => benefit.Id = 0)
                        .Build()
                )
                .Build().ToList();

            _tramsDbContext.AcademyTransferProjects.AddRange(academyTransferProjectsToCreate);
            _tramsDbContext.SaveChanges();

            
            var indexAcademyTransferProjectRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://trams-api.com/academyTransferProject?page={pageNumberRequested}"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
            };

            var indexResponse = await _client.SendAsync(indexAcademyTransferProjectRequest);
            indexResponse.StatusCode.Should().Be(200);
            var indexJson = await indexResponse.Content.ReadAsStringAsync();
            var indexProjectResponse =
                JsonConvert.DeserializeObject<List<AcademyTransferProjectSummaryResponse>>(indexJson);

            var expectedResponse = academyTransferProjectsToCreate.OrderByDescending(atp => atp.Id).Skip(numberOfProjectsPerPage).Take(numberOfProjectsPerPage).Select(
                atp =>
                    new AcademyTransferProjectSummaryResponse
                    {
                        ProjectUrn = atp.Urn.ToString(),
                        ProjectReference = atp.ProjectReference,
                        OutgoingTrustUkprn = atp.OutgoingTrustUkprn,
                        OutgoingTrustName = outgoingTrustGroup.GroupName,
                        TransferringAcademies = atp.TransferringAcademies.Select(ta => new TransferringAcademiesResponse
                        {
                            OutgoingAcademyUkprn = ta.OutgoingAcademyUkprn,
                            IncomingTrustUkprn = ta.IncomingTrustUkprn,
                            IncomingTrustName = incomingTrustGroup.GroupName,
                            PupilNumbersAdditionalInformation = ta.PupilNumbersAdditionalInformation,
                            LatestOfstedReportAdditionalInformation = ta.LatestOfstedReportAdditionalInformation,
                            KeyStage2PerformanceAdditionalInformation = ta.KeyStage2PerformanceAdditionalInformation,
                            KeyStage4PerformanceAdditionalInformation = ta.KeyStage4PerformanceAdditionalInformation,
                            KeyStage5PerformanceAdditionalInformation = ta.KeyStage5PerformanceAdditionalInformation
                        }).ToList()
                    }).ToList();
            indexProjectResponse.Count().Should().Be(numberOfProjectsPerPage);
            indexProjectResponse.Should().BeEquivalentTo(expectedResponse);
        }


        [Fact]
        public async Task ReturnsEmptyListWhenPageTooHigh()
        {
            var randomGenerator = new RandomGenerator();
            var academyTransferProjectsToCreate = Builder<AcademyTransferProjects>
                .CreateListOfSize(20)
                .All()
                .With(atp => atp.OutgoingTrustUkprn = randomGenerator.NextString(8, 8))
                .With(atp => atp.Urn = 0)
                .With(atp => atp.Id = 0)
                .With(atp => atp.TransferringAcademies = Builder<TransferringAcademies>
                    .CreateListOfSize(3)
                    .All()
                    .With(ta => ta.Id = 0)
                    .With(ta => ta.OutgoingAcademyUkprn = randomGenerator.NextString(8, 8))
                    .With(ta => ta.IncomingTrustUkprn = randomGenerator.NextString(8, 8))
                    .With(ta => ta.FkAcademyTransferProjectId = null)
                    .Build()
                )
                .With(atp => atp.AcademyTransferProjectIntendedTransferBenefits =
                    Builder<AcademyTransferProjectIntendedTransferBenefits>
                        .CreateListOfSize(5)
                        .All()
                        .With(benefit => benefit.Id = 0)
                        .Build()
                )
                .Build().ToList();

            _tramsDbContext.AcademyTransferProjects.AddRange(academyTransferProjectsToCreate);
            _tramsDbContext.SaveChanges();

            var indexAcademyTransferProjectRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://trams-api.com/academyTransferProject?page=4"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
            };

            var indexResponse = await _client.SendAsync(indexAcademyTransferProjectRequest);
            indexResponse.StatusCode.Should().Be(200);
            var indexJson = await indexResponse.Content.ReadAsStringAsync();
            var indexProjectResponse =
                JsonConvert.DeserializeObject<List<AcademyTransferProjectSummaryResponse>>(indexJson);

            indexProjectResponse.Count().Should().Be(0);
            indexProjectResponse.Should().BeEquivalentTo(new List<AcademyTransferProjectSummaryResponse>());
        }

        [Fact]
        public async Task Returns404WhenAcademyTransferProjectDoesNotExist()
        {
            var getAcademyTransferProjectRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://trams-api.com/academyTransferProject/10012313"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
            };

            var getResponse = await _client.SendAsync(getAcademyTransferProjectRequest);
            getResponse.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task CanGetAnAcademyTransferProject()
        {
            var randomGenerator = new RandomGenerator();

            var academyTransferProject = Builder<AcademyTransferProjects>
                .CreateNew()
                .With(atp => atp.OutgoingTrustUkprn = randomGenerator.NextString(8, 8))
                .With(atp => atp.Urn = 0)
                .With(atp => atp.Id = 0)
                .With(atp => atp.TransferringAcademies = Builder<TransferringAcademies>
                    .CreateListOfSize(3)
                    .All()
                    .With(ta => ta.Id = 0)
                    .With(ta => ta.OutgoingAcademyUkprn = randomGenerator.NextString(8, 8))
                    .With(ta => ta.IncomingTrustUkprn = randomGenerator.NextString(8, 8))
                    .With(ta => ta.FkAcademyTransferProjectId = null)
                    .Build()
                )
                .With(atp => atp.AcademyTransferProjectIntendedTransferBenefits =
                    Builder<AcademyTransferProjectIntendedTransferBenefits>
                        .CreateListOfSize(5)
                        .All()
                        .With(benefit => benefit.Id = 0)
                        .Build()
                )
                .Build();

            _tramsDbContext.AcademyTransferProjects.Add(academyTransferProject);
            _tramsDbContext.SaveChanges();

            var getAcademyTransferProjectRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://trams-api.com/academyTransferProject/{academyTransferProject.Urn}"),
                Headers =
                {
                    {"ApiKey", "testing-api-key"}
                },
            };

            var getResponse = await _client.SendAsync(getAcademyTransferProjectRequest);
            getResponse.StatusCode.Should().Be(200);

            var responseJson = await getResponse.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<AcademyTransferProjectResponse>(responseJson);

            var expected = AcademyTransferProjectResponseFactory.Create(academyTransferProject);

            responseModel.Should().BeEquivalentTo(expected);
        }

        private AcademyTransferProjectRequest GenerateCreateRequest()
        {
            var randomGenerator = new RandomGenerator();

            var benefitsRequest = Builder<AcademyTransferProjectBenefitsRequest>.CreateNew()
                .With(b => b.IntendedTransferBenefits = Builder<IntendedTransferBenefitRequest>.CreateNew()
                    .With(i => i.SelectedBenefits = new List<string>()).Build())
                .With(b => b.OtherFactorsToConsider = Builder<OtherFactorsToConsiderRequest>.CreateNew()
                    .With(o => o.ComplexLandAndBuilding = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .With(o => o.FinanceAndDebt = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .With(o => o.HighProfile = Builder<BenefitConsideredFactorRequest>.CreateNew().Build()).Build())
                .Build();

            var datesRequest = Builder<AcademyTransferProjectDatesRequest>.CreateNew()
                .With(d => d.TransferFirstDiscussed =
                    randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.TargetDateForTransfer =
                    randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.HtbDate = randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.HasHtbDate = true)
                .With(d => d.HasTargetDateForTransfer = true)
                .With(d => d.HasTransferFirstDiscussedDate = true)
                .Build();

            return Builder<AcademyTransferProjectRequest>.CreateNew()
                .With(c => c.OutgoingTrustUkprn = randomGenerator.NextString(8, 8))
                .With(c => c.Benefits = benefitsRequest)
                .With(c => c.Dates = datesRequest)
                .With(c => c.Rationale = Builder<AcademyTransferProjectRationaleRequest>.CreateNew().Build())
                .With(c => c.GeneralInformation =
                    Builder<AcademyTransferProjectGeneralInformationRequest>.CreateNew().Build())
                .With(c => c.Features = Builder<AcademyTransferProjectFeaturesRequest>.CreateNew().Build())
                .With(c => c.TransferringAcademies =
                    (List<TransferringAcademiesRequest>) Builder<TransferringAcademiesRequest>
                        .CreateListOfSize(2).All()
                        .With(ta => ta.IncomingTrustUkprn = randomGenerator.NextString(8, 8))
                        .With(ta => ta.OutgoingAcademyUkprn = randomGenerator.NextString(8, 8)).Build())
                .Build();
        }
        private IList<Group> AddGroups(int numberOfGroups)
        {
            var randomGenerator = new RandomGenerator();
            var groups = Builder<Group>.CreateListOfSize(numberOfGroups)
               .All()
               .With(g => g.Ukprn = randomGenerator.NextString(8, 8))
               .With(g => g.GroupId = randomGenerator.NextString(7, 7))
               .With(g => g.GroupName = randomGenerator.NextString(8, 8))
               .With(g => g.GroupUid = randomGenerator.Int().ToString())
               .Build();

            _legacyTramsDbContext.Group.AddRange(groups);
            _legacyTramsDbContext.SaveChanges();

            return groups;
        }

        private IList<Trust> AddTrustsFromGroups(IList<Group> groups)
        {
            var randomGenerator = new RandomGenerator();
            var trusts = groups.Select((group, index) => new Trust { Rid = index.ToString(), TrustRef = group.GroupId }).ToList();
            AddTrusts(trusts);
            return trusts;
        }

        private void AddTrusts(IList<Trust> trusts)
        {
            _legacyTramsDbContext.Trust.AddRange(trusts);
            _legacyTramsDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _tramsDbContext.TransferringAcademies.RemoveRange(_tramsDbContext.TransferringAcademies);
            _tramsDbContext.AcademyTransferProjectIntendedTransferBenefits
                .RemoveRange(_tramsDbContext.AcademyTransferProjectIntendedTransferBenefits);
            _tramsDbContext.AcademyTransferProjects.RemoveRange(_tramsDbContext.AcademyTransferProjects);
            _tramsDbContext.SaveChanges();

            _legacyTramsDbContext.Group.RemoveRange(_legacyTramsDbContext.Group);
            _legacyTramsDbContext.Trust.RemoveRange(_legacyTramsDbContext.Trust);
            _legacyTramsDbContext.SaveChanges();
        }
    }
}