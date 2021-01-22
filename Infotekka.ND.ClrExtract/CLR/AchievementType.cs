using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infotekka.ND.ClrExtract.CLR
{
    public class AchievementType
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("issuer")]
        public IssuerType Issuer { get; set; }

        [JsonProperty("achievementType")]
        public string TypeOfAchievement { get; set; }

        [JsonProperty("creditsAvailable")]
        public decimal? CreditsAvailable { get; set; }

        [JsonProperty("fieldOfStudy")]
        public string FieldOfStudy { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("requirement")]
        public RequirementType Requirement { get; set; }

        [JsonProperty("resultDescriptions")]
        public ResultDescriptionType[] ResultDescriptions { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("alignments")]
        public AlignmentType[] Alignments { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("identifiers")]
        public IdentifierType[] CourseIds { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}
