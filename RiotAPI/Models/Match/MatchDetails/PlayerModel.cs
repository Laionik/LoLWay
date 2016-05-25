using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Match.MatchDetails
{
    public class PlayerModel
    {
        public int summonerId { get; set; }
        public string summonerName { get; set; }
        public string matchHistoryUri { get; set; }
        public int profileIcon { get; set; }
    }
}
