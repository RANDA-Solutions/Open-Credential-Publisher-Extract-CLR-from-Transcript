using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class CourseIdType
    {
        [JsonProperty("courseIdentifier")]
        public string CourseIdentifier { get; set; }

        [JsonProperty("courseCodeSystem")]
        public string CourseCodeSystem { get; set; }
    }
}
