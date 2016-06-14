using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Summoner
{
    public class SummonerRankModel
    {
        public string queue { get; set; }
        public string name { get; set; }
        public List<SummonerRankEntryModel> entries { get; set; }
        public string tier { get; set; }
    }
}
