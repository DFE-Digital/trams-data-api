namespace TramsDataApi.RequestModels
{
    public class AcademyTransferProjectFeaturesRequest
    {
        public string WhoInitiatedTheTransfer { get; set; }
        public bool RddOrEsfaIntervention { get; set; }
        public string RddOrEsfaInterventionDetail { get; set; }
        public string TypeOfTransfer { get; set; }
        public string OtherTransferTypeDescription { get; set; }
    }
}