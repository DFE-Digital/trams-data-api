using System.Collections.Generic;
using System.Linq;
using TramsDataApi.DatabaseModels;

namespace TramsDataApi.Gateways
{
    public interface ITrustGateway
    {
        Group GetGroupByUkPrn(string ukPrn);
        Trust GetIfdTrustByGroupId(string groupId);
        Trust GetIfdTrustByRID(string RID);
        IQueryable<Trust> GetIfdTrustsByTrustRef(string[] trustRefs);
        IList<Group> SearchGroups(int page, int count, string groupName, string ukPrn, string companiesHouseNumber);
    }
}