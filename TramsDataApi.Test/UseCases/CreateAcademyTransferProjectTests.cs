using System;
using System.Collections.Generic;
using System.Globalization;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using TramsDataApi.DatabaseModels;
using TramsDataApi.Factories;
using TramsDataApi.Gateways;
using TramsDataApi.RequestModels;
using TramsDataApi.UseCases;
using Xunit;

namespace TramsDataApi.Test.UseCases
{
    public class CreateAcademyTransferProjectTests 
    {
        [Fact]
        public void ShouldCreateAndReturnAnAcademyTransferProject_WhenGivenACreateOrUpdateAcademyTransferProjectRequest()
        {
            var randomGenerator = new RandomGenerator();

            var gateway = new Mock<IAcademyTransferProjectGateway>();
            var benefitsRequest = Builder<AcademyTransferProjectBenefitsRequest>.CreateNew()
                .With(b => b.IntendedTransferBenefits = Builder<IntendedTransferBenefitRequest>.CreateNew()
                    .With(i => i.SelectedBenefits =  new List<string>()).Build())
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
                .Build();

            var createRequest = Builder<AcademyTransferProjectRequest>.CreateNew()
                .With(c => c.Benefits = benefitsRequest)
                .With(c => c.Dates = datesRequest)
                .With(c => c.Rationale = Builder<AcademyTransferProjectRationaleRequest>.CreateNew().Build())
                .With(c => c.GeneralInformation = Builder<AcademyTransferProjectGeneralInformationRequest>.CreateNew().Build())
                .With(c => c.Features = Builder<AcademyTransferProjectFeaturesRequest>.CreateNew().Build())
                .With(c => c.TransferringAcademies = (List<TransferringAcademiesRequest>) Builder<TransferringAcademiesRequest>
                    .CreateListOfSize(5).Build())
                .Build();

            var convertedAcademyTransferProject = AcademyTransferProjectFactory.Create(createRequest);
            var createdAcademyTransferProject = AcademyTransferProjectFactory.Create(createRequest);
            createdAcademyTransferProject.Urn = randomGenerator.Int();

            var expected = AcademyTransferProjectResponseFactory.Create(createdAcademyTransferProject);
        
            gateway.Setup(g => g.SaveAcademyTransferProject(It.IsAny<AcademyTransferProjects>())).Returns(createdAcademyTransferProject); 
            
        
            var useCase = new CreateAcademyTransferProject(gateway.Object);
            var result = useCase.Execute(createRequest);
            
            result.Should().BeEquivalentTo(expected);
        }
    }
}