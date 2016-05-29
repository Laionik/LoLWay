using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Summoner
{
    public class SummonerStatsModel
    {
        public int summonerId { get; set; }
        public long modifyDate { get; set; }
        public List<ChampionStatsModel> champions { get; set; }
    }
}
