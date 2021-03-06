using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TramsDataApi.Controllers.V2;
using TramsDataApi.DatabaseModels;
using TramsDataApi.Factories.CaseActionFactories;
using TramsDataApi.Gateways;
using TramsDataApi.RequestModels.CaseActions.FinancialPlan;
using TramsDataApi.RequestModels.CaseActions.SRMA;
using TramsDataApi.ResponseModels;
using TramsDataApi.ResponseModels.CaseActions.FinancialPlan;
using TramsDataApi.ResponseModels.CaseActions.SRMA;
using TramsDataApi.UseCases;
using Xunit;

namespace TramsDataApi.Test.Controllers
{
    public class FinancialPlanControllerTests
    {
        private readonly Mock<ILogger<FinancialPlanController>> _mockLogger;
        private readonly Mock<IUseCase<CreateFinancialPlanRequest, FinancialPlanResponse>> _mockCreateFinancialPlanUseCase;
        private readonly Mock<IUseCase<long, FinancialPlanResponse>> _mockGetFinancialPlanByIdUseCase;
        private readonly Mock<IUseCase<int, List<FinancialPlanResponse>>> _mockGetFinancialPlansByCaseUseCase;
        private readonly Mock<IUseCase<PatchFinancialPlanRequest, FinancialPlanResponse>> _mockPatchFinancialPlanUseCase;
        private readonly Mock<IUseCase<object, List<FinancialPlanStatus>>> _mockGetAllStatuses;

        private readonly FinancialPlanController controllerSUT;

        public FinancialPlanControllerTests()
        {
            _mockLogger = new Mock<ILogger<FinancialPlanController>>();
            _mockCreateFinancialPlanUseCase = new Mock<IUseCase<CreateFinancialPlanRequest, FinancialPlanResponse>>();
            _mockGetFinancialPlanByIdUseCase = new Mock<IUseCase<long, FinancialPlanResponse>>();
            _mockGetFinancialPlansByCaseUseCase = new Mock<IUseCase<int, List<FinancialPlanResponse>>>();
            _mockPatchFinancialPlanUseCase = new Mock<IUseCase<PatchFinancialPlanRequest, FinancialPlanResponse>>();
            _mockGetAllStatuses = new Mock<IUseCase<object, List<FinancialPlanStatus>>>();

            controllerSUT = new FinancialPlanController(_mockLogger.Object, _mockCreateFinancialPlanUseCase.Object, _mockGetFinancialPlanByIdUseCase.Object,
                _mockGetFinancialPlansByCaseUseCase.Object, _mockPatchFinancialPlanUseCase.Object, _mockGetAllStatuses.Object);
        }

        [Fact]
        public void Create_ReturnsApiSingleResponseWithNewFinancialPlan()
        {
            var status = 2;
            var createdAt = DateTime.Now.AddDays(-5);
            var caseUrn = 223;

            var response = Builder<FinancialPlanResponse>
                .CreateNew()
                .With(r => r.StatusId = status)
                .With(r => r.CreatedAt = createdAt)
                .Build();

            var expectedResponse = new ApiSingleResponseV2<FinancialPlanResponse>(response);

            _mockCreateFinancialPlanUseCase
                .Setup(x => x.Execute(It.IsAny<CreateFinancialPlanRequest>()))
                .Returns(response);

            var result = controllerSUT.Create(new CreateFinancialPlanRequest
            {
                StatusId = status,
                CaseUrn = caseUrn,
                CreatedAt = createdAt,
            });

            result.Result.Should().BeEquivalentTo(new ObjectResult(expectedResponse) { StatusCode = StatusCodes.Status201Created });
        }

        [Fact]
        public void GetFinancialPlansByCaseId_ReturnsMatchingFinancialPlan_WhenGivenCaseId()
        {
            var caseUrn = 123;

            var matchingFinancialPlan = new FinancialPlanCase
            {
                CaseUrn = caseUrn,
                Notes = "match"
            };

            var fps = new List<FinancialPlanCase> {
                matchingFinancialPlan,
                new FinancialPlanCase {
                    CaseUrn = 222,
                    Notes = "FinancialPlan 2"
                },
                new FinancialPlanCase {
                    CaseUrn = 456,
                    Notes = "FinancialPlan 3"
                }
            };

            var fpResponse = Builder<FinancialPlanResponse>
                .CreateNew()
                .With(r => r.CaseUrn = matchingFinancialPlan.CaseUrn)
                .With(r => r.Notes = matchingFinancialPlan.Notes)
                .Build();

            var collection = new List<FinancialPlanResponse> { fpResponse };

            _mockGetFinancialPlansByCaseUseCase
                .Setup(x => x.Execute(caseUrn))
                .Returns(collection);

            OkObjectResult controllerResponse = controllerSUT.GetFinancialPlansByCaseId(caseUrn).Result as OkObjectResult;

            var actualResult = controllerResponse.Value as ApiSingleResponseV2<List<FinancialPlanResponse>>;

            actualResult.Data.Should().NotBeNull();
            actualResult.Data.Count.Should().Be(1);
            actualResult.Data.First().CaseUrn.Should().Be(caseUrn);
        }

        [Fact]
        public void GetFinancialPlansById_ReturnsMatchingFinancialPlan_WhenGivenId()
        {
            var fpId = 444;

            var matchingFinancialPlan = new FinancialPlanCase
            {
                Id = fpId,
                Notes = "match"
            };

            var fps = new List<FinancialPlanCase> {
                matchingFinancialPlan,
                new FinancialPlanCase {
                    Id = 222,
                    Notes = "FinancialPlan 2"
                },
                new FinancialPlanCase {
                    Id = 456,
                    Notes = "FinancialPlan 3"
                }
            };

            var fpResponse = Builder<FinancialPlanResponse>
                .CreateNew()
                .With(r => r.Id = matchingFinancialPlan.Id)
                .With(r => r.Notes = matchingFinancialPlan.Notes)
                .Build();

            _mockGetFinancialPlanByIdUseCase
                .Setup(x => x.Execute(fpId))
                .Returns(fpResponse);

            OkObjectResult controllerResponse = controllerSUT.GetFinancialPlanById(fpId).Result as OkObjectResult;

            var actualResult = controllerResponse.Value as ApiSingleResponseV2<FinancialPlanResponse>;

            actualResult.Data.Should().NotBeNull();
            actualResult.Data.Id.Should().Be(fpId);
        }

        [Fact]
        public void PatchFinancialPlan_ReturnsUpdatedFinancialPlan()
        {
            var fpId = 444;
            var newNotes = "new notes xyz";
            var newStatusId = 3;

            var fp = new FinancialPlanCase
            {
                Id = fpId,
                Notes = "Original notes",
                StatusId = 1
            };

            var request = Builder<PatchFinancialPlanRequest>
                .CreateNew()
                .With(r => r.Id = fp.Id)
                .With(r => r.StatusId = newStatusId)
                .With(r => r.Notes = newNotes)
                .Build();

            var fpResponse = Builder<FinancialPlanResponse>
                .CreateNew()
                .With(r => r.Id = request.Id)
                .With(r => r.Notes = request.Notes)
                .With(r => r.StatusId = request.StatusId)
                .Build();

            _mockPatchFinancialPlanUseCase
                .Setup(x => x.Execute(request))
                .Returns(fpResponse);

            OkObjectResult controllerResponse = controllerSUT.Patch(request).Result as OkObjectResult;

            var actualResult = controllerResponse.Value as ApiSingleResponseV2<FinancialPlanResponse>;

            actualResult.Data.Should().NotBeNull();
            actualResult.Data.Id.Should().Be(fpId);
        }

        [Fact]
        public void GetAllStatuses_ReturnsAllStatuses()
        {
            var noOfStatuses = 4;

            var statuses = Builder<FinancialPlanStatus>
                .CreateListOfSize(noOfStatuses)
                .Build()
                .ToList();

            _mockGetAllStatuses
                .Setup(x => x.Execute(null))
                .Returns(statuses);

            OkObjectResult controllerResponse = controllerSUT.GetAllStatuses().Result as OkObjectResult;

            var actualResult = controllerResponse.Value as ApiSingleResponseV2<List<FinancialPlanStatus>>;

            actualResult.Data.Should().NotBeNull();
            actualResult.Data.Count.Should().Be(noOfStatuses);
            actualResult.Data.First().Name.Should().Be(statuses.First().Name);
        }
    }
}