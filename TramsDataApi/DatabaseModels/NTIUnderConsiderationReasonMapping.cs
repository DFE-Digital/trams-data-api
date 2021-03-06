using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TramsDataApi.DatabaseModels
{
    [Table("NTIUnderConsiderationReasonMapping", Schema = "sdd")]
	public class NTIUnderConsiderationReasonMapping
	{
		[Key]
		public int Id { get; set; }

        public long NTIUnderConsiderationId { get; set; }
		public virtual NTIUnderConsideration NTIUnderConsideration { get; set; }

		public int NTIUnderConsiderationReasonId { get; set; }
		public virtual NTIUnderConsiderationReason NTIUnderConsiderationReason { get; set; }
	}
}

