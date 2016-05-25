using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Summoner
{
    public class SummonerMysteryPagesModel
    {
        public List<Models.Summoner.MasteryPageModel> pages { get; set; }
        public int summonerId { get; set; }
    }
}
