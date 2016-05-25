using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Summoner
{
    public class RunePageModel
    {
        public int id { get; set; }
        public List<Models.Summoner.RuneModel> slots { get; set; }
        public string name { get; set; }
        public bool current { get; set; }
    }
}
