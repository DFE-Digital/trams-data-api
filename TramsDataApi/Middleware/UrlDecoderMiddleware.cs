using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;

namespace TramsDataApi.Middleware
{
    public class UrlDecoderMiddleware
    {
        private readonly RequestDelegate _next;
        
        public UrlDecoderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var queryString = context.Request.QueryString.ToString();
            var decodedQueryString = HttpUtility.UrlDecode(queryString);
            var newQuery = QueryHelpers.ParseQuery(decodedQueryString);
            var items = newQuery
                .SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();
            var qb = new QueryBuilder(items);
            context.Request.QueryString = qb.ToQueryString();
            
            await _next(context);
        }
    }
}