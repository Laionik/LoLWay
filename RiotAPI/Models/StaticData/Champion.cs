using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.StaticData
{
    public class Champion
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("key")]
        public string key { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("title")]
        public string title { get; set; }
    }
}
