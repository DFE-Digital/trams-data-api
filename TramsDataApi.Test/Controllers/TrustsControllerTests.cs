using System.Collections.Generic;
using FizzWare.NBuilder;
using FizzWare.NBuilder.PropertyNaming;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TramsDataApi.Controllers;
using TramsDataApi.ResponseModels;
using TramsDataApi.UseCases;
using Xunit;

namespace TramsDataApi.Test.Controllers
{
    public class TrustsControllerTests
    {
        public TrustsControllerTests()
        {
            BuilderSetup.SetDefaultPropertyName(new RandomValuePropertyNamer(new BuilderSettings()));
        }
        
        [Fact]
        public void GetTrustsByUkPrn_ReturnsNotFoundResult_WhenNoTrustsFound()
        {
            var getTrustsByUkprn = new Mock<IGetTrustByUkprn>();
            var ukprn = "mockukprn";
            getTrustsByUkprn.Setup(g => g.Execute(ukprn)).Returns(() => null);

            var controller = new TrustsController(getTrustsByUkprn.Object, new Mock<ISearchTrusts>().Object);
            var result = controller.GetTrustByUkprn(ukprn);

            result.Result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public void GetTrustsByUkprn_ReturnsTrustResponse_WhenTrustFound()
        {
            var ukprn = "mockukprn";

            var trustResponse = new TrustResponse
            {
                IfdData = new IFDDataResponse(),
                GiasData = new GIASDataResponse
                {
                    GroupId = "Test group ID",
                    GroupName = "Test group name",
                    CompaniesHouseNumber = "Test CH Number",
                    GroupContactAddress = new AddressResponse
                    {
                        Street = "Test street",
                        AdditionalLine = "Test additional line",
                        Locality = "Test locality",
                        Town = "Test town",
                        County = "Test county",
                        Postcode = "P05TC0D"
                    },
                    Ukprn = ukprn
                }
            };
            var getTrustByUkprn = new Mock<IGetTrustByUkprn>();
            getTrustByUkprn.Setup(g => g.Execute(ukprn)).Returns(trustResponse);
            
            var controller = new TrustsController(getTrustByUkprn.Object, new Mock<ISearchTrusts>().Object);
            var result = controller.GetTrustByUkprn(ukprn);

            result.Result.Should().BeEquivalentTo(new OkObjectResult(trustResponse));
        }
        
        [Fact]
        public void SearchTrusts_ReturnsEmptySetOfTrustListItems_WhenNoTrustsFound()
        {
            var groupName = "Mockgroupname";
            var urn = "Mockurn";
            var companiesHouseNumber = "Mockcompanieshousenumber";

            var searchTrusts = new Mock<ISearchTrusts>();
            searchTrusts.Setup(s => s.Execute(groupName, urn, companiesHouseNumber))
                .Returns(new List<TrustListItemResponse>());

            var controller = new TrustsController(new Mock<IGetTrustByUkprn>().Object, searchTrusts.Object);
            var result = controller.SearchTrusts(groupName, urn, companiesHouseNumber);

            result.Result.Should().BeEquivalentTo(new OkObjectResult(new List<TrustListItemResponse>()));
        }

        [Fact]
        public void SearchTrusts_ByGroupNameAndCompaniesHouseNumber_ReturnsListOfTrustListItems_WhenTrustsAreFound()
        {
            var groupName = "Mockgroupname";
            var companiesHouseNumber = "Mockcompanieshousenumber";

            var expectedTrustListItems = Builder<TrustListItemResponse>.CreateListOfSize(5)
                .All()
                .With(g => g.GroupName = groupName)
                .With(g => g.CompaniesHouseNumber = companiesHouseNumber)
                .Build();
            
            var searchTrusts = new Mock<ISearchTrusts>();
            searchTrusts.Setup(s => s.Execute(groupName, null, companiesHouseNumber))
                .Returns(expectedTrustListItems);

            var controller = new TrustsController(new Mock<IGetTrustByUkprn>().Object, searchTrusts.Object);
            var result = controller.SearchTrusts(groupName, null, companiesHouseNumber);

            result.Result.Should().BeEquivalentTo(new OkObjectResult(expectedTrustListItems));
        }
        
        
        [Fact]
        public void SearchTrusts_ByUrn_ReturnsListOfTrustListItems_WhenTrustsAreFound()
        {
            var urn = "Mockurn";

            var expectedTrustListItems = Builder<TrustListItemResponse>.CreateListOfSize(5)
                .All()
                .With(g => g.Urn = urn)
                .Build();
            
            var searchTrusts = new Mock<ISearchTrusts>();
            searchTrusts.Setup(s => s.Execute(null, urn, null))
                .Returns(expectedTrustListItems);

            var controller = new TrustsController(new Mock<IGetTrustByUkprn>().Object, searchTrusts.Object);
            var result = controller.SearchTrusts(null, urn, null);

            result.Result.Should().BeEquivalentTo(new OkObjectResult(expectedTrustListItems));
        }

        [Fact]
        public void SearchTrusts_WithNoParams_ReturnsAllTrusts()
        {
            var expectedTrustListItems = Builder<TrustListItemResponse>.CreateListOfSize(5).Build();
            
            var searchTrusts = new Mock<ISearchTrusts>();
            searchTrusts.Setup(s => s.Execute(null, null, null))
                .Returns(expectedTrustListItems);

            var controller = new TrustsController(new Mock<IGetTrustByUkprn>().Object, searchTrusts.Object);
            var result = controller.SearchTrusts(null, null, null);

            result.Result.Should().BeEquivalentTo(new OkObjectResult(expectedTrustListItems));
        }
    }
}

