using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TramsDataApi.RequestModels;
using TramsDataApi.ResponseModels;
using TramsDataApi.UseCases;

namespace TramsDataApi.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("v{version:apiVersion}/concerns-cases")]
    public class ConcernsCaseController: ControllerBase
    { 
        private readonly ILogger<ConcernsCaseController> _logger;
        private readonly ICreateConcernsCase _createConcernsCase;
        private readonly IGetConcernsCaseByUrn _getConcernsCaseByUrn;
        private readonly IGetConcernsCaseByTrustUkprn _getConcernsCaseByTrustUkprn;

        public ConcernsCaseController(
            ILogger<ConcernsCaseController> logger, 
            ICreateConcernsCase createConcernsCase,
            IGetConcernsCaseByUrn getConcernsCaseByUrn,
            IGetConcernsCaseByTrustUkprn getConcernsCaseByTrustUkprn)
        {
            _logger = logger;
            _createConcernsCase = createConcernsCase;
            _getConcernsCaseByUrn = getConcernsCaseByUrn;
            _getConcernsCaseByTrustUkprn = getConcernsCaseByTrustUkprn;
        }
        
        [HttpPost]
        [MapToApiVersion("2.0")]
        public ActionResult<ApiResponseV2<ConcernsCaseResponse>> Create(ConcernCaseRequest request)
        {
            var createdConcernsCase = _createConcernsCase.Execute(request);
            var response = new ApiResponseV2<ConcernsCaseResponse>(createdConcernsCase);
            
            return new ObjectResult(response) {StatusCode = StatusCodes.Status201Created};
        }
        
        [HttpGet]
        [Route("urn/{urn}")]
        [MapToApiVersion("2.0")]
        public ActionResult<ApiResponseV2<ConcernsCaseResponse>> GetByUrn(string urn)
        {
            _logger.LogInformation($"Attempting to get Concerns Case by Urn {urn}");
            var concernsCase = _getConcernsCaseByUrn.Execute(urn);
            
            if (concernsCase == null)
            {
                _logger.LogInformation($"No Concerns case found for URN {urn}");
                return NotFound();
            }
            
            _logger.LogInformation($"Returning Concerns case with Urn {urn}");
            _logger.LogDebug(JsonSerializer.Serialize(concernsCase));
            var response = new ApiResponseV2<ConcernsCaseResponse>(concernsCase);
            
            return Ok(response);
        }
        
        [HttpGet]
        [Route("ukprn/{trustUkprn}")]
        [MapToApiVersion("2.0")]
        public ActionResult<ApiResponseV2<ConcernsCaseResponse>> GetByTrustUkprn(string trustUkprn)
        {
            _logger.LogInformation($"Attempting to get Concerns Case by Trust Ukprn {trustUkprn}");
            var concernsCases = _getConcernsCaseByTrustUkprn.Execute(trustUkprn);

            _logger.LogInformation($"Returning Concerns case with Trust Ukprn {trustUkprn}");
            _logger.LogDebug(JsonSerializer.Serialize(concernsCases));
            var response = new ApiResponseV2<ConcernsCaseResponse>(concernsCases, null);
            
            return Ok(response);
        }
    }
}