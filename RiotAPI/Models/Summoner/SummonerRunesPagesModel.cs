using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Summoner
{
    public class SummonerRunesPagesModel
    {
        public List<Models.Summoner.RunePageModel> pages { get; set; }
        public int summonerId { get; set; }
    }
}
