using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TramsDataApi.DatabaseModels;

namespace TramsDataApi.RequestModels.CaseActions.NTI.WarningLetter
{
    public class PatchNTIWarningLetterRequest
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public int CaseUrn { get; set; }
        public DateTime? DateLetterSent { get; set; }
        public string Notes { get; set; }
        public int? StatusId { get; set; }

        public ICollection<int> WarningLetterReasonsMapping { get; set; }
        public ICollection<int> WarningLetterConditionsMapping { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public int? ClosedStatusId { get; set; }
    }
}
