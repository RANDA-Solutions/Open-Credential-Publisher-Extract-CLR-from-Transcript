using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class StudentIdType
    {
        [JsonProperty("studentIdentifier")]
        public string StudentIdentifier { get; set; }

        [JsonProperty("studentIdentificationSystem")]
        public string StudentIdentificationSystem { get; set; }
    }
}
