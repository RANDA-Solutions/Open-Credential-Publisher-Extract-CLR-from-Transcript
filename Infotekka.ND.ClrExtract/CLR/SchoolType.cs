using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class SchoolType
    {
        [JsonProperty("@context")]
        public string Context { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("principal")]
        public string Principal { get; set; }

        [JsonProperty("schoolId")]
        public SchoolIdType[] SchoolIds { get; set; }

        [JsonProperty("parentOrg")]
        public OrgType ParentOrg { get; set; }
    }
}
