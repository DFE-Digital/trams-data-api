using System.Collections.Generic;

namespace TramsDataApi.ResponseModels
{
    public class TrustSummaryResponse
    {
        public string Urn { get; set; }
        public string GroupName { get; set; }
        public string CompaniesHouseNumber { get; set; }
        public List<EstablishmentSummaryResponse> Establishments { get; set; }
    }
}