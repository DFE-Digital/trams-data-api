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
    public class UpdateConcernsCaseTests
    {
        [Fact]
        public void ShouldSaveAConcernsCase_WhenGivenAModelToUpdate()
        {
            var urn = 1234;
            var gateway = new Mock<IConcernsCaseGateway>();
            var concernsCase = Builder<ConcernsCase>.CreateNew().With(c => c.Urn = urn).Build();
            var updateRequest = Builder<ConcernCaseRequest>.CreateNew().Build();

            var concernsToUpdate = ConcernsCaseFactory.Update(concernsCase, updateRequest);
            
            gateway.Setup(g => g.GetConcernsCaseByUrn(urn)).Returns(concernsCase);
            gateway.Setup(g => g.Update(It.IsAny<ConcernsCase>())).Returns(concernsToUpdate);

            var expected = ConcernsCaseResponseFactory.Create(concernsToUpdate);
            
            var useCase = new UpdateConcernsCase(gateway.Object);
            var result = useCase.Execute(urn, updateRequest);

            result.Should().BeEquivalentTo(expected);
        }
    }
}