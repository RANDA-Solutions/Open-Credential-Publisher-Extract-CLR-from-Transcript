using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infotekka.ND.ClrExtract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infotekka.ND.ClrExtract.CLR;

namespace Infotekka.ND.ClrExtract.Tests
{
    [TestClass()]
    public class ConvertTests
    {
        [TestMethod()]
        public void ClrFromJsonTest() {
            string jsonData = System.IO.File.ReadAllText("../../../resources/sample clr.json");

            var model = Convert.ClrFromJson(jsonData);

            Assert.AreEqual("Student Transcript", model.Name);
            Assert.AreEqual("Student3 Demo", model.Learner.Name);
            Assert.AreEqual("Bismarck High School", model.Publisher.Name);
            Assert.IsTrue(model.Assertions.Length > 0);
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
    }
}