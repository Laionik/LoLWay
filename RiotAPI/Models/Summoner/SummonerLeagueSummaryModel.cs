using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Summoner
{
    public class SummonerLeagueSummaryModel
    {
        public string playerOrTeamId { get; set; }
        public string playerOrTeamName { get; set; }
        public string division { get; set; }
        public int leaguePoints { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public bool isHotStreak { get; set; }
        public bool isVeteran { get; set; }
        public bool isFreshBlood { get; set; }
        public bool isInactive { get; set; }
    }
}
