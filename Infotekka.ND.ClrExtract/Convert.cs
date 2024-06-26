﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infotekka.ND.ClrExtract.CLR;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract
{
    public static class Convert
    {
        //const string context = "https://contexts.ward.guru/clr_v1p0.jsonld";

        public static ClrRoot ClrFromJson(string JsonData) {
            return JsonConvert.DeserializeObject<ClrRoot>(JsonData);
        }

        public static string JsonFromClr(ClrRoot ClrData) {
            return JsonConvert.SerializeObject(ClrData, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }

        /// <summary>
        /// Generate a CLR object with the provided transcript data
        /// </summary>
        /// <param name="SourcedId">The primary key of the CLR</param>
        /// <param name="IssuerId">The identifier of the issuer</param>
        /// <param name="RecipientId">The identifier of the recipient</param>
        /// <param name="Transcript">Transcript data</param>
        /// <param name="Courses">Course data</param>
        /// <returns><see cref="ClrRoot"/> object that can be converted to JSON</returns>
        public static ClrRoot GenerateClr(string ClrSourcedId, Guid IssuerId, Guid RecipientId, ITranscriptData Transcript, ICourseData[] Courses, IAssessmentData[] Assessments, byte[] TranscriptPdfData) {
            string base64Transcript = System.Convert.ToBase64String(TranscriptPdfData);
            Guid parentOrgId = Guid.NewGuid();

            Guid clrId = Guid.NewGuid();
            var recipient = new RecipientType() {
                Type = "id",
                Identity = $"urn:uuid:{RecipientId}",
                Hashed = false
            };
            DateTime issuedOn = DateTime.UtcNow;

            var publisher = new OrgType() {
                ID = $"urn:uuid:{IssuerId}",
                Name = Transcript.SchoolName,
                SourcedId = ClrSourcedId,
                Telephone = Transcript.SchoolPhone,
                Address = new AddressType() {
                    StreetAddress = (Transcript.SchoolAddress.Address1 + " " + Transcript.SchoolAddress.Address2).Trim(),
                    AddressRegion = Transcript.SchoolAddress.State,
                    AddressLocality = Transcript.SchoolAddress.City,
                    PostalCode = Transcript.SchoolAddress.Zip,
                    addressCountry = Transcript.SchoolAddress.Country
                },
                Description = "School",
                Official = Transcript.Principal,
                Identifiers = Transcript.SchoolIds.Select(s => new IdentifierType() {
                    Identifier = s.Identifier,
                    IdentifierTypeName = s.IdentificatierType
                }).ToArray(),
                ParentOrg = new OrgType() {
                    ID = $"urn:uuid:{parentOrgId}",
                    Description = "District",
                    Name = Transcript.DistrictName,
                    Address = new AddressType() {
                        StreetAddress = (Transcript.DistrictAddress.Address1 + " " + Transcript.SchoolAddress.Address2).Trim(),
                        AddressRegion = Transcript.DistrictAddress.State,
                        AddressLocality = Transcript.DistrictAddress.City,
                        PostalCode = Transcript.DistrictAddress.Zip,
                        addressCountry = Transcript.DistrictAddress.Country
                    }
                }
            };

            List<AssertionType> coreAssertions = new List<AssertionType>();

            //Transcript PDF
            if (TranscriptPdfData != null) {
                coreAssertions.Add(new AssertionType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    IssuedOn = issuedOn,
                    Achievement = new AchievementType() {
                        ID = $"urn:uuid:{Guid.NewGuid()}",
                        Name = "Transcript Document",
                        Issuer = publisher,
                        TypeOfAchievement = AchievementTypes.Transcript,
                        Description = "Transcript PDF"
                    },
                    Recipient = recipient,
                    Evidence = new EvidenceType[] {
                        new EvidenceType() {
                            Name = "Transcript",
                            Artifacts = new ArtifactType[] {
                                new ArtifactType() {
                                    Description = "PDF Transcript",
                                    Url = $"data:application/pdf;base64,{base64Transcript}"
                                }
                            }
                        }
                    }
                });
            }

            //Graduation
            if (Transcript.Graduated) {
                string gradId = $"urn:uuid:{Guid.NewGuid()}";
                coreAssertions.Add(new AssertionType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    IssuedOn = issuedOn,
                    Recipient = recipient,
                    Achievement = new AchievementType() {
                        ID = $"urn:uuid:{Guid.NewGuid()}",
                        Name = "Graduation",
                        Issuer = publisher,
                        TypeOfAchievement = AchievementTypes.Diploma,
                        Description = "High School Graduation",
                        Requirement = new RequirementType() {
                            ID = $"urn:uuid:{Guid.NewGuid()}",
                            Narrative = "Completion of graduation requirements"
                        },
                        ResultDescriptions = new ResultDescriptionType[] {
                            new ResultDescriptionType() {
                                ID = gradId,
                                Name = "Graduation Status",
                                ResultType = "Status"
                            }
                        }
                    },
                    Results = new ResultType[] {
                        new ResultType() {
                            ResultDescription = gradId,
                            Status = "Completed"
                        }
                    },
                    ActivityEndDate = Transcript.GraduationDate
                });
            }

            //Civics test
            if (Transcript.CivicsTest != null) {
                coreAssertions.Add(new AssertionType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    IssuedOn = issuedOn,
                    Recipient = recipient,
                    Achievement = new AchievementType() {
                        ID = $"urn:uuid:{Guid.NewGuid()}",
                        Name = "Civics Test",
                        Issuer = publisher,
                        TypeOfAchievement = AchievementTypes.Achievement,
                        Description = "Civics Test",
                        Requirement = new RequirementType() {
                            ID = $"urn:uuid{Guid.NewGuid()}",
                            Narrative = "Completion of civics test"
                        }
                    }
                });
            }

            //GPAs and Class Rank
            foreach (var g in Transcript.GPAs) {
                Guid resultId = Guid.NewGuid();
                var uwGpa = new AssertionType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    IssuedOn = issuedOn,
                    Recipient = recipient,
                    Achievement = new AchievementType() {
                        ID = $"urn:uuid:{Guid.NewGuid()}",
                        Name = g.Description,
                        Issuer = publisher,
                        TypeOfAchievement = AchievementTypes.Achievement,
                        Description = g.Description,
                        ResultDescriptions = new ResultDescriptionType[] {
                                new ResultDescriptionType() {
                                    ID = $"urn:uuid:{resultId}",
                                    Name = g.Description,
                                    ResultType = g.GPA != null ? "GradePointAverage" : "ext:Rank"
                                }
                            },
                        Tags = new string[] {
                                g.GPA != null ? "gpa" : "rank"
                            }
                    },
                    Results = new ResultType[] {
                            new ResultType() {
                                ResultDescription = $"urn:uuid:{resultId}",
                                Value = g.GPA != null ? String.Format("{0:0.0000}", g.GPA) : $"{g.ClassRank} of {g.ClassSize}"
                            }
                        }
                };
                coreAssertions.Add(uwGpa);
            }

            //Standard College Admission GPA
            Guid collegeGpaId = Guid.NewGuid();
            coreAssertions.Add(new AssertionType() {
                ID = $"urn:uuid:{Guid.NewGuid()}",
                IssuedOn = issuedOn,
                Recipient = recipient,
                Achievement = new AchievementType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    Name = "Standard College Admission GPA",
                    Issuer = publisher,
                    TypeOfAchievement = AchievementTypes.Achievement,
                    Description = "Based on a 4.0 Scale - Total GPA Points times potential credit, summed, and divided by sum of Potential Credids.",
                    ResultDescriptions = new ResultDescriptionType[] {
                        new ResultDescriptionType() {
                            ID = $"urn:uuid:{collegeGpaId}",
                            Name = "Standard College Admission GPA",
                            ResultType = "GradePointAverage"
                        }
                    },
                    Tags = new string[] {
                        "gpa"
                    }
                },
                Results = new ResultType[] {
                    new ResultType() {
                        ResultDescription = $"urn:uuid:{collegeGpaId}",
                        Value = String.Format("{0:0.0000}", Transcript.GpaWeightedCalc)
                    }
                }
            });

            //NDUS GPA
            Guid ndusGpaId = Guid.NewGuid();
            coreAssertions.Add(new AssertionType() {
                ID = $"urn:uuid:{Guid.NewGuid()}",
                IssuedOn = issuedOn,
                Recipient = recipient,
                Achievement = new AchievementType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    Name = "NDUS Core GPA",
                    Issuer = publisher,
                    TypeOfAchievement = AchievementTypes.Achievement,
                    Description = "NDUS Core GPA",
                    ResultDescriptions = new ResultDescriptionType[] {
                        new ResultDescriptionType() {
                            ID = $"urn:uuid:{ndusGpaId}",
                            Name = "NDUS Core GPA",
                            ResultType = "GradePointAverage"
                        }
                    },
                    Tags = new string[] {
                        "gpa"
                    }
                },
                Results = new ResultType[] {
                    new ResultType() {
                        ResultDescription = $"urn:uuid:{ndusGpaId}",
                        Value = String.Format("{0:0.0000}", Transcript.NdusGpa)
                    }
                }
            });

            //Enrollment ?
            if (Transcript.SchoolEntryDate != null) {
                coreAssertions.Add(new AssertionType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    IssuedOn = (DateTime)Transcript.SchoolEntryDate,
                    Recipient = recipient,
                    Achievement = new AchievementType() {
                        ID = $"urn:uuid:{Guid.NewGuid()}",
                        Name = "Enrollment",
                        Issuer = publisher,
                        TypeOfAchievement = AchievementTypes.SchoolEnrollment,
                        Description = "Enrollment Date"
                    }
                });
            }

            //Exit ?
            if (Transcript.SchoolExitDate != null) {
                coreAssertions.Add(new AssertionType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    IssuedOn = (DateTime)Transcript.SchoolExitDate,
                    Narrative = Transcript.WithdrawalReason,
                    Recipient = recipient,
                    Achievement = new AchievementType() {
                        ID = $"urn:uuid:{Guid.NewGuid()}",
                        Name = "Exit",
                        Issuer = publisher,
                        TypeOfAchievement = AchievementTypes.SchoolExit,
                        Description = "Exit Date / Reason"
                    }
                });
            }

            //NDUS Core Course Credit Totals
            var coreCredits = new AssertionType() {
                ID = $"urn:uuid:{Guid.NewGuid()}",
                Achievement = new AchievementType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    Name = "NDUS Core Course Credit Totals",
                    Issuer = publisher,
                    TypeOfAchievement = AchievementTypes.Achievement,
                    ResultDescriptions = null,
                    Tags = new string[] {
                        "credits"
                    }
                },
                Recipient = recipient,
                IssuedOn = issuedOn,
                Results = null
            };
            Dictionary<string, decimal> CreditsEarnedBySubjectArea = Courses.Where(w => w.CoreCourse).GroupBy(g => g.StateDescriptiveCode).ToDictionary(k => k.Key, v => v.Sum(s => s.CreditsReceived));
            List<ResultDescriptionType> coreCreditResultDesc = new List<ResultDescriptionType>();
            List<ResultType> coreCreditResults = new List<ResultType>();
            foreach (string key in CreditsEarnedBySubjectArea.Keys) {
                Guid coreCreditId = Guid.NewGuid();
                coreCreditResultDesc.Add(new ResultDescriptionType() {
                    ID = $"urn:uuid:{coreCreditId}",
                    Name = key,
                    ResultType = "ext:Credits"
                });
                coreCreditResults.Add(new ResultType() {
                    ResultDescription = $"urn:uuid:{coreCreditId}",
                    Value = string.Format("{0:0.000}", CreditsEarnedBySubjectArea[key])
                });
            }
            coreCredits.Achievement.ResultDescriptions = coreCreditResultDesc.ToArray();
            coreCredits.Results = coreCreditResults.ToArray();
            coreAssertions.Add(coreCredits);

            //High School Course Credit Totals
            Dictionary<string, decimal> CreditsEarnedByGrade = Courses.GroupBy(g => g.GradeLevel).ToDictionary(k => k.Key, v => v.Sum(s => s.CreditsReceived));
            var creditTotals = new AssertionType() {
                ID = $"urn:uuid:{Guid.NewGuid()}",
                Achievement = new AchievementType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    Name = "High School Course Credit Totals",
                    Issuer = publisher,
                    TypeOfAchievement = AchievementTypes.Achievement,
                    ResultDescriptions = null,
                    Tags = new string[] {
                        "credits"
                    }
                },
                Recipient = recipient,
                IssuedOn = issuedOn,
                Results = null
            };
            List<ResultDescriptionType> creditDesc = new List<ResultDescriptionType>();
            List<ResultType> creditResults = new List<ResultType>();
            Guid creditId;
            foreach (string g in CreditsEarnedByGrade.Keys) {
                creditId = Guid.NewGuid();
                creditDesc.Add(new ResultDescriptionType() {
                    ID = $"urn:uuid:{creditId}",
                    Name = $"{g}th",
                    ResultType = "ext:Credits"
                });
                creditResults.Add(new ResultType() {
                    ResultDescription = $"urn:uuid:{creditId}",
                    Value = String.Format("{0:0.000}", CreditsEarnedByGrade[g])
                });
            }
            //Total Earned
            decimal creditsEarned = CreditsEarnedByGrade.Sum(v => v.Value);
            creditId = Guid.NewGuid();
            creditDesc.Add(new ResultDescriptionType() {
                ID = $"urn:uuid:{creditId}",
                Name = "Total Earned",
                ResultType = "ext:Credits"
            });
            creditResults.Add(new ResultType() {
                ResultDescription = $"urn:uuid:{creditId}",
                Value = String.Format("{0:0.000}", creditsEarned)
            });
            //Total Attempted
            decimal creditsAttempted = Courses.Sum(s => s.CreditsAttempted);
            creditId = Guid.NewGuid();
            creditDesc.Add(new ResultDescriptionType() {
                ID = $"urn:uuid:{creditId}",
                Name = "Total Attempted",
                ResultType = "ext:Credits"
            });
            creditResults.Add(new ResultType() {
                ResultDescription = $"urn:uuid:{creditId}",
                Value = String.Format("{0:0.000}", creditsAttempted)
            });
            creditTotals.Achievement.ResultDescriptions = creditDesc.ToArray();
            creditTotals.Results = creditResults.ToArray();
            coreAssertions.Add(creditTotals);

            foreach (var a in Assessments) {
                Dictionary<Guid, KeyValuePair<string, decimal>> dIds = a
                    .CategoryScores
                    .ToDictionary(k => Guid.NewGuid(), v => new KeyValuePair<string, decimal>(v.Key, v.Value));

                var assessmentResults = new AssertionType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    Achievement = new AchievementType() {
                        ID = $"urn:uuid:{Guid.NewGuid()}",
                        Name = a.Name,
                        Description = a.Description,
                        Issuer = publisher,
                        TypeOfAchievement = AchievementTypes.Achievement,
                        ResultDescriptions = dIds
                            .Select(s => new ResultDescriptionType() {
                                ID = $"urn:uuid:{s.Key}",
                                Name = s.Value.Key,
                                ResultType = "Result"
                            })
                            .ToArray(),
                        Tags = new string[] {
                            a.AssessmentType,
                            "Unofficial"
                        }
                    },
                    Recipient = recipient,
                    IssuedOn = a.DateTaken,
                    Results = dIds
                        .Select(s => new ResultType() {
                            ResultDescription = $"urn:uuid:{s.Key}",
                            Value = $"{s.Value.Value}"
                        })
                        .ToArray()
                };
                coreAssertions.Add(assessmentResults);
            }

            //Awards
            foreach(var a in Transcript.Awards) {
                coreAssertions.Add(new AssertionType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    IssuedOn = a.Awarded,
                    Recipient = recipient,
                    Achievement = new AchievementType() {
                        ID = $"urn:uuid:{Guid.NewGuid()}",
                        Name = a.Name,
                        Issuer = publisher,
                        TypeOfAchievement = AchievementTypes.Award,
                        Description = a.Description,
                        Image = a.Image
                    }
                });
            }

            //Build CLR
            var clr = new ClrRoot() {
                //Context = context,
                ID = $"urn:uuid:{clrId}",
                Name = "Student Transcript",
                Partial = true,
                IssuedOn = issuedOn,
                Learner = new LearnerType() {
                    ID = $"urn:uuid:{RecipientId}",
                    Name = $"{Transcript.FirstName} {Transcript.LastName}",
                    GivenName = Transcript.FirstName,
                    AdditionalName = Transcript.MiddleName,
                    FamilyName = Transcript.LastName,
                    SourcedId = Transcript.SourcedId,
                    StudentId = Transcript.StudentId,
                    Identifiers = Transcript.StudentIds.Select(s => new IdentifierType() {
                        Identifier = s.Identifier,
                        IdentifierTypeName = s.IdentificatierType
                    }).ToArray(),
                    Telephone = Transcript.StudentPhone,
                    Address = new AddressType() {
                        StreetAddress = (Transcript.StudentAddress.Address1 + " " + Transcript.StudentAddress.Address2).Trim(),
                        AddressRegion = Transcript.StudentAddress.State,
                        AddressLocality = Transcript.StudentAddress.City,
                        PostalCode = Transcript.StudentAddress.Zip,
                        addressCountry = Transcript.StudentAddress.Country
                    },
                    //Enrollment = new EnrollmentType() {
                    //    //Context = context,
                    //    Type = "Enrollment",
                    //    CurrentGrade = Transcript.GradeLevel,
                    //    GraduationDate = Transcript.GraduationDate
                    //},
                    Birthdate = Transcript.DateOfBirth
                },
                Publisher = publisher,
                Assertions = coreAssertions
                    .Union(Courses.Select(s => courseAssertion(s, publisher, recipient)))
                    .ToArray()
            };

            return clr;
        }

        private static AssertionType courseAssertion(ICourseData Course, OrgType Publisher, RecipientType Recipient) {
            var ca = new AssertionType() {
                ID = $"urn:uuid:{Guid.NewGuid()}",
                Recipient = Recipient,
                CreditsEarned = Course.CreditsReceived,
                Term = Course.TermName,
                IssuedOn = Course.DateAwarded,
                Achievement = new AchievementType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    Name = Course.CourseTitle,
                    Issuer = Publisher,
                    TypeOfAchievement = AchievementTypes.Course,
                    CreditsAvailable = Course.CreditsAttempted,
                    FieldOfStudy = Course.StateSubjectDesc,
                    Level = Course.GradeLevel,
                    ResultDescriptions = null,
                    Tags = null,
                    Alignments = new AlignmentType[] {
                        new AlignmentType() {
                            TargetCode = Course.StateCourseId,
                            TargetName = Course.CourseTitle,
                            TargetUrl = Course.CourseUri,
                            TargetType = "CFItem",
                            TargetFramework = "North Dakota State Course Codes"
                        }
                    }
                },
                Results = null,
                Verification = new VerificationType() {
                    Type = VerificationTypes.Signed
                }
            };

            Guid resultId = Guid.NewGuid();
            ca.Results = new ResultType[] {
                new ResultType() {
                    ResultDescription = $"urn:uuid:{resultId}",
                    Value = Course.LetterGrade
                }
            };
            ca.Achievement.ResultDescriptions = new ResultDescriptionType[] {
                new ResultDescriptionType() {
                    ID = $"urn:uuid:{resultId}",
                    Name = "Term Grade",
                    ResultType = "LetterGrade"
                }
            };

            List<string> tags = new List<string>();
            if (Course.DualCredit) {
                tags.Add("DC");
            }
            if (Course.ApCourse) {
                tags.Add("AP");
            }
            if (Course.CoreCourse) {
                tags.Add("Core");
            }
            if (Course.CteCourse) {
                tags.Add("CTE");
            }

            ca.Achievement.Tags = tags.ToArray();

            List<IdentifierType> courseIds = new List<IdentifierType>();
            if (!String.IsNullOrEmpty(Course.LocalCourseId)) {
                courseIds.Add(new IdentifierType() {
                    Identifier = Course.LocalCourseId,
                    IdentifierTypeName = IdentifierTypes.SeaCourseId
                });
            };
            if (!String.IsNullOrEmpty(Course.StateCourseId)) {
                courseIds.Add(new IdentifierType() {
                    Identifier = Course.StateCourseId,
                    IdentifierTypeName = IdentifierTypes.SeaCourseId
                });
            }
            if (!String.IsNullOrEmpty(Course.NationalCourseId)) {
                courseIds.Add(new IdentifierType() {
                    Identifier = Course.NationalCourseId,
                    IdentifierTypeName = IdentifierTypes.NcesCourseId
                });
            }
            ca.Achievement.CourseIds = courseIds.ToArray();

            return ca;
        }

        public static class VerificationTypes
        {
            public const string Signed = "Signed";
        }

        private static class AchievementTypes
        {
            public const string Diploma = "Diploma";
            public const string Achievement = "Achievement";
            public const string Course = "Course";
            public const string Transcript = "ext:Transcript";
            public const string SchoolEnrollment = "ext:SchoolEnrollment";
            public const string SchoolExit = "ext:SchoolExit";
            public const string Award = "Award";
        }

        public static class IdentifierTypes
        {
            public const string LeaSchoolId = "ext:LeaSchoollId";
            public const string SeaSchoolId = "ext:SeaSchoolId";
            public const string NcesSchoolId = "ext:NcesSchoolId";

            public const string LeaStudentId = "ext:LeaStudentlId";
            public const string SeaStudentId = "ext:SeaStudentId";
            public const string NdheStudentId = "ext:NDHEStudentId";

            public const string LeaCourseId = "ext:LeaCourselId";
            public const string SeaCourseId = "ext:SeaCourseId";
            public const string NcesCourseId = "ext:NcesCourseId";
        }
    }
}