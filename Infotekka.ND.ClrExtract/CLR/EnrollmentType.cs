using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class EnrollmentType
    {
        [JsonProperty("@context")]
        public string Context { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("currentGrade")]
        public string CurrentGrade { get; set; }

        [JsonProperty("graduationDate")]
        public DateTime? GraduationDate { get; set; }
    }
}
