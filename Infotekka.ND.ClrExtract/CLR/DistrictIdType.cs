using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class DistrictIdType
    {
        [JsonProperty("districtIdentifier")]
        public string DistrictIdentifier { get; set; }

        [JsonProperty("districtIdentificationSystem")]
        public string DistrictIdentificationSystem { get; set; }
    }
}
