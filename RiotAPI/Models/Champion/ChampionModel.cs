using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Champion
{
    public class ChampionModel
    {
        public bool botMmEnabled { get; set; }
        public int id { get; set; }
        public bool rankedPlayEnabled { get; set; }
        public bool botEnabled { get; set; }
        public bool active { get; set; }
        public bool freeToPlay { get; set; }
    }
}
