using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TramsDataApi.DatabaseModels;
using TramsDataApi.Factories.CaseActionFactories;
using TramsDataApi.Gateways;
using TramsDataApi.UseCases.CaseActions;
using Xunit;


namespace TramsDataApi.Test.UseCases
{
    public class GetSRMAByIdTests
    {
        [Fact]
        public void GetSRMAById_ShouldReturnSRMAResponse_WhenGivenSRMAId()
        {
            var srmaId = 123;

            var matchingSRMA = new SRMACase
            {
                Id = srmaId,
                Notes = "match"
            };

            var srmas = new List<SRMACase> {
                matchingSRMA,
                new SRMACase {
                    Id = 222,
                    Notes = "SRMA 1"
                },
                new SRMACase {
                    Id = 456,
                    Notes = "SRMA 2"
                }
            };

            var expectedResult = SRMAFactory.CreateResponse(matchingSRMA);

            var mockSRMAGateway = new Mock<ISRMAGateway>();
            mockSRMAGateway.Setup(g => g.GetSRMAById(srmaId)).Returns(Task.FromResult(srmas.Single(s => s.Id == srmaId)));

            var useCase = new GetSRMAById(mockSRMAGateway.Object);

            var result = useCase.Execute(srmaId);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }

    }
}
