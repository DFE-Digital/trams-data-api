using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TramsDataApi.DatabaseModels
{
    [Table("FinancialPlanStatus", Schema = "sdd")]
    public class FinancialPlanStatus
    {
        [Key]
        public long Id { get; set;  }
        [StringLength(255)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set;  }
        public DateTime UpdatedAt { get; set; }
    }
}
