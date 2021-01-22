using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class OrgType : IssuerType
    {
        [JsonProperty("@context")]
        public string Context { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("official")]
        public string Official { get; set; }

        [JsonProperty("parentOrg")]
        public OrgType ParentOrg { get; set; }
    }
}