using System.Globalization;
using System.Linq;
using TramsDataApi.DatabaseModels;
using TramsDataApi.ResponseModels;

namespace TramsDataApi.Factories
{
    public class AcademyTransferProjectResponseFactory
    {
        public static AcademyTransferProjectResponse Create(AcademyTransferProjects model)
        {
            if (model == null)
            {
                return null;
            }

            var transferringAcademies = model.TransferringAcademies
                .Select(a => new TransferringAcademiesResponse
                {
                    IncomingTrustUkprn = a.IncomingTrustUkprn,
                    OutgoingAcademyUkprn = a.OutgoingAcademyUkprn,
                    PupilNumbersAdditionalInformation = a.PupilNumbersAdditionalInformation,
                    LatestOfstedReportAdditionalInformation = a.LatestOfstedReportAdditionalInformation,
                    KeyStage2PerformanceAdditionalInformation = a.KeyStage2PerformanceAdditionalInformation,
                    KeyStage4PerformanceAdditionalInformation = a.KeyStage4PerformanceAdditionalInformation,
                    KeyStage5PerformanceAdditionalInformation = a.KeyStage5PerformanceAdditionalInformation
                })
                .ToList();

            var features = new AcademyTransferProjectFeaturesResponse
            {
                WhoInitiatedTheTransfer = model.WhoInitiatedTheTransfer,
                RddOrEsfaIntervention = model.RddOrEsfaIntervention,
                RddOrEsfaInterventionDetail = model.RddOrEsfaInterventionDetail,
                TypeOfTransfer = model.TypeOfTransfer,
                OtherTransferTypeDescription = model.OtherTransferTypeDescription,
                IsCompleted = model.FeatureSectionIsCompleted
            };

            var dates = new AcademyTransferProjectDatesResponse
            {
                TransferFirstDiscussed =
                    model.TransferFirstDiscussed?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                TargetDateForTransfer =
                    model.TargetDateForTransfer?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                HtbDate = model.HtbDate?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                HasHtbDate = model.HasHtbDate,
                HasTargetDateForTransfer = model.HasTargetDateForTransfer,
                HasTransferFirstDiscussedDate = model.HasTransferFirstDiscussedDate
            };

            var intendedTransferBenefits = new IntendedTransferBenefitResponse
            {
                OtherBenefitValue = model.OtherBenefitValue,
                SelectedBenefits = model.AcademyTransferProjectIntendedTransferBenefits?
                    .Select(b => b.SelectedBenefit).ToList()
            };

            var otherFactorsToConsider = new OtherFactorsToConsiderResponse
            {
                HighProfile = new BenefitConsideredFactorResponse
                {
                    ShouldBeConsidered = model.HighProfileShouldBeConsidered,
                    FurtherSpecification = model.HighProfileFurtherSpecification
                },
                ComplexLandAndBuilding = new BenefitConsideredFactorResponse
                {
                    ShouldBeConsidered = model.ComplexLandAndBuildingShouldBeConsidered,
                    FurtherSpecification = model.ComplexLandAndBuildingFurtherSpecification
                },
                FinanceAndDebt = new BenefitConsideredFactorResponse
                {
                    ShouldBeConsidered = model.FinanceAndDebtShouldBeConsidered,
                    FurtherSpecification = model.FinanceAndDebtFurtherSpecification
                },
                OtherRisks = new BenefitConsideredFactorResponse
                {
                    ShouldBeConsidered = model.OtherRisksShouldBeConsidered,
                    FurtherSpecification = model.OtherRisksFurtherSpecification
                }
            };

            return new AcademyTransferProjectResponse
            {
                ProjectUrn = model.Urn.ToString(),
                ProjectReference = model.ProjectReference,
                OutgoingTrustUkprn = model.OutgoingTrustUkprn,
                TransferringAcademies = transferringAcademies,
                Features = features,
                Dates = dates,
                Benefits = new AcademyTransferProjectBenefitsResponse
                {
                    IntendedTransferBenefits = intendedTransferBenefits,
                    OtherFactorsToConsider = otherFactorsToConsider,
                    IsCompleted = model.BenefitsSectionIsCompleted,
                    AnyRisks = model.AnyRisks
                },
                Rationale = new AcademyTransferProjectRationaleResponse
                {
                    ProjectRationale = model.ProjectRationale,
                    TrustSponsorRationale = model.TrustSponsorRationale,
                    IsCompleted = model.RationaleSectionIsCompleted
                },
                GeneralInformation = new AcademyTransferProjectGeneralInformationResponse
                {
                    Author = model.Author,
                    Recommendation = model.Recommendation
                },
                State = model.State,
                Status = model.Status
            };
        }
    }
}