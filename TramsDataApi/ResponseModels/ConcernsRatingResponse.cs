using System;

namespace TramsDataApi.ResponseModels
{
    public class ConcernsRatingResponse
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Urn { get; set; }
    }
}