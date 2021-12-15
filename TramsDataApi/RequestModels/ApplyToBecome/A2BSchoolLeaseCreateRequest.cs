namespace TramsDataApi.RequestModels.ApplyToBecome
{
    public class A2BSchoolLeaseCreateRequest
    {
        public string SchoolLeaseId {get; set;}
        public string SchoolLeaseTerm {get; set;}
        public string SchoolLeaseRepaymentValue {get; set;}
        public string SchoolLeaseInterestRate {get; set;}
        public string SchoolLeasePaymentToDate {get; set;}
        public string SchoolLeasePurpose {get; set;}
        public string SchoolLeaseValueOfAssets {get; set;}
        public string SchoolLeaseResponsibleForAssets {get; set;}
    }
}