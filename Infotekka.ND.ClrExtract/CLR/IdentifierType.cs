using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class IdentifierType
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("identifierType")]
        public string IdentifierTypeName { get; set; }
    }
}