using System;
using System.Collections.Generic;

namespace TramsDataApi.DatabaseModels
{
    public class ConcernsType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Urn { get; set; }
        public virtual ICollection<ConcernsRecord> FkConcernsRecord { get; set; }
    }
}