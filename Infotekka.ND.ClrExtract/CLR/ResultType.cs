using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class ResultType
    {
        [JsonProperty("resultDescription")]
        public string ResultDescription { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
