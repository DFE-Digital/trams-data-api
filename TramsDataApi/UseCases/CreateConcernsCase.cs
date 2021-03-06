using TramsDataApi.Factories;
using TramsDataApi.Gateways;
using TramsDataApi.RequestModels;
using TramsDataApi.ResponseModels;

namespace TramsDataApi.UseCases
{
    public class CreateConcernsCase : ICreateConcernsCase
    {
        private readonly IConcernsCaseGateway _concernsCaseGateway;

        public CreateConcernsCase(IConcernsCaseGateway concernsCaseGateway)
        {
            _concernsCaseGateway = concernsCaseGateway;
        }

        public ConcernsCaseResponse Execute(ConcernCaseRequest request)
        {
            var concernsCaseToCreate = ConcernsCaseFactory.Create(request);
            var createdConcernsCase = _concernsCaseGateway.SaveConcernsCase(concernsCaseToCreate);
            return ConcernsCaseResponseFactory.Create(createdConcernsCase);
        }
    }
}