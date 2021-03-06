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
    public class GetConcernsRecordsByCaseUrnTests
    {
        [Fact]
        public void Execute_ShouldReturnListOfConcernsRecordResponsesForAGivenCaseUrn()
        {
            var caseUrn = 1234;
            var concernsCase = Builder<ConcernsCase>.CreateNew()
                .With(c => c.Urn = caseUrn)
                .Build();
            
            var concernsType = Builder<ConcernsType>.CreateNew().Build();
            var concernsRating = Builder<ConcernsRating>.CreateNew().Build();
            
            var concernsRecords = Builder<ConcernsRecord>.CreateListOfSize(5)
                .All()
                .With(r => r.ConcernsCase = concernsCase)
                .With(r => r.ConcernsType = concernsType)
                .With(r => r.ConcernsRating = concernsRating)
                .Build();

            concernsCase.ConcernsRecords = concernsRecords;
            
            var gateway = new Mock<IConcernsCaseGateway>();
            gateway.Setup(g => g.GetConcernsCaseIncludingRecordsByUrn(caseUrn))
                .Returns(concernsCase);

            var useCase = new GetConcernsRecordsByCaseUrn(gateway.Object);
            var expected = concernsRecords
                .Select(ConcernsRecordResponseFactory.Create).ToList();
            
            var result = useCase.Execute(caseUrn);

            result.Should().BeEquivalentTo(expected);
        }
    }
}