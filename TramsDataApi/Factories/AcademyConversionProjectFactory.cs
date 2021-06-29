using System;
using System.Collections.Generic;
using TramsDataApi.DatabaseModels;
using TramsDataApi.RequestModels.AcademyConversionProject;

namespace TramsDataApi.Factories
{
    public class AcademyConversionProjectFactory
    {
        public static IfdPipeline Update(IfdPipeline project, UpdateAcademyConversionProjectRequest updateRequest)
        {
            if (updateRequest == null)
            {
                return project;
            }

            project.DeliveryProcessDateForDiscussionByRscHtb = updateRequest.HeadTeacherBoardDate ?? project.DeliveryProcessDateForDiscussionByRscHtb;
            project.GeneralDetailsProjectLead = updateRequest.Author ?? project.GeneralDetailsProjectLead;
            project.GeneralDetailsTeamLeader = updateRequest.ClearedBy ?? project.GeneralDetailsTeamLeader;
            project.GeneralDetailsExpectedOpeningDate = updateRequest.ProposedAcademyOpeningDate ?? project.GeneralDetailsExpectedOpeningDate;
            project.DeliveryProcessPan = updateRequest.PublishedAdmissionNumber ?? project.DeliveryProcessPan;
            project.ProjectTemplateInformationViabilityIssue = updateRequest.ViabilityIssues ?? project.ProjectTemplateInformationViabilityIssue;
            project.ProjectTemplateInformationDeficit = updateRequest.FinancialDeficit ?? project.ProjectTemplateInformationDeficit;
            project.ProjectTemplateInformationRationaleForProject = updateRequest.RationaleForProject ?? project.ProjectTemplateInformationRationaleForProject;
            project.ProjectTemplateInformationRationaleForSponsor = updateRequest.RationaleForTrust ?? project.ProjectTemplateInformationRationaleForSponsor;
            project.ProjectTemplateInformationRisksAndIssues = updateRequest.RisksAndIssues ?? project.ProjectTemplateInformationRisksAndIssues;
            project.ProjectTemplateInformationFyRevenueBalanceCarriedForward = updateRequest.RevenueCarryForwardAtEndMarchCurrentYear == default(decimal) ? null :
                updateRequest.RevenueCarryForwardAtEndMarchCurrentYear?.ToString() ?? project.ProjectTemplateInformationFyRevenueBalanceCarriedForward;
            project.ProjectTemplateInformationFy1RevenueBalanceCarriedForward = updateRequest.ProjectedRevenueBalanceAtEndMarchNextYear == default(decimal) ? null :
                updateRequest.ProjectedRevenueBalanceAtEndMarchNextYear?.ToString() ?? project.ProjectTemplateInformationFy1RevenueBalanceCarriedForward;

            return project;
        }

        public static AcademyConversionProject Update(AcademyConversionProject project, UpdateAcademyConversionProjectRequest updateRequest)
        {
            if (updateRequest == null)
            {
                return project;
            }

            project.RationaleSectionComplete = updateRequest.RationaleSectionComplete ?? project.RationaleSectionComplete;
            project.LocalAuthorityInformationTemplateSentDate =
                updateRequest.LocalAuthorityInformationTemplateSentDate == default(DateTime) ?
                    null : updateRequest.LocalAuthorityInformationTemplateSentDate ?? project.LocalAuthorityInformationTemplateSentDate;
            project.LocalAuthorityInformationTemplateReturnedDate =
                updateRequest.LocalAuthorityInformationTemplateReturnedDate == default(DateTime) ?
                    null : updateRequest.LocalAuthorityInformationTemplateReturnedDate ?? project.LocalAuthorityInformationTemplateReturnedDate;
            project.LocalAuthorityInformationTemplateComments = updateRequest.LocalAuthorityInformationTemplateComments ??
                project.LocalAuthorityInformationTemplateComments;
            project.LocalAuthorityInformationTemplateLink = updateRequest.LocalAuthorityInformationTemplateLink ??
                project.LocalAuthorityInformationTemplateLink;
            project.LocalAuthorityInformationTemplateSectionComplete = updateRequest.LocalAuthorityInformationTemplateSectionComplete ??
                project.LocalAuthorityInformationTemplateSectionComplete;
            project.RecommendationForProject = updateRequest.RecommendationForProject ?? project.RecommendationForProject;
            project.AcademyOrderRequired = updateRequest.AcademyOrderRequired ?? project.AcademyOrderRequired;
            project.SchoolAndTrustInformationSectionComplete = updateRequest.SchoolAndTrustInformationSectionComplete ?? project.SchoolAndTrustInformationSectionComplete;
            project.DistanceFromSchoolToTrustHeadquarters = updateRequest.DistanceFromSchoolToTrustHeadquarters ?? project.DistanceFromSchoolToTrustHeadquarters;
            project.DistanceFromSchoolToTrustHeadquartersAdditionalInformation = updateRequest.DistanceFromSchoolToTrustHeadquartersAdditionalInformation ?? project.DistanceFromSchoolToTrustHeadquartersAdditionalInformation;
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
            if (updateRequest.ProjectNote == null) return project;
            var projectNote = new ProjectNote
            {
                Subject = updateRequest.ProjectNote.Subject,
                Note = updateRequest.ProjectNote.Note,
                Author = updateRequest.ProjectNote.Author,
                Date = updateRequest.ProjectNote.Date,
            };
            if (project.ProjectNotes == null)
            {
                project.ProjectNotes = new List<ProjectNote> {projectNote};
            }
            else
            {
                project.ProjectNotes.Add(projectNote);
            }

            return project;
        }
    }
}
