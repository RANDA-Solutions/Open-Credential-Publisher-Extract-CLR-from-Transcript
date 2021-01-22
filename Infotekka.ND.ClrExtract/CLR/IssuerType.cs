using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class IssuerType
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sourcedId")]
        public string SourcedId { get; set; }

        [JsonProperty("address")]
        public AddressType Address { get; set; }

        [JsonProperty("telephone")]
        public string Telephone { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("identifiers")]
        public IdentifierType[] Identifiers { get; set; }

        [JsonProperty("verification")]
        public string Verification { get; set; }
    }
}
