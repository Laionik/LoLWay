using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Summoner
{
    public class MasteryPageModel
    {
        public List<Models.Summoner.MasteryModel> masteries { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public bool current { get; set; }
    }
}
