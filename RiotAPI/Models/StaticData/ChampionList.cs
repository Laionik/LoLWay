using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.StaticData
{
    public class ChampionList
    {
        [JsonProperty("data")]
        public Dictionary<string, Models.StaticData.Champion> Champions { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
