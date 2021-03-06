using FizzWare.NBuilder;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using TramsDataApi.DatabaseModels;
using TramsDataApi.Factories.CaseActionFactories;
using TramsDataApi.RequestModels.CaseActions.SRMA;
using TramsDataApi.ResponseModels.CaseActions.SRMA;
using Xunit;

namespace TramsDataApi.Test.Factories
{
    public class SRMAFactoryTests
    {
        [Fact]
        public void CreateDBModel_ExpectedSRMACase_WhenCreateSRMARequestProvided()
        {
            var dtNow = DateTime.Now;

            var details = new
            {
                Id = 123,
                CaseId = 567,
                CreatedAt = dtNow,
                DateOffered = dtNow.AddDays(29),
                DateReportSentToTrust = dtNow.AddDays(28),
                DateVisitStart = dtNow.AddDays(27),
                DateVisitEnd = dtNow.AddDays(26),
                DateAccepted = dtNow.AddDays(25),
                Status = Enums.SRMAStatus.TrustConsidering,
                Reason = Enums.SRMAReasonOffered.RDDIntervention,
                Notes = "notes notes notes"
            };

            var createSRMARequest = Builder<CreateSRMARequest>.CreateNew()
                .With(r => r.Id = details.Id)
                .With(r => r.CaseUrn = details.CaseId)
                .With(r => r.DateOffered = details.DateOffered)
                .With(r => r.DateReportSentToTrust = details.DateReportSentToTrust)
                .With(r => r.DateVisitStart = details.DateVisitStart)
                .With(r => r.DateVisitEnd = details.DateVisitEnd)
                .With(r => r.DateAccepted = details.DateAccepted)
                .With(r => r.Status = details.Status)
                .With(r => r.Reason = details.Reason)
                .With(r => r.Notes = details.Notes)
                .With(r => r.CreatedAt = details.CreatedAt)
                .Build();

            var expectedSRMAModel = new SRMACase
            {
                Id = details.Id,
                CaseUrn = details.CaseId,
                DateOffered = details.DateOffered,
                DateReportSentToTrust = details.DateReportSentToTrust,
                StartDateOfVisit = details.DateVisitStart,
                EndDateOfVisit = details.DateVisitEnd,
                DateAccepted = details.DateAccepted,
                StatusId = (int)details.Status,
                ReasonId = (int)details.Reason,
                Notes = details.Notes,
                CreatedAt = details.CreatedAt
            };

            var response = SRMAFactory.CreateDBModel(createSRMARequest);

            response.Should().BeEquivalentTo(expectedSRMAModel);
        }

        [Fact]
        public void CreateResponse_ExpectedSRMAResponse_WhenSRMACaseProvided()
        {
            var dtNow = DateTime.Now;

            var details = new
            {
                Id = 123,
                CaseId = 988,
                CreatedAt = dtNow,
                DateOffered = dtNow.AddDays(29),
                DateReportSentToTrust = dtNow.AddDays(28),
                DateVisitStart = dtNow.AddDays(27),
                DateVisitEnd = dtNow.AddDays(26),
                DateAccepted = dtNow.AddDays(25),
                Status = Enums.SRMAStatus.TrustConsidering,
                Reason = Enums.SRMAReasonOffered.RDDIntervention,
                Notes = "notes notes notes",
                Urn = 963L,
                CloseStatusId = (int?)null,
                UpdatedAt = dtNow,
                ClosedAt = (DateTime?)null,
                CreatedBy = "TestUser"
            };

            var srmaModel = Builder<SRMACase>.CreateNew()
                .With(r => r.Id = details.Id)
                .With(r => r.CaseUrn = details.CaseId)
                .With(r => r.DateOffered = details.DateOffered)
                .With(r => r.DateReportSentToTrust = details.DateReportSentToTrust)
                .With(r => r.StartDateOfVisit = details.DateVisitStart)
                .With(r => r.EndDateOfVisit = details.DateVisitEnd)
                .With(r => r.DateAccepted = details.DateAccepted)
                .With(r => r.StatusId = (int)details.Status)
                .With(r => r.ReasonId = (int)details.Reason)
                .With(r => r.Notes = details.Notes)
                .With(r => r.CreatedAt = details.CreatedAt)
                .With(r => r.Urn = details.Urn)
                .With(r => r.CloseStatusId = details.CloseStatusId)
                .With(r => r.UpdatedAt = details.UpdatedAt)
                .With(r => r.ClosedAt = details.ClosedAt)
                .With(r => r.CreatedBy= details.CreatedBy)
                .Build();

            var expectedCreateSRMAResponse = new SRMAResponse
            {
                Id = details.Id,
                CaseUrn = details.CaseId,
                DateOffered = details.DateOffered,
                DateReportSentToTrust = details.DateReportSentToTrust,
                DateVisitStart = details.DateVisitStart,
                DateVisitEnd = details.DateVisitEnd,
                DateAccepted = details.DateAccepted,
                Status = details.Status,
                Reason = details.Reason,
                Notes = details.Notes,
                CreatedAt = details.CreatedAt,
                Urn = details.Urn,
                CloseStatus = (Enums.SRMAStatus)(details.CloseStatusId ?? 0),
                UpdatedAt = details.UpdatedAt,
                ClosedAt = details.ClosedAt,
                CreatedBy = details.CreatedBy
            };

            var response = SRMAFactory.CreateResponse(srmaModel);

            response.Should().BeEquivalentTo(expectedCreateSRMAResponse);
        }

    }
}
