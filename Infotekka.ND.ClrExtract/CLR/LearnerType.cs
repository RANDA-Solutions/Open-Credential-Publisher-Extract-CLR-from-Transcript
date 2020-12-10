using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class LearnerType : IssuerType
    {
        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("additionalName")]
        public string AdditionalName { get; set; }

        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("studentId")]
        public string StudentId { get; set; }

        [JsonProperty("hsx:Identification")]
        public IdentificationType Identification { get; set; }

        [JsonProperty("hsx:Enrollment")]
        public EnrollmentType Enrollment { get; set; }

        [JsonProperty("hsx:Demographic")]
        public DemographicType Demographic { get; set; }
    }
}
