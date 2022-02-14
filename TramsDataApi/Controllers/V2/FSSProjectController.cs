﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Json;
using TramsDataApi.ResponseModels;
using TramsDataApi.UseCases;

namespace TramsDataApi.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("v{version:apiVersion}/fss/")]
    public class FssProjectController : Controller
    {
        private readonly ILogger<FssProjectController> _logger;
        private readonly IGetAllFssProject _getAllFssProject;

        public FssProjectController(ILogger<FssProjectController> logger, IGetAllFssProject getAllFssProject)
        {
            _logger = logger;
            _getAllFssProject = getAllFssProject;
        }

        [HttpGet("projects")]
        [MapToApiVersion("2.0")]
        public ActionResult<ApiResponseV2<FssProjectResponse>> GetAll()
        {
            _logger.LogInformation($"Retreiving all FSS Projects ");

            var projects = _getAllFssProject.Execute().ToList();

            _logger.LogInformation($"Found {0} projects, " , projects.Count);

            _logger.LogDebug(JsonSerializer.Serialize(projects));

            // Since the data sent is not being paginated passing null in the pagingResponse parameter
            var response = new ApiResponseV2<FssProjectResponse>(projects.ToList(), null);
            return new OkObjectResult(response);
        }
    }
}
