using System.Collections.Generic;
using System.IO.Compression;
using TramsDataApi.DatabaseModels;
using TramsDataApi.ResponseModels;

namespace TramsDataApi.UseCases
{
    public interface ISearchTrusts
    {
        public IList<TrustListItemResponse> Execute(string groupName, string urn, string companiesHouseNumber);
    }
}