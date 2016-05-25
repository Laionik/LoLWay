using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Match
{
    public class MatchModel
    {
        public string region { get; set; }
        public string platformId { get; set; }
        public int matchId { get; set; }
        public int champion { get; set; }
        public string queue { get; set; }
        public string season { get; set; }
        public object timestamp { get; set; }
        public string lane { get; set; }
        public string role { get; set; }
    }
}
