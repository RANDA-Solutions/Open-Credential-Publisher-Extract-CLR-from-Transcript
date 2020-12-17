using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class EvidenceType
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("artifacts")]
        public ArtifactType[] Artifacts { get; set; }
    }
}
