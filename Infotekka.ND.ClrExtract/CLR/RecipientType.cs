using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class RecipientType
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("identity")]
        public string Identity { get; set; }
    }
}
