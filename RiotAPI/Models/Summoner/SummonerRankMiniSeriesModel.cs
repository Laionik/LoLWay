using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Summoner
{
    public class SummonerRankMiniSeriesModel
    {
        public int losses { get; set; }
        public String progress { get; set; }
        public int target { get; set; }
        public int wins { get; set; }

    }
}
