using TramsDataApi.DatabaseModels;
using TramsDataApi.ResponseModels.EducationalPerformance;

namespace TramsDataApi.Factories.EducationPerformanceFactories
{
    public static class KeyStage5PerformanceResponseFactory
    {
        public static KeyStage5PerformanceResponse Create(SipEducationalperformancedata educationalPerformanceData,
            SipEducationalperformancedata nationalEducationPerformance,
            SipEducationalperformancedata localAuthorityEducationPerformance)
        {
            return new KeyStage5PerformanceResponse
            {
                Year = educationalPerformanceData.SipName,
                AcademicQualificationAverage = educationalPerformanceData.SipAcademicLevelAveragePspe,
                AppliedGeneralQualificationAverage = educationalPerformanceData.SipAppliedGeneralAveragePspe,
                NationalAcademicQualificationAverage = nationalEducationPerformance?.SipAcademicLevelAveragePspe,
                NationalAppliedGeneralQualificationAverage = nationalEducationPerformance?.SipAppliedGeneralAveragePspe,
                LAAcademicQualificationAverage = localAuthorityEducationPerformance?.SipAcademicLevelAveragePspe,
                LAAppliedGeneralQualificationAverage = localAuthorityEducationPerformance?.SipAppliedGeneralAveragePspe,
                AppliedGeneralProgress = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationalPerformanceData.SipAppliedGeneralProgress.ToString(),
                    Disadvantaged = educationalPerformanceData.SipAppliedGeneralProgressDisadvantaged.ToString()
                },
                AcademicProgress = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationalPerformanceData.SipAcademicProgress.ToString(),
                    Disadvantaged = educationalPerformanceData.SipAcademicProgressDisadvantaged.ToString()
                }
            };
        }
    }
}
