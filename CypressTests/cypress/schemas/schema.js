export const applyToBecomeAcademySchema = {
  type: 'object',
  properties: {
    data: {
      type: 'object',
      properties: {
        name: {
          type: 'string'
        },
        applicationId: {
          type: 'string'
        },
        applicationType: {
          type: 'string'
        },
        trustId: {
          type: 'string'
        },
        formTrustProposedNameOfTrust: {
          type: 'string'
        },
        applicationSubmitted: {
          type: 'boolean'
        },
        applicationLeadAuthorId: {
          type: 'string'
        },
        applicationLeadEmail: {
          type: 'string'
        },
        applicationVersion: {
          type: 'string'
        },
        applicationLeadAuthorName: {
          type: 'string'
        },
        applicationRole: {
          type: 'string'
        },
        applicationRoleOtherDescription: {
          type: 'string'
        },
        changesToTrust: {
          type: 'boolean'
        },
        changesToTrustExplained: {
          type: 'string'
        },
        changesToLaGovernance: {
          type: 'boolean'
        },
        changesToLaGovernanceExplained: {
          type: 'string'
        },
        formTrustOpeningDate: {
          type: 'string'
        },
        trustApproverName: {
          type: 'string'
        },
        trustApproverEmail: {
          type: 'string'
        },
        formTrustReasonApprovalToConvertAsSat: {
          type: 'boolean'
        },
        formTrustReasonApprovedPerson: {
          type: 'string'
        },
        formTrustReasonForming: {
          type: 'string'
        },
        formTrustReasonVision: {
          type: 'string'
        },
        formTrustReasonGeoAreas: {
          type: 'string'
        },
        formTrustReasonFreedom: {
          type: 'string'
        },
        formTrustReasonImproveTeaching: {
          type: 'string'
        },
        formTrustPlanForGrowth: {
          type: 'string'
        },
        formTrustPlansForNoGrowth: {
          type: 'string'
        },
        formTrustGrowthPlansYesNo: {
          type: 'boolean'
        },
        formTrustImprovementSupport: {
          type: 'string'
        },
        formTrustImprovementStrategy: {
          type: 'string'
        },
        formTrustImprovementApprovedSponsor: {
          type: 'string'
        },
        applicationStatusId: {
          type: 'string'
        },
        keyPersons: {
          type: 'array',
          items: [
            {
              type: 'object',
              properties: {
                name: {
                  type: 'string'
                },
                keyPersonDateOfBirth: {
                  type: 'string'
                },
                keyPersonBiography: {
                  type: 'string'
                },
                keyPersonCeoExecutive: {
                  type: 'boolean'
                },
                keyPersonChairOfTrust: {
                  type: 'boolean'
                },
                keyPersonFinancialDirector: {
                  type: 'boolean'
                },
                keyPersonFinancialDirectorTime: {
                  type: 'string'
                },
                keyPersonMember: {
                  type: 'string'
                },
                keyPersonOther: {
                  type: 'string'
                },
                keyPersonTrustee: {
                  type: 'string'
                }
              }
            }
          ]
        },
        applyingSchools: {
          type: 'array',
          items: [
            {
              type: 'object',
              properties: {
                schoolName: {
                  type: 'string'
                },
                schoolConversionContactHeadName: {
                  type: 'string'
                },
                schoolConversionContactHeadEmail: {
                  type: 'string'
                },
                schoolConversionContactHeadTel: {
                  type: 'string'
                },
                schoolConversionContactChairName: {
                  type: 'string'
                },
                schoolConversionContactChairEmail: {
                  type: 'string'
                },
                schoolConversionContactChairTel: {
                  type: 'string'
                },
                schoolConversionContactRole: {
                  type: 'string'
                },
                schoolConversionMainContactOtherName: {
                  type: 'string'
                },
                schoolConversionMainContactOtherEmail: {
                  type: 'string'
                },
                schoolConversionMainContactOtherTelephone: {
                  type: 'string'
                },
                schoolConversionMainContactOtherRole: {
                  type: 'string'
                },
                schoolConversionApproverContactName: {
                  type: 'string'
                },
                schoolConversionApproverContactEmail: {
                  type: 'string'
                },
                schoolConversionTargetDateSpecified: {
                  type: 'boolean'
                },
                schoolConversionTargetDate: {
                  type: 'string'
                },
                schoolConversionTargetDateExplained: {
                  type: 'string'
                },
                schoolConversionReasonsForJoining: {
                  type: 'string'
                },
                schoolConversionChangeNamePlanned: {
                  type: 'boolean'
                },
                schoolConversionProposedNewSchoolName: {
                  type: 'string'
                },
                schoolAdSchoolContributionToTrust: {
                  type: 'string'
                },
                schoolOngoingSafeguardingInvestigations: {
                  type: 'boolean'
                },
                schoolOngoingSafeguardingDetails: {
                  type: 'string'
                },
                schoolPartOfLaReorganizationPlan: {
                  type: 'boolean'
                },
                schoolLaReorganizationDetails: {
                  type: 'string'
                },
                schoolPartOfLaClosurePlan: {
                  type: 'boolean'
                },
                schoolLaClosurePlanDetails: {
                  type: 'string'
                },
                schoolFaithSchool: {
                  type: 'boolean'
                },
                schoolFaithSchoolDioceseName: {
                  type: 'string'
                },
                diocesePermissionEvidenceDocumentLink: {
                  type: 'string'
                },
                schoolIsPartOfFederation: {
                  type: 'boolean'
                },
                schoolIsSupportedByFoundation: {
                  type: 'boolean'
                },
                schoolSupportedFoundationBodyName: {
                  type: 'string'
                },
                foundationEvidenceDocumentLink: {
                  type: 'string'
                },
                schoolHasSACREException: {
                  type: 'boolean'
                },
                schoolSACREExemptionEndDate: {
                  type: 'string'
                },
                schoolAdFeederSchools: {
                  type: 'string'
                },
                governingBodyConsentEvidenceDocumentLink: {
                  type: 'string'
                },
                schoolAdEqualitiesImpactAssessmentCompleted: {
                  type: 'boolean'
                },
                schoolAdEqualitiesImpactAssessmentDetails: {
                  type: 'string'
                },
                schoolAdInspectedButReportNotPublished: {
                  type: 'boolean'
                },
                schoolAdInspectedButReportNotPublishedExplain: {
                  type: 'string'
                },
                schoolAdditionalInformationAdded: {
                  type: 'boolean'
                },
                schoolAdditionalInformation: {
                  type: 'string'
                },
                previousFinancialYear: {
                  type: 'object',
                  properties: {
                    fyEndDate: {
                      type: 'string'
                    },
                    revenueCarryForward: {
                      type: 'number'
                    },
                    revenueIsDeficit: {
                      type: 'boolean'
                    },
                    revenueStatusExplained: {
                      type: 'string'
                    },
                    capitalCarryForward: {
                      type: 'number'
                    },
                    capitalIsDeficit: {
                      type: 'boolean'
                    },
                    capitalStatusExplained: {
                      type: 'string'
                    }
                  }
                },
                currentFinancialYear: {
                  type: 'object',
                  properties: {
                    fyEndDate: {
                      type: 'string'
                    },
                    revenueCarryForward: {
                      type: 'number'
                    },
                    revenueIsDeficit: {
                      type: 'boolean'
                    },
                    revenueStatusExplained: {
                      type: 'string'
                    },
                    capitalCarryForward: {
                      type: 'number'
                    },
                    capitalIsDeficit: {
                      type: 'boolean'
                    },
                    capitalStatusExplained: {
                      type: 'string'
                    }
                  }
                },
                nextFinancialYear: {
                  type: 'object',
                  properties: {
                    fyEndDate: {
                      type: 'string'
                    },
                    revenueCarryForward: {
                      type: 'number'
                    },
                    revenueIsDeficit: {
                      type: 'boolean'
                    },
                    revenueStatusExplained: {
                      type: 'string'
                    },
                    capitalCarryForward: {
                      type: 'number'
                    },
                    capitalIsDeficit: {
                      type: 'boolean'
                    },
                    capitalStatusExplained: {
                      type: 'string'
                    }
                  }
                },
                financeOngoingInvestigations: {
                  type: 'boolean'
                },
                schoolFinancialInvestigationsExplain: {
                  type: 'string'
                },
                schoolFinancialInvestigationsTrustAware: {
                  type: 'boolean'
                },
                schoolCapacityYear1: {
                  type: 'integer'
                },
                schoolCapacityYear2: {
                  type: 'integer'
                },
                schoolCapacityYear3: {
                  type: 'integer'
                },
                schoolCapacityAssumptions: {
                  type: 'string'
                },
                schoolCapacityPublishedAdmissionsNumber: {
                  type: 'integer'
                },
                schoolBuildLandOwnerExplained: {
                  type: 'string'
                },
                schoolBuildLandWorksPlanned: {
                  type: 'boolean'
                },
                schoolBuildLandWorksPlannedExplained: {
                  type: 'string'
                },
                schoolBuildLandWorksPlannedCompletionDate: {
                  type: 'string'
                },
                schoolBuildLandSharedFacilities: {
                  type: 'boolean'
                },
                schoolBuildLandSharedFacilitiesExplained: {
                  type: 'string'
                },
                schoolBuildLandGrants: {
                  type: 'boolean'
                },
                schoolBuildLandGrantsExplained: {
                  type: 'string'
                },
                schoolBuildLandPFIScheme: {
                  type: 'boolean'
                },
                schoolBuildLandPFISchemeType: {
                  type: 'string'
                },
                schoolBuildLandPriorityBuildingProgramme: {
                  type: 'boolean'
                },
                schoolBuildLandFutureProgramme: {
                  type: 'boolean'
                },
                schoolSupportGrantFundsPaidTo: {
                  type: 'string'
                },
                schoolHasConsultedStakeholders: {
                  type: 'boolean'
                },
                schoolPlanToConsultStakeholders: {
                  type: 'string'
                },
                declarationBodyAgree: {
                  type: 'boolean'
                },
                declarationIAmTheChairOrHeadteacher: {
                  type: 'boolean'
                },
                declarationSignedByName: {
                  type: 'string'
                },
                schoolLoans: {
                  type: 'array',
                  items: [
                    {
                      type: 'object',
                      properties: {
                        schoolLoanAmount: {
                          type: 'number'
                        },
                        schoolLoanPurpose: {
                          type: 'string'
                        },
                        schoolLoanProvider: {
                          type: 'string'
                        },
                        schoolLoanInterestRate: {
                          type: 'string'
                        },
                        schoolLoanSchedule: {
                          type: 'string'
                        }
                      }
                    }
                  ]
                },
                schoolLeases: {
                  type: 'array',
                  items: [
                    {
                      type: 'object',
                      properties: {
                        schoolLeaseTerm: {
                          type: 'string'
                        },
                        schoolLeaseRepaymentValue: {
                          type: 'number'
                        },
                        schoolLeaseInterestRate: {
                          type: 'number'
                        },
                        schoolLeasePaymentToDate: {
                          type: 'number'
                        },
                        schoolLeasePurpose: {
                          type: 'string'
                        },
                        schoolLeaseValueOfAssets: {
                          type: 'string'
                        },
                        schoolLeaseResponsibleForAssets: {
                          type: 'string'
                        }
                      }
                    }
                  ]
                }
              },
                required: [
                    'previousFinancialYear',
                    'currentFinancialYear',
                    'nextFinancialYear'
                ]
            }
          ]
        }
      }
    }
  },
  required: [
    'data'
  ]
}