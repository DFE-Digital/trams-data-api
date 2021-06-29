using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using FizzWare.NBuilder.PropertyNaming;
using FluentAssertions;
using Moq;
using TramsDataApi.DatabaseModels;
using TramsDataApi.Factories;
using TramsDataApi.Gateways;
using TramsDataApi.ResponseModels.EducationalPerformance;
using TramsDataApi.UseCases;
using Xunit;

namespace TramsDataApi.Test.UseCases
{
    public class GetKeyStagePerformanceByUrnTests
    {
        private RandomGenerator _randomGenerator;

        public GetKeyStagePerformanceByUrnTests()
        {
            _randomGenerator = new RandomGenerator();
        }
        
        [Fact]
        public void TestGetKeyStagePerformanceByUrn_ReturnsNull_WhenNoAccountIsFound()
        {
            var urn = "mockurn";
            var mockEducationPerformanceGateway = new Mock<IEducationPerformanceGateway>();
            mockEducationPerformanceGateway.Setup(gateway => gateway.GetAccountByUrn(urn)).Returns(() => null);
            var useCase = new GetKeyStagePerformanceByUrn(mockEducationPerformanceGateway.Object);

            useCase.Execute(urn).Should().BeNull();
        }
        
        [Fact]
        public void TestGetKeyStagePerformanceByUrn_ReturnsAEducationPerformanceResponse_WhenDataIsFound()
        {
            var urn = "123453";
            var guid = Guid.NewGuid();
            var year = "2017-2018";
            
            var account = Builder<Account>.CreateNew()
                .With(a => a.SipUrn = urn)
                .With(a => a.Id = guid)
                .Build();
            var phonics = Builder<SipPhonics>.CreateListOfSize(5)
                .All()
                .With(ph => ph.SipUrn = urn)
                .Build();

            var educationPerformanceData = Builder<SipEducationalperformancedata>.CreateNew()
                .With(epd => epd.SipParentaccountid = guid)
                .With(epd => epd.SipName = year)
                .With(epd => epd.SipMathsprogressscore = _randomGenerator.Int())
                .With(epd => epd.SipMathsprogressscoredisadv = _randomGenerator.Int())
                .With(epd => epd.SipReadingprogressscore = _randomGenerator.Int())
                .With(epd => epd.SipReadingprogressscoredisadv = _randomGenerator.Int())
                .With(epd => epd.SipWritingprogressscore = _randomGenerator.Int())
                .With(epd => epd.SipWritingprogressscoredisadv = _randomGenerator.Int())
                .With(epd => epd.SipMeetingexpectedstandardinrwm = _randomGenerator.Int())
                .With(epd => epd.SipMeetingexpectedstandardinrwmdisadv = _randomGenerator.Int())
                .With(epd => epd.SipMeetinghigherstandardinrwm = _randomGenerator.Int())
                .With(epd => epd.SipMeetinghigherstandardrwmdisadv = _randomGenerator.Int())
                .With(epd => epd.SipAttainment8score = _randomGenerator.Int())
                .With(epd => epd.SipAttainment8scoredisadvantaged = _randomGenerator.Int())
                .With(epd => epd.SipAttainment8scoreenglish = _randomGenerator.Int())
                .With(epd => epd.SipAttainment8scoreenglishdisadvantaged = _randomGenerator.Int())
                .With(epd => epd.SipAttainment8scoremaths = _randomGenerator.Int())
                .With(epd => epd.SipAttainment8scoremathsdisadvantaged = _randomGenerator.Int())
                .With(epd => epd.SipAttainment8scoreebacc = _randomGenerator.Int())
                .With(epd => epd.SipAttainment8scoreebaccdisadvantaged = _randomGenerator.Int())
                .With(epd => epd.SipNumberofpupilsprogress8 = _randomGenerator.Int())
                .With(epd => epd.SipNumberofpupilsprogress8disadvantaged = _randomGenerator.Int())
                .With(epd => epd.SipProgress8upperconfidence = _randomGenerator.Int())
                .With(epd => epd.SipProgress8lowerconfidence = _randomGenerator.Int())
                .With(epd => epd.SipProgress8english = _randomGenerator.Int())
                .With(epd => epd.SipProgress8englishdisadvantaged = _randomGenerator.Int())
                .With(epd => epd.SipProgress8maths = _randomGenerator.Int())
                .With(epd => epd.SipProgress8mathsdisadvantaged = _randomGenerator.Int())
                .With(epd => epd.SipProgress8ebacc = _randomGenerator.Int())
                .With(epd => epd.SipProgress8ebaccdisadvantaged = _randomGenerator.Int())
                .Build();

            var nationalEducationPerformanceData1 = Builder<SipEducationalperformancedata>.CreateNew()
                .With(nepd => nepd.SipParentaccountid = null)
                .With(nepd => nepd.SipPerformancetype = _randomGenerator.Int())
                .With(nepd => nepd.SipName = "2018-2019")
                .With(nepd => nepd.SipMathsprogressscore = _randomGenerator.Int())
                .With(nepd => nepd.SipMathsprogressscoredisadv = _randomGenerator.Int())
                .With(nepd => nepd.SipReadingprogressscore = _randomGenerator.Int())
                .With(nepd => nepd.SipReadingprogressscoredisadv = _randomGenerator.Int())
                .With(nepd => nepd.SipWritingprogressscore = _randomGenerator.Int())
                .With(nepd => nepd.SipWritingprogressscoredisadv = _randomGenerator.Int())
                .With(nepd => nepd.SipMeetingexpectedstandardinrwm = _randomGenerator.Int())
                .With(nepd => nepd.SipMeetingexpectedstandardinrwmdisadv = _randomGenerator.Int())
                .With(nepd => nepd.SipMeetinghigherstandardinrwm = _randomGenerator.Int())
                .With(nepd => nepd.SipMeetinghigherstandardrwmdisadv = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8score = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoredisadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoreenglish = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoreenglishdisadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoremaths = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoremathsdisadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoreebacc = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoreebaccdisadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipNumberofpupilsprogress8 = _randomGenerator.Int())
                .With(nepd => nepd.SipNumberofpupilsprogress8disadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8upperconfidence = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8lowerconfidence = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8english = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8englishdisadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8maths = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8mathsdisadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8ebacc = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8ebaccdisadvantaged = _randomGenerator.Int())
                .Build();
            
            var nationalEducationPerformanceData2 = Builder<SipEducationalperformancedata>.CreateNew()
                .With(nepd => nepd.SipParentaccountid = guid)
                .With(nepd => nepd.SipPerformancetype = _randomGenerator.Int())
                .With(nepd => nepd.SipName = year)
                .With(nepd => nepd.SipMathsprogressscore = _randomGenerator.Int())
                .With(nepd => nepd.SipMathsprogressscoredisadv = _randomGenerator.Int())
                .With(nepd => nepd.SipReadingprogressscore = _randomGenerator.Int())
                .With(nepd => nepd.SipReadingprogressscoredisadv = _randomGenerator.Int())
                .With(nepd => nepd.SipWritingprogressscore = _randomGenerator.Int())
                .With(nepd => nepd.SipWritingprogressscoredisadv = _randomGenerator.Int())
                .With(nepd => nepd.SipMeetingexpectedstandardinrwm = _randomGenerator.Int())
                .With(nepd => nepd.SipMeetingexpectedstandardinrwmdisadv = _randomGenerator.Int())
                .With(nepd => nepd.SipMeetinghigherstandardinrwm = _randomGenerator.Int())
                .With(nepd => nepd.SipMeetinghigherstandardrwmdisadv = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8score = null)
                .With(nepd => nepd.SipAttainment8scoredisadvantaged = null)
                .With(nepd => nepd.SipAttainment8scoreenglish = null)
                .With(nepd => nepd.SipAttainment8scoreenglishdisadvantaged = null)
                .With(nepd => nepd.SipAttainment8scoremaths = null)
                .With(nepd => nepd.SipAttainment8scoremathsdisadvantaged = null)
                .With(nepd => nepd.SipAttainment8scoreebacc = null)
                .With(nepd => nepd.SipAttainment8scoreebaccdisadvantaged = null)
                .With(nepd => nepd.SipNumberofpupilsprogress8 = _randomGenerator.Int())
                .With(nepd => nepd.SipNumberofpupilsprogress8disadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8upperconfidence = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8lowerconfidence = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8english = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8englishdisadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8maths = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8mathsdisadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8ebacc = _randomGenerator.Int())
                .With(nepd => nepd.SipProgress8ebaccdisadvantaged = _randomGenerator.Int())
                .Build();
            
            var nationalEducationPerformanceData3 = Builder<SipEducationalperformancedata>.CreateNew()
                .With(nepd => nepd.SipParentaccountid = guid)
                .With(nepd => nepd.SipPerformancetype = _randomGenerator.Int())
                .With(nepd => nepd.SipName = year)
                .With(nepd => nepd.SipMathsprogressscore = null)
                .With(nepd => nepd.SipMathsprogressscoredisadv = null)
                .With(nepd => nepd.SipReadingprogressscore = null)
                .With(nepd => nepd.SipReadingprogressscoredisadv = null)
                .With(nepd => nepd.SipWritingprogressscore = null)
                .With(nepd => nepd.SipWritingprogressscoredisadv = null)
                .With(nepd => nepd.SipMeetingexpectedstandardinrwm = null)
                .With(nepd => nepd.SipMeetingexpectedstandardinrwmdisadv = null)
                .With(nepd => nepd.SipMeetinghigherstandardinrwm = null)
                .With(nepd => nepd.SipMeetinghigherstandardrwmdisadv = null)
                .With(nepd => nepd.SipAttainment8score = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoredisadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoreenglish = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoreenglishdisadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoremaths = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoremathsdisadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoreebacc = _randomGenerator.Int())
                .With(nepd => nepd.SipAttainment8scoreebaccdisadvantaged = _randomGenerator.Int())
                .With(nepd => nepd.SipNumberofpupilsprogress8 = null)
                .With(nepd => nepd.SipNumberofpupilsprogress8disadvantaged = null)
                .With(nepd => nepd.SipProgress8upperconfidence = null)
                .With(nepd => nepd.SipProgress8lowerconfidence = null)
                .With(nepd => nepd.SipProgress8english = null)
                .With(nepd => nepd.SipProgress8englishdisadvantaged = null)
                .With(nepd => nepd.SipProgress8maths = null)
                .With(nepd => nepd.SipProgress8mathsdisadvantaged = null)
                .With(nepd => nepd.SipProgress8ebacc = null)
                .With(nepd => nepd.SipProgress8ebaccdisadvantaged = null)
                .Build();

            var nationalEducationPerformanceDataList = new List<SipEducationalperformancedata>
            {
                nationalEducationPerformanceData1,
                nationalEducationPerformanceData2,
                nationalEducationPerformanceData3

            };
            
            var mockEducationPerformanceGateway = new Mock<IEducationPerformanceGateway>();
            mockEducationPerformanceGateway.Setup(gateway => gateway.GetAccountByUrn(urn)).Returns(() => account);
            mockEducationPerformanceGateway.Setup(gateway => gateway.GetPhonicsByUrn(urn)).Returns(() => phonics);
            mockEducationPerformanceGateway.Setup(gateway => gateway.GetEducationalPerformanceForAccount(account)).Returns(() => new List<SipEducationalperformancedata> {educationPerformanceData});
            mockEducationPerformanceGateway.Setup(gateway => gateway.GetNationalEducationalPerformanceData()).Returns(nationalEducationPerformanceDataList);

            
            var expectedKs1 = phonics.Select(ph => new KeyStage1PerformanceResponse
            {
                Year = ph.SipYear,
                Reading = ph.SipKs1readingpercentageresults,
                Writing = ph.SipKs1writingpercentageresults,
                Maths = ph.SipKs1mathspercentageresults
            }).ToList();

            var expectedKs2 = new KeyStage2PerformanceResponse
            {
                Year = educationPerformanceData.SipName,
                PercentageMeetingExpectedStdInRWM = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipMeetingexpectedstandardinrwm.ToString(),
                    Disadvantaged = educationPerformanceData.SipMeetingexpectedstandardinrwmdisadv.ToString()
                },
                PercentageAchievingHigherStdInRWM = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipMeetinghigherstandardinrwm.ToString(),
                    Disadvantaged = educationPerformanceData.SipMeetinghigherstandardrwmdisadv.ToString()
                },
                ReadingProgressScore = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipReadingprogressscore.ToString(),
                    Disadvantaged = educationPerformanceData.SipReadingprogressscoredisadv.ToString()
                },
                WritingProgressScore = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipWritingprogressscore.ToString(),
                    Disadvantaged = educationPerformanceData.SipWritingprogressscoredisadv.ToString()
                },
                MathsProgressScore = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipMathsprogressscore.ToString(),
                    Disadvantaged = educationPerformanceData.SipMathsprogressscoredisadv.ToString()
                }
            };

            var expectedKs4 = new KeyStage4PerformanceResponse
            {
                Year = educationPerformanceData.SipName,
                SipAttainment8score = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipAttainment8score.ToString(),
                    Disadvantaged = educationPerformanceData.SipAttainment8scoredisadvantaged.ToString()
                },
                SipAttainment8scoreenglish = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipAttainment8scoreenglish.ToString(),
                    Disadvantaged = educationPerformanceData.SipAttainment8scoreenglishdisadvantaged.ToString()
                },
                SipAttainment8scoremaths = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipAttainment8scoremaths.ToString(),
                    Disadvantaged = educationPerformanceData.SipAttainment8scoremathsdisadvantaged.ToString()
                },
                SipAttainment8scoreebacc = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipAttainment8scoreebacc.ToString(),
                    Disadvantaged = educationPerformanceData.SipAttainment8scoreebaccdisadvantaged.ToString()
                },
                SipNumberofpupilsprogress8 = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipNumberofpupilsprogress8.ToString(),
                    Disadvantaged = educationPerformanceData.SipNumberofpupilsprogress8disadvantaged.ToString()
                },
                SipProgress8upperconfidence = educationPerformanceData.SipProgress8upperconfidence,
                SipProgress8lowerconfidence = educationPerformanceData.SipProgress8lowerconfidence,
                SipProgress8english = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipProgress8english.ToString(),
                    Disadvantaged = educationPerformanceData.SipProgress8englishdisadvantaged.ToString()
                },
                SipProgress8maths = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipProgress8maths.ToString(),
                    Disadvantaged = educationPerformanceData.SipProgress8mathsdisadvantaged.ToString()
                },
                SipProgress8ebacc = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipProgress8ebacc.ToString(),
                    Disadvantaged = educationPerformanceData.SipProgress8ebaccdisadvantaged.ToString()
                },
                SipProgress8Score = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = educationPerformanceData.SipProgress8score.ToString(),
                    Disadvantaged = educationPerformanceData.SipProgress8scoredisadvantaged.ToString()
                },
                NationalAverageA8Score = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = nationalEducationPerformanceData3?.SipAttainment8score.ToString(),
                    Disadvantaged = nationalEducationPerformanceData3?.SipAttainment8scoredisadvantaged.ToString()
                },
                NationalAverageA8English = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = nationalEducationPerformanceData3?.SipAttainment8scoreenglish.ToString(),
                    Disadvantaged = nationalEducationPerformanceData3?.SipAttainment8scoreenglishdisadvantaged.ToString()
                },
                NationalAverageA8Maths = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = nationalEducationPerformanceData3?.SipAttainment8scoremaths.ToString(),
                    Disadvantaged = nationalEducationPerformanceData3?.SipAttainment8scoremathsdisadvantaged.ToString()
                },
                NationalAverageA8EBacc = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = nationalEducationPerformanceData3?.SipAttainment8scoreebacc.ToString(),
                    Disadvantaged = nationalEducationPerformanceData3?.SipAttainment8scoreebaccdisadvantaged.ToString()
                },
                NationalAverageP8PupilsIncluded = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = nationalEducationPerformanceData2?.SipNumberofpupilsprogress8.ToString(),
                    Disadvantaged = nationalEducationPerformanceData2?.SipNumberofpupilsprogress8disadvantaged.ToString()
                },
                NationalAverageP8Score = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = nationalEducationPerformanceData2?.SipProgress8score.ToString(),
                    Disadvantaged = nationalEducationPerformanceData2?.SipProgress8scoredisadvantaged.ToString()
                },
                NationalAverageP8English = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = nationalEducationPerformanceData2?.SipProgress8english?.ToString(),
                    Disadvantaged = nationalEducationPerformanceData2?.SipProgress8englishdisadvantaged.ToString()
                },
                NationalAverageP8Maths = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = 
                        nationalEducationPerformanceData2?.SipProgress8maths?.ToString(),
                    Disadvantaged = nationalEducationPerformanceData2?.SipProgress8mathsdisadvantaged.ToString().ToString()
                },
                NationalAverageP8Ebacc = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = nationalEducationPerformanceData2?.SipProgress8ebacc.ToString(),
                    Disadvantaged = nationalEducationPerformanceData2?.SipProgress8ebaccdisadvantaged.ToString()
                },
                NationalAverageP8LowerConfidence = nationalEducationPerformanceData2?.SipProgress8lowerconfidence,
                NationalAverageP8UpperConfidence = nationalEducationPerformanceData2?.SipProgress8upperconfidence,
                NationalAverage = new DisadvantagedPupilsResponse
                {
                    NotDisadvantaged = null,
                    Disadvantaged = null
                }
            };
            
            var expected = new EducationalPerformanceResponse
            {
                SchoolName = account.Name,
                KeyStage1 = expectedKs1,
                KeyStage2 = new List<KeyStage2PerformanceResponse>{ expectedKs2 },
                KeyStage4 = new List<KeyStage4PerformanceResponse>{ expectedKs4 }
            };
            
            var useCase = new GetKeyStagePerformanceByUrn(mockEducationPerformanceGateway.Object);
            var result = useCase.Execute(urn);
            
            result.Should().BeEquivalentTo(expected);
        }
    }
}