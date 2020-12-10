using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class DemographicType
    {
        [JsonProperty("@context")]
        public string Context { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("birthdate")]
        public DateTime Birthdate { get; set; }
    }
}
