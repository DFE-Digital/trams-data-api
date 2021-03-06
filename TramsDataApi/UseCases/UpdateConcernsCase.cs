using TramsDataApi.Factories;
using TramsDataApi.Gateways;
using TramsDataApi.RequestModels;
using TramsDataApi.ResponseModels;

namespace TramsDataApi.UseCases
{
    public class UpdateConcernsCase : IUpdateConcernsCase
    {
        private readonly IConcernsCaseGateway _concernsCaseGateway;
        
        public UpdateConcernsCase(
            IConcernsCaseGateway concernsCaseGateway)
        {
            _concernsCaseGateway = concernsCaseGateway;
        }
        
        public ConcernsCaseResponse Execute(int urn, ConcernCaseRequest request)
        {
            var currentConcernsCase = _concernsCaseGateway.GetConcernsCaseByUrn(urn);
            var concernsCaseToUpdate = ConcernsCaseFactory.Update(currentConcernsCase, request);
            var updatedConcernsCase = _concernsCaseGateway.Update(concernsCaseToUpdate);
            return ConcernsCaseResponseFactory.Create(updatedConcernsCase);
        }
    }
}