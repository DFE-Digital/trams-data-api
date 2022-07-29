namespace TramsDataApi.ResponseModels
{
    public class AcademyTransferProjectDatesResponse
    {
        public string TransferFirstDiscussed { get; set; }
        public string TargetDateForTransfer { get; set; }
        public string HtbDate { get; set; }
        public bool? HasTransferFirstDiscussedDate { get; set; }
        public bool? HasTargetDateForTransfer { get; set; }
        public bool? HasHtbDate { get; set; }
    }
}