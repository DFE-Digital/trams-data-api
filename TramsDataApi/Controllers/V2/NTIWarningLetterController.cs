using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TramsDataApi.DatabaseModels;
using TramsDataApi.RequestModels.CaseActions.NTI.WarningLetter;
using TramsDataApi.ResponseModels;
using TramsDataApi.ResponseModels.CaseActions.NTI.WarningLetter;
using TramsDataApi.UseCases;

namespace TramsDataApi.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("v{version:apiVersion}/case-actions/nti-warning-letter")]
    [ApiController]
    public class NTIWarningLetterController : Controller
    {
        private readonly ILogger<NTIWarningLetterController> _logger;
        private readonly IUseCase<CreateNTIWarningLetterRequest, NTIWarningLetterResponse> _createNtiWarningLetterUseCase;
        private readonly IUseCase<long, NTIWarningLetterResponse> _getNtiWarningLetterByIdUseCase;
        private readonly IUseCase<int, List<NTIWarningLetterResponse>> _getNtiWarningLetterByCaseUrnUseCase;
        private readonly IUseCase<PatchNTIWarningLetterRequest, NTIWarningLetterResponse> _patchNTIWarningLetterUseCase;
        private readonly IUseCase<object, List<NTIWarningLetterStatus>> _getAllStatuses;
        private readonly IUseCase<object, List<NTIWarningLetterReason>> _getAllReasons;
        private readonly IUseCase<object, List<NTIWarningLetterCondition>> _getAllConditions;
        private readonly IUseCase<object, List<NTIWarningLetterConditionType>> _getAllConditionTypes;

        public NTIWarningLetterController(ILogger<NTIWarningLetterController> logger,
            IUseCase<CreateNTIWarningLetterRequest, NTIWarningLetterResponse> createNtiWarningLetterUseCase,
            IUseCase<long, NTIWarningLetterResponse> getNtiWarningLetterByIdUseCase,
            IUseCase<int, List<NTIWarningLetterResponse>> getNtiWarningLetterByCaseUrnUseCase,
            IUseCase<PatchNTIWarningLetterRequest, NTIWarningLetterResponse> patchNTIWarningLetterUseCase,
            IUseCase<object, List<NTIWarningLetterStatus>> getAllStatuses,
            IUseCase<object, List<NTIWarningLetterReason>> getAllReasons,
            IUseCase<object, List<NTIWarningLetterCondition>> getAllConditions,
            IUseCase<object, List<NTIWarningLetterConditionType>> getAllConditionTypes
            )
        {
            _logger = logger;
            _createNtiWarningLetterUseCase = createNtiWarningLetterUseCase;
            _getNtiWarningLetterByIdUseCase = getNtiWarningLetterByIdUseCase;
            _getNtiWarningLetterByCaseUrnUseCase = getNtiWarningLetterByCaseUrnUseCase;

            _getAllStatuses = getAllStatuses;
            _getAllReasons = getAllReasons;
            _getAllConditions = getAllConditions;
            _patchNTIWarningLetterUseCase = patchNTIWarningLetterUseCase;
            _getAllConditionTypes = getAllConditionTypes;
        }

        [HttpPost]
        [MapToApiVersion("2.0")]
        public ActionResult<ApiSingleResponseV2<NTIWarningLetterResponse>> Create(CreateNTIWarningLetterRequest request)
        {
            var createdWarningLetter = _createNtiWarningLetterUseCase.Execute(request);
            var response = new ApiSingleResponseV2<NTIWarningLetterResponse>(createdWarningLetter);

            return CreatedAtAction(nameof(GetNTIWarningLetterById), new { warningLetterId = createdWarningLetter.Id}, response);
        }

        [HttpGet]
        [Route("{warningLetterId}")]
        [MapToApiVersion("2.0")]
        public ActionResult<ApiSingleResponseV2<NTIWarningLetterResponse>> GetNTIWarningLetterById(long warningLetterId)
        {
            var warningLetter = _getNtiWarningLetterByIdUseCase.Execute(warningLetterId);
            var response = new ApiSingleResponseV2<NTIWarningLetterResponse>(warningLetter);

            return Ok(response);
        }

        [HttpGet]
        [Route("case/{caseUrn}")]
        [MapToApiVersion("2.0")]
        public ActionResult<ApiSingleResponseV2<List<NTIWarningLetterResponse>>> GetNtiWarningLetterByCaseUrn(int caseUrn)
        {
            var warningLetters = _getNtiWarningLetterByCaseUrnUseCase.Execute(caseUrn);
            var response = new ApiSingleResponseV2<List<NTIWarningLetterResponse>>(warningLetters);

            return Ok(response);
        }

        [HttpPatch]
        [MapToApiVersion("2.0")]
        public ActionResult<ApiSingleResponseV2<NTIWarningLetterResponse>> Patch(PatchNTIWarningLetterRequest request)
        {
            var createdWarningLetter = _patchNTIWarningLetterUseCase.Execute(request);
            var response = new ApiSingleResponseV2<NTIWarningLetterResponse>(createdWarningLetter);

            return Ok(response);
        }

        [HttpGet]
        [Route("all-statuses")]
        [MapToApiVersion("2.0")]
        public ActionResult<ApiSingleResponseV2<List<NTIWarningLetterStatus>>> GetAllStatuses()
        {
            var statuses = _getAllStatuses.Execute(null);
            var response = new ApiSingleResponseV2<List<NTIWarningLetterStatus>>(statuses);

            return Ok(response);
        }

        [HttpGet]
        [Route("all-reasons")]
        [MapToApiVersion("2.0")]
        public ActionResult<ApiSingleResponseV2<List<NTIWarningLetterReason>>> GetAllReasons()
        {
            var reasons = _getAllReasons.Execute(null);
            var response = new ApiSingleResponseV2<List<NTIWarningLetterReason>>(reasons);

            return Ok(response);
        }

        [HttpGet]
        [Route("all-conditions")]
        [MapToApiVersion("2.0")]
        public ActionResult<ApiSingleResponseV2<List<NTIWarningLetterCondition>>> GetAllConditions()
        {
            var conditions = _getAllConditions.Execute(null);
            var response = new ApiSingleResponseV2<List<NTIWarningLetterCondition>>(conditions);

            return Ok(response);
        }

        [HttpGet]
        [Route("all-condition-types")]
        [MapToApiVersion("2.0")]
        public ActionResult<ApiSingleResponseV2<List<NTIWarningLetterConditionType>>> GetAllConditionTypes()
        {
            var conditionTypes = _getAllConditionTypes.Execute(null);
            var response = new ApiSingleResponseV2<List<NTIWarningLetterConditionType>>(conditionTypes);

            return Ok(response);
        }
    }
}
