using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Match.MatchDetails
{
    public class CsDiffPerMinDeltasModel
    {
        public double zeroToTen { get; set; }
        public double tenToTwenty { get; set; }
        public double twentyToThirty { get; set; }
        public double thirtyToEnd { get; set; }
    }
}
