namespace TramsDataApi.RequestModels
{
    public class AcademyTransferProjectBenefitsRequest
    {
        public IntendedTransferBenefitRequest IntendedTransferBenefits { get; set; }
        public OtherFactorsToConsiderRequest OtherFactorsToConsider { get; set; }
    }
}