using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Match.MatchDetails
{
    public class TimelineModel
    {
        public CreepsPerMinDeltasModel creepsPerMinDeltas { get; set; }
        public XpPerMinDeltasModel xpPerMinDeltas { get; set; }
        public GoldPerMinDeltasModel goldPerMinDeltas { get; set; }
        public CsDiffPerMinDeltasModel csDiffPerMinDeltas { get; set; }
        public XpDiffPerMinDeltasModel xpDiffPerMinDeltas { get; set; }
        public DamageTakenPerMinDeltasModel damageTakenPerMinDeltas { get; set; }
        public DamageTakenDiffPerMinDeltasModel damageTakenDiffPerMinDeltas { get; set; }
        public string role { get; set; }
        public string lane { get; set; }
    }
}
