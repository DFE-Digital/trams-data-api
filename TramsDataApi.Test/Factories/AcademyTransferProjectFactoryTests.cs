using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FizzWare.NBuilder;
using FluentAssertions;
using TramsDataApi.DatabaseModels;
using TramsDataApi.Factories;
using TramsDataApi.RequestModels;
using Xunit;

namespace TramsDataApi.Test.Factories
{
    public class AcademyTransferProjectFactoryTests
    {
        [Fact]
        public void ReturnsAnAcademyTransferProject_WhenGivenAnInitialAcademyTransferProjectRequest()
        {
            var randomGenerator = new RandomGenerator();
            var createRequest = Builder<AcademyTransferProjectRequest>.CreateNew()
                .With(c => c.OutgoingTrustUkprn = randomGenerator.NextString(8, 8))
                .With(c => c.Status = null)
                .With(c => c.State = null)
                .With(c => c.GeneralInformation = null)
                .With(c => c.Benefits = null)
                .With(c => c.Dates = null)
                .With(c => c.Rationale = null)
                .With(c => c.Features = null)
                .With(c => c.TransferringAcademies =
                    (List<TransferringAcademiesRequest>) Builder<TransferringAcademiesRequest>
                        .CreateListOfSize(5)
                        .All()
                        .With(t => t.OutgoingAcademyUkprn = randomGenerator.NextString(8, 8))
                        .With(t => t.IncomingTrustUkprn = null)
                        .With(t => t.PupilNumbersAdditionalInformation = null)
                        .With(t => t.LatestOfstedReportAdditionalInformation = null)
                        .With(t => t.KeyStage2PerformanceAdditionalInformation = null)
                        .With(t => t.KeyStage4PerformanceAdditionalInformation = null)
                        .With(t => t.KeyStage5PerformanceAdditionalInformation = null).Build())
                .Build();

            var expected = new AcademyTransferProjects
            {
                ProjectReference = createRequest.ProjectReference,
                OutgoingTrustUkprn = createRequest.OutgoingTrustUkprn,
                WhoInitiatedTheTransfer = null,
                RddOrEsfaIntervention = null,
                RddOrEsfaInterventionDetail = null,
                TypeOfTransfer = null,
                OtherTransferTypeDescription = null,
                TransferFirstDiscussed = null,
                TargetDateForTransfer = null,
                HtbDate = null,
                ProjectRationale = null,
                TrustSponsorRationale = null,
                State = null,
                Status = null,
                Author = null,
                Recommendation = null,
                HighProfileShouldBeConsidered = null,
                HighProfileFurtherSpecification = null,
                ComplexLandAndBuildingShouldBeConsidered = null,
                ComplexLandAndBuildingFurtherSpecification = null,
                FinanceAndDebtShouldBeConsidered = null,
                FinanceAndDebtFurtherSpecification = null,
                OtherBenefitValue = null,
                AcademyTransferProjectIntendedTransferBenefits = null,
                FeatureSectionIsCompleted = null,
                BenefitsSectionIsCompleted = null,
                RationaleSectionIsCompleted = null,
                TransferringAcademies = createRequest.TransferringAcademies
                    .Select(t => new TransferringAcademies
                    {
                        OutgoingAcademyUkprn = t.OutgoingAcademyUkprn,
                        IncomingTrustUkprn = null,
                        LatestOfstedReportAdditionalInformation = null,
                        PupilNumbersAdditionalInformation = null,
                        KeyStage2PerformanceAdditionalInformation = null,
                        KeyStage4PerformanceAdditionalInformation = null,
                        KeyStage5PerformanceAdditionalInformation = null
                    })
                    .ToList()
            };

            var result = AcademyTransferProjectFactory.Create(createRequest);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReturnsAnAcademyTransferProject_WhenGivenACompleteAcademyTransferProjectRequest()
        {
            var randomGenerator = new RandomGenerator();

            var benefitsRequest = Builder<AcademyTransferProjectBenefitsRequest>.CreateNew()
                .With(b => b.IntendedTransferBenefits = Builder<IntendedTransferBenefitRequest>.CreateNew()
                    .With(i => i.SelectedBenefits = new List<string>()).Build())
                .With(b => b.OtherFactorsToConsider = Builder<OtherFactorsToConsiderRequest>.CreateNew()
                    .With(o => o.ComplexLandAndBuilding = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .With(o => o.FinanceAndDebt = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .With(o => o.HighProfile = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .With(o => o.OtherRisks = Builder<BenefitConsideredFactorRequest>.CreateNew().Build())
                    .Build()
                )
                .Build();

            var datesRequest = Builder<AcademyTransferProjectDatesRequest>.CreateNew()
                .With(d => d.TransferFirstDiscussed =
                    randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.TargetDateForTransfer =
                    randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.HtbDate = randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .With(d => d.HasTransferFirstDiscussedDate = true)
                .With(d => d.HasHtbDate = true)
                .With(d => d.HasTargetDateForTransfer = true)
                .Build();

            var createRequest = Builder<AcademyTransferProjectRequest>.CreateNew()
                .With(c => c.OutgoingTrustUkprn = randomGenerator.NextString(8, 8))
                .With(c => c.Benefits = benefitsRequest)
                .With(c => c.Dates = datesRequest)
                .With(c => c.Rationale = Builder<AcademyTransferProjectRationaleRequest>.CreateNew().Build())
                .With(c => c.GeneralInformation =
                    Builder<AcademyTransferProjectGeneralInformationRequest>.CreateNew().Build())
                .With(c => c.Features = Builder<AcademyTransferProjectFeaturesRequest>.CreateNew().Build())
                .With(c => c.TransferringAcademies =
                    (List<TransferringAcademiesRequest>) Builder<TransferringAcademiesRequest>
                        .CreateListOfSize(5).Build())
                .Build();

            var expected = new AcademyTransferProjects
            {
                ProjectReference = createRequest.ProjectReference,
                OutgoingTrustUkprn = createRequest.OutgoingTrustUkprn,
                WhoInitiatedTheTransfer = createRequest.Features.WhoInitiatedTheTransfer,
                RddOrEsfaIntervention = createRequest.Features.RddOrEsfaIntervention,
                RddOrEsfaInterventionDetail = createRequest.Features.RddOrEsfaInterventionDetail,
                TypeOfTransfer = createRequest.Features.TypeOfTransfer,
                OtherTransferTypeDescription = createRequest.Features.OtherTransferTypeDescription,
                TransferFirstDiscussed = DateTime.ParseExact(createRequest.Dates.TransferFirstDiscussed, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture),
                TargetDateForTransfer = DateTime.ParseExact(createRequest.Dates.TargetDateForTransfer, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture),
                HtbDate = DateTime.ParseExact(createRequest.Dates.HtbDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                HasHtbDate = createRequest.Dates.HasHtbDate,
                HasTransferFirstDiscussedDate = createRequest.Dates.HasTransferFirstDiscussedDate,
                HasTargetDateForTransfer = createRequest.Dates.HasTargetDateForTransfer,
                ProjectRationale = createRequest.Rationale.ProjectRationale,
                TrustSponsorRationale = createRequest.Rationale.TrustSponsorRationale,
                State = createRequest.State,
                Status = createRequest.Status,
                Author = createRequest.GeneralInformation.Author,
                Recommendation = createRequest.GeneralInformation.Recommendation,
                AnyRisks = createRequest.Benefits.AnyRisks,
                HighProfileShouldBeConsidered =
                    createRequest.Benefits.OtherFactorsToConsider.HighProfile.ShouldBeConsidered,
                HighProfileFurtherSpecification =
                    createRequest.Benefits.OtherFactorsToConsider.HighProfile.FurtherSpecification,
                ComplexLandAndBuildingShouldBeConsidered = createRequest.Benefits.OtherFactorsToConsider
                    .ComplexLandAndBuilding.ShouldBeConsidered,
                ComplexLandAndBuildingFurtherSpecification = createRequest.Benefits.OtherFactorsToConsider
                    .ComplexLandAndBuilding.FurtherSpecification,
                FinanceAndDebtShouldBeConsidered =
                    createRequest.Benefits.OtherFactorsToConsider.FinanceAndDebt.ShouldBeConsidered,
                FinanceAndDebtFurtherSpecification =
                    createRequest.Benefits.OtherFactorsToConsider.FinanceAndDebt.FurtherSpecification,
                OtherRisksShouldBeConsidered = 
                    createRequest.Benefits.OtherFactorsToConsider.OtherRisks.ShouldBeConsidered,
                OtherRisksFurtherSpecification = 
                    createRequest.Benefits.OtherFactorsToConsider.OtherRisks.FurtherSpecification,
                OtherBenefitValue = createRequest.Benefits.IntendedTransferBenefits.OtherBenefitValue,
                FeatureSectionIsCompleted = createRequest.Features?.IsCompleted,
                BenefitsSectionIsCompleted = createRequest.Benefits?.IsCompleted,
                RationaleSectionIsCompleted = createRequest.Rationale?.IsCompleted,
                AcademyTransferProjectIntendedTransferBenefits = createRequest.Benefits.IntendedTransferBenefits
                    .SelectedBenefits
                    .Select(b => new AcademyTransferProjectIntendedTransferBenefits {SelectedBenefit = b}).ToList(),
                TransferringAcademies = createRequest.TransferringAcademies
                    .Select(t => new TransferringAcademies
                    {
                        OutgoingAcademyUkprn = t.OutgoingAcademyUkprn,
                        IncomingTrustUkprn = t.IncomingTrustUkprn,
                        PupilNumbersAdditionalInformation = t.PupilNumbersAdditionalInformation,
                        LatestOfstedReportAdditionalInformation = t.LatestOfstedReportAdditionalInformation,
                        KeyStage2PerformanceAdditionalInformation = t.KeyStage2PerformanceAdditionalInformation,
                        KeyStage4PerformanceAdditionalInformation = t.KeyStage4PerformanceAdditionalInformation,
                        KeyStage5PerformanceAdditionalInformation = t.KeyStage5PerformanceAdditionalInformation
                    })
                    .ToList()
            };

            var result = AcademyTransferProjectFactory.Create(createRequest);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReturnsOriginalAcademyTransferProject_WhenUpdating_IfAcademyTransferProjectRequestIsNull()
        {
            var academyTransferProject = Builder<AcademyTransferProjects>.CreateNew().Build();

            var result = AcademyTransferProjectFactory.Update(academyTransferProject, null);

            result.Should().BeEquivalentTo(academyTransferProject);
        }

        [Fact]
        public void ReturnsUpdatedAcademyTransferProject_WhenUpdating_IfRequestHasUpdatedFields()
        {
            var academyTransferProject = Builder<AcademyTransferProjects>.CreateNew().Build();
            var updateRequest = new AcademyTransferProjectRequest
            {
                OutgoingTrustUkprn = "12312354",
                Rationale = new AcademyTransferProjectRationaleRequest
                {
                    ProjectRationale = "A new rationale for the project"
                }
            };

            var expected = new AcademyTransferProjects
            {
                Id = academyTransferProject.Id,
                Urn = academyTransferProject.Urn,
                ProjectReference = academyTransferProject.ProjectReference,
                OutgoingTrustUkprn = updateRequest.OutgoingTrustUkprn,
                ProjectRationale = updateRequest.Rationale.ProjectRationale,
                TransferringAcademies = academyTransferProject.TransferringAcademies,
                WhoInitiatedTheTransfer = academyTransferProject.WhoInitiatedTheTransfer,
                TargetDateForTransfer = academyTransferProject.TargetDateForTransfer,
                RddOrEsfaIntervention = academyTransferProject.RddOrEsfaIntervention,
                RddOrEsfaInterventionDetail = academyTransferProject.RddOrEsfaInterventionDetail,
                TypeOfTransfer = academyTransferProject.TypeOfTransfer,
                OtherTransferTypeDescription = academyTransferProject.OtherTransferTypeDescription,
                TransferFirstDiscussed = academyTransferProject.TransferFirstDiscussed,
                HtbDate = academyTransferProject.HtbDate,
                TrustSponsorRationale = academyTransferProject.TrustSponsorRationale,
                State = academyTransferProject.State,
                Status = academyTransferProject.Status,
                Author = academyTransferProject.Author,
                Recommendation = academyTransferProject.Recommendation,
                AnyRisks = academyTransferProject.AnyRisks,
                HighProfileShouldBeConsidered = academyTransferProject.HighProfileShouldBeConsidered,
                HighProfileFurtherSpecification = academyTransferProject.HighProfileFurtherSpecification,
                ComplexLandAndBuildingShouldBeConsidered =
                    academyTransferProject.ComplexLandAndBuildingShouldBeConsidered,
                ComplexLandAndBuildingFurtherSpecification =
                    academyTransferProject.ComplexLandAndBuildingFurtherSpecification,
                FinanceAndDebtShouldBeConsidered = academyTransferProject.FinanceAndDebtShouldBeConsidered,
                FinanceAndDebtFurtherSpecification = academyTransferProject.FinanceAndDebtFurtherSpecification,
                OtherRisksShouldBeConsidered = academyTransferProject.OtherRisksShouldBeConsidered,
                OtherRisksFurtherSpecification = academyTransferProject.OtherRisksFurtherSpecification,
                OtherBenefitValue = academyTransferProject.OtherBenefitValue,
                FeatureSectionIsCompleted = academyTransferProject.FeatureSectionIsCompleted,
                BenefitsSectionIsCompleted = academyTransferProject.BenefitsSectionIsCompleted,
                RationaleSectionIsCompleted = academyTransferProject.RationaleSectionIsCompleted,
                AcademyTransferProjectIntendedTransferBenefits =
                    academyTransferProject.AcademyTransferProjectIntendedTransferBenefits,
                HasTransferFirstDiscussedDate = academyTransferProject.HasTransferFirstDiscussedDate,
                HasHtbDate = academyTransferProject.HasHtbDate,
                HasTargetDateForTransfer = academyTransferProject.HasTargetDateForTransfer
            };

            var result = AcademyTransferProjectFactory.Update(academyTransferProject, updateRequest);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReturnsUpdatedAcademyTransferProject_WhenUpdating_AndUpdatesTheListOfTransferringAcademies()
        {
            var academyTransferProject = Builder<AcademyTransferProjects>
                .CreateNew()
                .With(atp => atp.TransferringAcademies = Builder<TransferringAcademies>.CreateListOfSize(5).Build())
                .Build();

            var transferringAcademiesRequests = academyTransferProject.TransferringAcademies.Select(ta =>
                new TransferringAcademiesRequest
                {
                    OutgoingAcademyUkprn = ta.OutgoingAcademyUkprn,
                    IncomingTrustUkprn = ta.IncomingTrustUkprn,
                    LatestOfstedReportAdditionalInformation = ta.LatestOfstedReportAdditionalInformation,
                    PupilNumbersAdditionalInformation = ta.PupilNumbersAdditionalInformation,
                    KeyStage2PerformanceAdditionalInformation = ta.KeyStage2PerformanceAdditionalInformation,
                    KeyStage4PerformanceAdditionalInformation = ta.KeyStage4PerformanceAdditionalInformation,
                    KeyStage5PerformanceAdditionalInformation = ta.KeyStage5PerformanceAdditionalInformation
                    
                }).ToList();
            transferringAcademiesRequests.ElementAt(0).OutgoingAcademyUkprn = "12385731";

            var updateRequest = new AcademyTransferProjectRequest
            {
                OutgoingTrustUkprn = "12387123",
                TransferringAcademies = transferringAcademiesRequests,
            };

            var expectedTransferringAcademies = transferringAcademiesRequests.Select(ta => new TransferringAcademies
            {
                OutgoingAcademyUkprn = ta.OutgoingAcademyUkprn,
                IncomingTrustUkprn = ta.IncomingTrustUkprn,
                PupilNumbersAdditionalInformation = ta.PupilNumbersAdditionalInformation,
                LatestOfstedReportAdditionalInformation = ta.LatestOfstedReportAdditionalInformation,
                KeyStage2PerformanceAdditionalInformation = ta.KeyStage2PerformanceAdditionalInformation,
                KeyStage4PerformanceAdditionalInformation = ta.KeyStage4PerformanceAdditionalInformation,
                KeyStage5PerformanceAdditionalInformation = ta.KeyStage5PerformanceAdditionalInformation
            }).ToList();

            var expected = new AcademyTransferProjects
            {
                Id = academyTransferProject.Id,
                Urn = academyTransferProject.Urn,
                ProjectReference = academyTransferProject.ProjectReference,
                OutgoingTrustUkprn = updateRequest.OutgoingTrustUkprn,
                ProjectRationale = academyTransferProject.ProjectRationale,
                TransferringAcademies = expectedTransferringAcademies,
                WhoInitiatedTheTransfer = academyTransferProject.WhoInitiatedTheTransfer,
                TargetDateForTransfer = academyTransferProject.TargetDateForTransfer,
                RddOrEsfaIntervention = academyTransferProject.RddOrEsfaIntervention,
                RddOrEsfaInterventionDetail = academyTransferProject.RddOrEsfaInterventionDetail,
                TypeOfTransfer = academyTransferProject.TypeOfTransfer,
                OtherTransferTypeDescription = academyTransferProject.OtherTransferTypeDescription,
                TransferFirstDiscussed = academyTransferProject.TransferFirstDiscussed,
                HtbDate = academyTransferProject.HtbDate,
                TrustSponsorRationale = academyTransferProject.TrustSponsorRationale,
                State = academyTransferProject.State,
                Status = academyTransferProject.Status,
                Author = academyTransferProject.Author,
                Recommendation = academyTransferProject.Recommendation,
                AnyRisks = academyTransferProject.AnyRisks,
                HighProfileShouldBeConsidered = academyTransferProject.HighProfileShouldBeConsidered,
                HighProfileFurtherSpecification = academyTransferProject.HighProfileFurtherSpecification,
                ComplexLandAndBuildingShouldBeConsidered =
                    academyTransferProject.ComplexLandAndBuildingShouldBeConsidered,
                ComplexLandAndBuildingFurtherSpecification =
                    academyTransferProject.ComplexLandAndBuildingFurtherSpecification,
                FinanceAndDebtShouldBeConsidered = academyTransferProject.FinanceAndDebtShouldBeConsidered,
                FinanceAndDebtFurtherSpecification = academyTransferProject.FinanceAndDebtFurtherSpecification,
                OtherRisksShouldBeConsidered = academyTransferProject.OtherRisksShouldBeConsidered,
                OtherRisksFurtherSpecification = academyTransferProject.OtherRisksFurtherSpecification,
                OtherBenefitValue = academyTransferProject.OtherBenefitValue,
                FeatureSectionIsCompleted = academyTransferProject.FeatureSectionIsCompleted,
                BenefitsSectionIsCompleted = academyTransferProject.BenefitsSectionIsCompleted,
                RationaleSectionIsCompleted = academyTransferProject.RationaleSectionIsCompleted,
                AcademyTransferProjectIntendedTransferBenefits =
                    academyTransferProject.AcademyTransferProjectIntendedTransferBenefits,
                HasHtbDate = academyTransferProject.HasHtbDate,
                HasTransferFirstDiscussedDate = academyTransferProject.HasTransferFirstDiscussedDate,
                HasTargetDateForTransfer = academyTransferProject.HasTargetDateForTransfer
            };

            var result = AcademyTransferProjectFactory.Update(academyTransferProject, updateRequest);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReturnsUpdatedAcademyTransferProject_WhenUpdating_IfRequestHasUpdatedSelectedBenefitsRequests()
        {
            var academyTransferProject = Builder<AcademyTransferProjects>
                .CreateNew()
                .With(atp =>
                    atp.AcademyTransferProjectIntendedTransferBenefits =
                        Builder<AcademyTransferProjectIntendedTransferBenefits>.CreateListOfSize(5).Build())
                .Build();

            var updatedBenefits = academyTransferProject.AcademyTransferProjectIntendedTransferBenefits
                .Select(benefit => benefit.SelectedBenefit)
                .ToList();
            updatedBenefits.Insert(0, "A completely new benefit");

            var updateRequest = new AcademyTransferProjectRequest
            {
                State = "A New State",
                Benefits = new AcademyTransferProjectBenefitsRequest
                {
                    IntendedTransferBenefits = new IntendedTransferBenefitRequest
                    {
                        SelectedBenefits = updatedBenefits,
                    }
                }
            };

            var expectedBenefits = updateRequest.Benefits.IntendedTransferBenefits.SelectedBenefits
                .Select(selectedBenefit => new AcademyTransferProjectIntendedTransferBenefits
                    {SelectedBenefit = selectedBenefit})
                .ToList();

            var expected = new AcademyTransferProjects
            {
                Id = academyTransferProject.Id,
                Urn = academyTransferProject.Urn,
                ProjectReference = academyTransferProject.ProjectReference,
                OutgoingTrustUkprn = academyTransferProject.OutgoingTrustUkprn,
                ProjectRationale = academyTransferProject.ProjectRationale,
                TransferringAcademies = academyTransferProject.TransferringAcademies,
                WhoInitiatedTheTransfer = academyTransferProject.WhoInitiatedTheTransfer,
                TargetDateForTransfer = academyTransferProject.TargetDateForTransfer,
                RddOrEsfaIntervention = academyTransferProject.RddOrEsfaIntervention,
                RddOrEsfaInterventionDetail = academyTransferProject.RddOrEsfaInterventionDetail,
                TypeOfTransfer = academyTransferProject.TypeOfTransfer,
                OtherTransferTypeDescription = academyTransferProject.OtherTransferTypeDescription,
                TransferFirstDiscussed = academyTransferProject.TransferFirstDiscussed,
                HtbDate = academyTransferProject.HtbDate,
                TrustSponsorRationale = academyTransferProject.TrustSponsorRationale,
                State = updateRequest.State,
                Status = academyTransferProject.Status,
                Author = academyTransferProject.Author,
                Recommendation = academyTransferProject.Recommendation,
                AnyRisks = academyTransferProject.AnyRisks,
                HighProfileShouldBeConsidered = academyTransferProject.HighProfileShouldBeConsidered,
                HighProfileFurtherSpecification = academyTransferProject.HighProfileFurtherSpecification,
                ComplexLandAndBuildingShouldBeConsidered =
                    academyTransferProject.ComplexLandAndBuildingShouldBeConsidered,
                ComplexLandAndBuildingFurtherSpecification =
                    academyTransferProject.ComplexLandAndBuildingFurtherSpecification,
                FinanceAndDebtShouldBeConsidered = academyTransferProject.FinanceAndDebtShouldBeConsidered,
                FinanceAndDebtFurtherSpecification = academyTransferProject.FinanceAndDebtFurtherSpecification,
                OtherRisksShouldBeConsidered = academyTransferProject.OtherRisksShouldBeConsidered,
                OtherRisksFurtherSpecification = academyTransferProject.OtherRisksFurtherSpecification,
                OtherBenefitValue = academyTransferProject.OtherBenefitValue,
                FeatureSectionIsCompleted = academyTransferProject.FeatureSectionIsCompleted,
                BenefitsSectionIsCompleted = academyTransferProject.BenefitsSectionIsCompleted,
                RationaleSectionIsCompleted = academyTransferProject.RationaleSectionIsCompleted,
                AcademyTransferProjectIntendedTransferBenefits = expectedBenefits,
                HasHtbDate = academyTransferProject.HasHtbDate,
                HasTransferFirstDiscussedDate = academyTransferProject.HasTransferFirstDiscussedDate,
                HasTargetDateForTransfer = academyTransferProject.HasTargetDateForTransfer
            };

            var result = AcademyTransferProjectFactory.Update(academyTransferProject, updateRequest);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReturnsUpdatedAcademyTransferProject_WhenUpdating_IfRequestIsToSetDatesBackToNull()
        {
            var academyTransferProject = Builder<AcademyTransferProjects>
                .CreateNew()
                .Build();

            var updateRequest = new AcademyTransferProjectRequest
            {
                Dates = new AcademyTransferProjectDatesRequest
                {
                    HasTargetDateForTransfer = false,
                    HasTransferFirstDiscussedDate = false,
                    HasHtbDate = false
                }
            };

            var expected = new AcademyTransferProjects
            {
                Id = academyTransferProject.Id,
                Urn = academyTransferProject.Urn,
                ProjectReference = academyTransferProject.ProjectReference,
                OutgoingTrustUkprn = academyTransferProject.OutgoingTrustUkprn,
                ProjectRationale = academyTransferProject.ProjectRationale,
                TransferringAcademies = academyTransferProject.TransferringAcademies,
                WhoInitiatedTheTransfer = academyTransferProject.WhoInitiatedTheTransfer,
                TargetDateForTransfer = null,
                RddOrEsfaIntervention = academyTransferProject.RddOrEsfaIntervention,
                RddOrEsfaInterventionDetail = academyTransferProject.RddOrEsfaInterventionDetail,
                TypeOfTransfer = academyTransferProject.TypeOfTransfer,
                OtherTransferTypeDescription = academyTransferProject.OtherTransferTypeDescription,
                TransferFirstDiscussed = null,
                HtbDate = null,
                TrustSponsorRationale = academyTransferProject.TrustSponsorRationale,
                State = academyTransferProject.State,
                Status = academyTransferProject.Status,
                Author = academyTransferProject.Author,
                Recommendation = academyTransferProject.Recommendation,
                AnyRisks = academyTransferProject.AnyRisks,
                HighProfileShouldBeConsidered = academyTransferProject.HighProfileShouldBeConsidered,
                HighProfileFurtherSpecification = academyTransferProject.HighProfileFurtherSpecification,
                ComplexLandAndBuildingShouldBeConsidered =
                    academyTransferProject.ComplexLandAndBuildingShouldBeConsidered,
                ComplexLandAndBuildingFurtherSpecification =
                    academyTransferProject.ComplexLandAndBuildingFurtherSpecification,
                FinanceAndDebtShouldBeConsidered = academyTransferProject.FinanceAndDebtShouldBeConsidered,
                FinanceAndDebtFurtherSpecification = academyTransferProject.FinanceAndDebtFurtherSpecification,
                OtherRisksShouldBeConsidered = academyTransferProject.OtherRisksShouldBeConsidered,
                OtherRisksFurtherSpecification = academyTransferProject.OtherRisksFurtherSpecification,
                OtherBenefitValue = academyTransferProject.OtherBenefitValue,
                FeatureSectionIsCompleted = academyTransferProject.FeatureSectionIsCompleted,
                BenefitsSectionIsCompleted = academyTransferProject.BenefitsSectionIsCompleted,
                RationaleSectionIsCompleted = academyTransferProject.RationaleSectionIsCompleted,
                AcademyTransferProjectIntendedTransferBenefits =
                    academyTransferProject.AcademyTransferProjectIntendedTransferBenefits,
                HasTransferFirstDiscussedDate = false,
                HasHtbDate = false,
                HasTargetDateForTransfer = false
            };

            var result = AcademyTransferProjectFactory.Update(academyTransferProject, updateRequest);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReturnsOriginalAcademyTransferProjectWithDatesSetToNull_WhenUpdating_IfHasDateFieldsAreFalse()
        {
            var randomGenerator = new RandomGenerator();

            var academyTransferProject = Builder<AcademyTransferProjects>
                .CreateNew()
                .Build();

            var updateRequest = new AcademyTransferProjectRequest
            {
                Dates = new AcademyTransferProjectDatesRequest
                {
                    TargetDateForTransfer =
                        randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    TransferFirstDiscussed =
                        randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    HtbDate = randomGenerator.DateTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    HasHtbDate = false,
                    HasTargetDateForTransfer = false,
                    HasTransferFirstDiscussedDate = false
                }
            };

            var expected = new AcademyTransferProjects
            {
                Id = academyTransferProject.Id,
                Urn = academyTransferProject.Urn,
                ProjectReference = academyTransferProject.ProjectReference,
                OutgoingTrustUkprn = academyTransferProject.OutgoingTrustUkprn,
                ProjectRationale = academyTransferProject.ProjectRationale,
                TransferringAcademies = academyTransferProject.TransferringAcademies,
                WhoInitiatedTheTransfer = academyTransferProject.WhoInitiatedTheTransfer,
                TargetDateForTransfer = null,
                RddOrEsfaIntervention = academyTransferProject.RddOrEsfaIntervention,
                RddOrEsfaInterventionDetail = academyTransferProject.RddOrEsfaInterventionDetail,
                TypeOfTransfer = academyTransferProject.TypeOfTransfer,
                OtherTransferTypeDescription = academyTransferProject.OtherTransferTypeDescription,
                TransferFirstDiscussed = null,
                HtbDate = null,
                TrustSponsorRationale = academyTransferProject.TrustSponsorRationale,
                State = academyTransferProject.State,
                Status = academyTransferProject.Status,
                Author = academyTransferProject.Author,
                Recommendation = academyTransferProject.Recommendation,
                AnyRisks = academyTransferProject.AnyRisks,
                HighProfileShouldBeConsidered = academyTransferProject.HighProfileShouldBeConsidered,
                HighProfileFurtherSpecification = academyTransferProject.HighProfileFurtherSpecification,
                ComplexLandAndBuildingShouldBeConsidered =
                    academyTransferProject.ComplexLandAndBuildingShouldBeConsidered,
                ComplexLandAndBuildingFurtherSpecification =
                    academyTransferProject.ComplexLandAndBuildingFurtherSpecification,
                FinanceAndDebtShouldBeConsidered = academyTransferProject.FinanceAndDebtShouldBeConsidered,
                FinanceAndDebtFurtherSpecification = academyTransferProject.FinanceAndDebtFurtherSpecification,
                OtherRisksShouldBeConsidered = academyTransferProject.OtherRisksShouldBeConsidered,
                OtherRisksFurtherSpecification = academyTransferProject.OtherRisksFurtherSpecification,
                OtherBenefitValue = academyTransferProject.OtherBenefitValue,
                FeatureSectionIsCompleted = academyTransferProject.FeatureSectionIsCompleted,
                BenefitsSectionIsCompleted = academyTransferProject.BenefitsSectionIsCompleted,
                RationaleSectionIsCompleted = academyTransferProject.RationaleSectionIsCompleted,
                AcademyTransferProjectIntendedTransferBenefits =
                    academyTransferProject.AcademyTransferProjectIntendedTransferBenefits,
                HasTransferFirstDiscussedDate = false,
                HasHtbDate = false,
                HasTargetDateForTransfer = false
            };

            var result = AcademyTransferProjectFactory.Update(academyTransferProject, updateRequest);

            result.Should().BeEquivalentTo(expected);
        }
    }
}