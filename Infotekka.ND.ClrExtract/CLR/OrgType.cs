using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class OrgType
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("districtIds")]
        public DistrictIdType[] DistrictIds { get; set; }

        [JsonProperty("address")]
        public AddressType Address { get; set; }
    }
}