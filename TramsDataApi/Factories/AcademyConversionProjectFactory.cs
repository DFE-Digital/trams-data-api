using System;
using TramsDataApi.DatabaseModels;
using TramsDataApi.RequestModels.AcademyConversionProject;

namespace TramsDataApi.Factories
{
    public class AcademyConversionProjectFactory
    {
        public static AcademyConversionProject Update(AcademyConversionProject project, UpdateAcademyConversionProjectRequest updateRequest)
        {
            if (updateRequest == null)
            {
                return project;
            }

            project.HeadTeacherBoardDate = updateRequest.HeadTeacherBoardDate == default(DateTime)
                ? null
                : updateRequest.HeadTeacherBoardDate ?? project.HeadTeacherBoardDate;
            project.Author = updateRequest.Author ?? project.Author;
            project.ClearedBy = updateRequest.ClearedBy ?? project.ClearedBy;
            project.ProposedAcademyOpeningDate = updateRequest.ProposedAcademyOpeningDate ?? project.ProposedAcademyOpeningDate;
            project.PublishedAdmissionNumber = updateRequest.PublishedAdmissionNumber ?? project.PublishedAdmissionNumber;
            project.ViabilityIssues = updateRequest.ViabilityIssues ?? project.ViabilityIssues;
            project.FinancialDeficit = updateRequest.FinancialDeficit ?? project.FinancialDeficit;
            project.RationaleForProject = updateRequest.RationaleForProject ?? project.RationaleForProject;
            project.RationaleForTrust = updateRequest.RationaleForTrust ?? project.RationaleForTrust;
            project.RisksAndIssues = updateRequest.RisksAndIssues ?? project.RisksAndIssues;
            project.RevenueCarryForwardAtEndMarchCurrentYear = updateRequest.RevenueCarryForwardAtEndMarchCurrentYear == default(decimal) ? null :
                updateRequest.RevenueCarryForwardAtEndMarchCurrentYear ?? project.RevenueCarryForwardAtEndMarchCurrentYear;
            project.ProjectedRevenueBalanceAtEndMarchNextYear = updateRequest.ProjectedRevenueBalanceAtEndMarchNextYear == default(decimal) ? null :
                updateRequest.ProjectedRevenueBalanceAtEndMarchNextYear ?? project.ProjectedRevenueBalanceAtEndMarchNextYear;

            project.RationaleSectionComplete = updateRequest.RationaleSectionComplete ?? project.RationaleSectionComplete;
            project.LocalAuthorityInformationTemplateSentDate = updateRequest.LocalAuthorityInformationTemplateSentDate == default(DateTime)
                ? null
                : updateRequest.LocalAuthorityInformationTemplateSentDate ?? project.LocalAuthorityInformationTemplateSentDate;
            project.LocalAuthorityInformationTemplateReturnedDate = updateRequest.LocalAuthorityInformationTemplateReturnedDate == default(DateTime)
                ? null
                : updateRequest.LocalAuthorityInformationTemplateReturnedDate ?? project.LocalAuthorityInformationTemplateReturnedDate;
            project.LocalAuthorityInformationTemplateComments = updateRequest.LocalAuthorityInformationTemplateComments ??
                project.LocalAuthorityInformationTemplateComments;
            project.LocalAuthorityInformationTemplateLink = updateRequest.LocalAuthorityInformationTemplateLink ??
                project.LocalAuthorityInformationTemplateLink;
            project.LocalAuthorityInformationTemplateSectionComplete = updateRequest.LocalAuthorityInformationTemplateSectionComplete ??
                project.LocalAuthorityInformationTemplateSectionComplete;
            project.RecommendationForProject = updateRequest.RecommendationForProject ?? project.RecommendationForProject;
            project.AcademyOrderRequired = updateRequest.AcademyOrderRequired ?? project.AcademyOrderRequired;
            project.SchoolAndTrustInformationSectionComplete = updateRequest.SchoolAndTrustInformationSectionComplete ?? project.SchoolAndTrustInformationSectionComplete;
            project.DistanceFromSchoolToTrustHeadquarters = updateRequest.DistanceFromSchoolToTrustHeadquarters == default(decimal) ? null
                : updateRequest.DistanceFromSchoolToTrustHeadquarters ?? project.DistanceFromSchoolToTrustHeadquarters;
            project.DistanceFromSchoolToTrustHeadquartersAdditionalInformation = updateRequest.DistanceFromSchoolToTrustHeadquartersAdditionalInformation ?? project.DistanceFromSchoolToTrustHeadquartersAdditionalInformation;
            project.MemberOfParliamentName = updateRequest.MemberOfParliamentName ?? project.MemberOfParliamentName;
            project.MemberOfParliamentParty = updateRequest.MemberOfParliamentParty ?? project.MemberOfParliamentParty;
             project.GeneralInformationSectionComplete = updateRequest.GeneralInformationSectionComplete ?? project.GeneralInformationSectionComplete;
            project.RisksAndIssuesSectionComplete = updateRequest.RisksAndIssuesSectionComplete ?? project.RisksAndIssuesSectionComplete;
            project.SchoolPerformanceAdditionalInformation = updateRequest.SchoolPerformanceAdditionalInformation ??
                project.SchoolPerformanceAdditionalInformation;
            project.CapitalCarryForwardAtEndMarchCurrentYear = updateRequest.CapitalCarryForwardAtEndMarchCurrentYear == default(decimal) ? null :
                updateRequest.CapitalCarryForwardAtEndMarchCurrentYear ?? project.CapitalCarryForwardAtEndMarchCurrentYear;
            project.CapitalCarryForwardAtEndMarchNextYear = updateRequest.CapitalCarryForwardAtEndMarchNextYear == default(decimal) ? null :
                updateRequest.CapitalCarryForwardAtEndMarchNextYear ?? project.CapitalCarryForwardAtEndMarchNextYear;
            project.SchoolBudgetInformationAdditionalInformation = updateRequest.SchoolBudgetInformationAdditionalInformation ??
                project.SchoolBudgetInformationAdditionalInformation;
            project.SchoolBudgetInformationSectionComplete = updateRequest.SchoolBudgetInformationSectionComplete ?? project.SchoolBudgetInformationSectionComplete;
            project.SchoolPupilForecastsAdditionalInformation = updateRequest.SchoolPupilForecastsAdditionalInformation ??
                project.SchoolPupilForecastsAdditionalInformation;
            project.YearOneProjectedCapacity =
                updateRequest.YearOneProjectedCapacity ?? project.YearOneProjectedCapacity;
            project.YearOneProjectedPupilNumbers =
                updateRequest.YearOneProjectedPupilNumbers ?? project.YearOneProjectedPupilNumbers;
            project.YearTwoProjectedCapacity =
                updateRequest.YearTwoProjectedCapacity ?? project.YearTwoProjectedCapacity;
            project.YearTwoProjectedPupilNumbers =
                updateRequest.YearTwoProjectedPupilNumbers ?? project.YearTwoProjectedPupilNumbers;
            project.YearThreeProjectedCapacity =
                updateRequest.YearThreeProjectedCapacity ?? project.YearThreeProjectedCapacity;
            project.YearThreeProjectedPupilNumbers =
                updateRequest.YearThreeProjectedPupilNumbers ?? project.YearThreeProjectedPupilNumbers;
            project.KeyStage2PerformanceAdditionalInformation = updateRequest.KeyStage2PerformanceAdditionalInformation ??
                project.KeyStage2PerformanceAdditionalInformation;
            project.KeyStage4PerformanceAdditionalInformation = updateRequest.KeyStage4PerformanceAdditionalInformation ??
                project.KeyStage4PerformanceAdditionalInformation;
            project.KeyStage5PerformanceAdditionalInformation = updateRequest.KeyStage5PerformanceAdditionalInformation ??
                project.KeyStage5PerformanceAdditionalInformation;
            project.PreviousHeadTeacherBoardDateQuestion = updateRequest.PreviousHeadTeacherBoardDateQuestion ??
                project.PreviousHeadTeacherBoardDateQuestion;
            project.PreviousHeadTeacherBoardDate = updateRequest.PreviousHeadTeacherBoardDate == default(DateTime) ? null
                : updateRequest.PreviousHeadTeacherBoardDate ?? project.PreviousHeadTeacherBoardDate;
            project.ConversionSupportGrantAmount =
                updateRequest.ConversionSupportGrantAmount ?? project.ConversionSupportGrantAmount;
            project.ConversionSupportGrantChangeReason = updateRequest.ConversionSupportGrantChangeReason ?? project.ConversionSupportGrantChangeReason;

            return project;
        }
    }
}
