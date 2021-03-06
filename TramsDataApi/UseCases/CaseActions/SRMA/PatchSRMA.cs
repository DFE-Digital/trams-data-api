using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TramsDataApi.Factories.CaseActionFactories;
using TramsDataApi.Gateways;
using TramsDataApi.RequestModels.CaseActions.SRMA;
using TramsDataApi.ResponseModels.CaseActions.SRMA;

namespace TramsDataApi.UseCases.CaseActions
{
    public class PatchSRMA : IUseCase<PatchSRMARequest, SRMAResponse>
    {
        private readonly ISRMAGateway _gateway;

        public PatchSRMA(ISRMAGateway gateway)
        {
            _gateway = gateway;
        }

        public SRMAResponse Execute(PatchSRMARequest request)
        {
            return ExecuteAsync(request).Result;
        }

        public async Task<SRMAResponse> ExecuteAsync(PatchSRMARequest request)
        {
            if (request?.Delegate == null || request.SRMAId == default(int))
            {
                throw new ArgumentNullException("SRMA Id or delegate not provided");
            }

            var patchedSRMA = await _gateway.PatchSRMAAsync(request.SRMAId, request.Delegate);
            return SRMAFactory.CreateResponse(patchedSRMA);
        }
    }
}
