using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using TramsDataApi.DatabaseModels;
using TramsDataApi.Factories;
using TramsDataApi.Gateways;
using TramsDataApi.ResponseModels;
using TramsDataApi.UseCases;
using Xunit;

namespace TramsDataApi.Test.UseCases
{
    public class GetConcernsCasesByOwnerIdTests
    {
        [Fact]
        public void Execute_ShouldReturnListOfConcernsCaseResponsesForAGivenOwnerId()
        {
            var ownerId = "76832";
            var gateway = new Mock<IConcernsCaseGateway>();
            
            var cases = Builder<ConcernsCase>.CreateListOfSize(10)
                .All()
                .With(c => c.CreatedBy = ownerId)
                .Build();

            gateway.Setup(g =>
                    g.GetConcernsCasesByOwnerId(It.IsAny<string>(), null, 1, 50))
                .Returns(cases);

            var expected = cases.Select(ConcernsCaseResponseFactory.Create).ToList();

            var usecase = new GetConcernsCasesByOwnerId(gateway.Object);

            var result = usecase.Execute(ownerId, null, 1, 50);
            result.Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void Execute_ShouldReturnListOfConcernsCaseResponsesFilteredByStatusWhenSupplied()
        {
            var ownerId = "564372";
            var statusUrn = 123;
            var gateway = new Mock<IConcernsCaseGateway>();
            
            var cases = Builder<ConcernsCase>.CreateListOfSize(5)
                .All()
                .With(c => c.CreatedBy = ownerId)
                .With(c => c.StatusUrn = statusUrn)
                .Build();

            gateway.Setup(g =>
                    g.GetConcernsCasesByOwnerId(It.IsAny<string>(), statusUrn, 1, 50))
                .Returns(cases);

            var expected = cases.Select(ConcernsCaseResponseFactory.Create).ToList();

            var usecase = new GetConcernsCasesByOwnerId(gateway.Object);

            var result = usecase.Execute(ownerId, statusUrn, 1, 50);
            result.Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void Execute_ShouldReturnEmptyListWhenNoConcernsCasesArefound()
        {
            var ownerId = "96784";
            var statusUrn = 567;
            var gateway = new Mock<IConcernsCaseGateway>();
            

            gateway.Setup(g =>
                    g.GetConcernsCasesByOwnerId(It.IsAny<string>(), statusUrn, 1, 50))
                .Returns(new List<ConcernsCase>());
            

            var usecase = new GetConcernsCasesByOwnerId(gateway.Object);

            var result = usecase.Execute(ownerId, statusUrn, 1, 50);
            result.Should().BeEquivalentTo(new List<ConcernsCaseResponse>());
        }
    }
}