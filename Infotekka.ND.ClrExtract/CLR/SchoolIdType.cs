using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class SchoolIdType
    {
        [JsonProperty("schoolIdentifier")]
        public string StudentIdentifier { get; set; }

        [JsonProperty("schoolIdentificationSystem")]
        public string StudentIdentificationSystem { get; set; }
    }
}