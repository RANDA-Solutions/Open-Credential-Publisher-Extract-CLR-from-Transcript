using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class ClrRoot
    {
        [JsonProperty("@context")]
        public string Context { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("issuedOn")]
        public DateTime IssuedOn { get; set; }

        [JsonProperty("partial")]
        public bool Partial { get; set; }

        [JsonProperty("learner")]
        public LearnerType Learner { get; set; }

        [JsonProperty("publisher")]
        public PublisherType Publisher { get; set; }

        [JsonProperty("assertions")]
        public AssertionType[] Assertions { get; set; }
    }
}
