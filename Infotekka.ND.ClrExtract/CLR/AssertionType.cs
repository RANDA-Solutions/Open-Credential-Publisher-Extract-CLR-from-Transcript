using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class AssertionType
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("achievement")]
        public AchievementType Achievement { get; set; }

        [JsonProperty("recipient")]
        public RecipientType Recipient { get; set; }

        [JsonProperty("creditsEarned")]
        public decimal? CreditsEarned { get; set; }

        [JsonProperty("term")]
        public string Term { get; set; }

        [JsonProperty("issuedOn")]
        public DateTime IssuedOn { get; set; }

        [JsonProperty("narrative")]
        public string Narrative { get; set; }

        [JsonProperty("results")]
        public ResultType[] Results { get; set; }
    }
}