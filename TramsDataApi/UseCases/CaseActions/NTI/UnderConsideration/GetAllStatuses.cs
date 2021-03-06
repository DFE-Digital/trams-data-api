using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TramsDataApi.DatabaseModels;
using TramsDataApi.Gateways;

namespace TramsDataApi.UseCases.CaseActions.NTI.UnderConsideration
{
    public class GetAllStatuses : IUseCase<Object, List<NTIUnderConsiderationStatus>>
    {
        private readonly INTIUnderConsiderationGateway _gateway;

        public GetAllStatuses(INTIUnderConsiderationGateway gateway)
        {
            _gateway = gateway;
        }

        public List<NTIUnderConsiderationStatus> Execute(Object ignore)
        {
            return ExecuteAsync(ignore).Result;
        }

        public async Task<List<NTIUnderConsiderationStatus>> ExecuteAsync(Object ignore)
        {
            return await _gateway.GetAllStatuses();
        }
    }
}
