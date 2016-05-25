using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.CurrentGame
{
    public class BannedChampionModel
    {
        public int pickTurn { get; set; }
        public int championId { get; set; }
        public int teamId { get; set; }
    }
}
