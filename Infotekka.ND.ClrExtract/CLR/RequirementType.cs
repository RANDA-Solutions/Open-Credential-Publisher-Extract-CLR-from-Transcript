using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class RequirementType
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("narrative")]
        public string Narrative { get; set; }
    }
}
