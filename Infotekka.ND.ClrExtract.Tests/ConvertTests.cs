using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infotekka.ND.ClrExtract;
using System;
using System.Collections.Generic;
using System.Text;
using Infotekka.ND.ClrExtract.CLR;
using Infotekka.ND.ClrExtract.Tests.CLR;

namespace Infotekka.ND.ClrExtract.Tests
{
    [TestClass()]
    public class ConvertTests
    {
        [TestMethod()]
        public void GenerateClrTest() {
            string sourceId = Guid.NewGuid().ToString();

            var t = new TranscriptData() {
                FirstName = "test",
                LastName = "user",
                GPAs = new GpaClassRankData[0],
                DistrictAddress = new AddressData(),
                SchoolAddress = new AddressData(),
                SchoolIds = new IdentityData[0],
                StudentIds = new IdentityData[0],
                StudentAddress = new AddressData()
            };

            var c = new CourseData[] {
                new CourseData() {
                    CourseTitle = "test course",
                    GradeLevel = "11"
                },
                new CourseData() {
                    CourseTitle = "another test",
                    GradeLevel = "11"
                }
            };

            var issuerId = Guid.NewGuid();
            var recipientId = Guid.NewGuid();

            var clr = Convert.GenerateClr(sourceId, issuerId, recipientId, t, c);

            Assert.IsNotNull(clr);
            Assert.AreEqual("test user", clr.Learner.Name);
        }

        [TestMethod()]
        public void JsonFromClrTest() {
            Guid id = Guid.NewGuid();
            string name = "Test CLR Student";

            var clrData = new ClrRoot() {
                Context = "https://contexts.ward.guru/clr_v1p0.jsonld",
                ID = $"urn:uuid:{id}",
                Name = name,
                Partial = true,
                Learner = new LearnerType() {
                    ID = $"urn:uuid:{Guid.NewGuid()}",
                    Name = name,
                    SourcedId = "1234567",
                    StudentId = "0987654321"
                }
            };

            string jsonData = Convert.JsonFromClr(clrData);

            Assert.IsNotNull(jsonData);
        }

        [TestMethod()]
        public void ClrFromJsonTest() {
            string jsonData = System.IO.File.ReadAllText("../../../resources/sample clr.json");

            var model = Convert.ClrFromJson(jsonData);

            Assert.AreEqual("Student Transcript", model.Name);
            Assert.AreEqual("Student3 Demo", model.Learner.Name);
            Assert.AreEqual("Bismarck High School", model.Publisher.Name);
            Assert.IsTrue(model.Assertions.Length > 0);
        }
    }
}