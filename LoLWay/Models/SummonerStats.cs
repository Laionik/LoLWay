using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoLWay.Models
{
    public class SummonerStats
    {
        public int summonerId { get; set; }
        public string summonerName { get; set; }
        public string spell1 { get; set; }
        public string spell2 { get; set; }
        public string champion { get; set; }
        public List<string> runes{ get; set; }
        public List<string> masteries { get; set; }
        public double averageKills { get; set; }
        public double averageAssists { get; set; }
        public double averageDeaths { get; set; }
        public string division { get; set; }
        public SummonerStats(int summonerId, string summonerName, string spell1, string spell2, string champion, string division)
        {
            this.summonerId = summonerId;
            this.summonerName = summonerName;
            this.spell1 = spell1;
            this.spell2 = spell2;
            this.champion = champion;
            this.division = division;
        }
    }

    public class CurrentGameBan
    {
        public int team { get; set; }
        public string banName { get; set; }

        public CurrentGameBan(int team, string banName)
        {
            this.team = team;
            this.banName = banName;
        }
    }
}