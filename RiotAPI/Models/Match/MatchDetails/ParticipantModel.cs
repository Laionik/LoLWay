using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Match.MatchDetails
{
    public class ParticipantModel 
    {
        public int teamId { get; set; }
        public int spell1Id { get; set; }
        public int spell2Id { get; set; }
        public int championId { get; set; }
        public string highestAchievedSeasonTier { get; set; }
        public TimelineModel timeline { get; set; }
        public List<MasteryModel> masteries { get; set; }
        public StatsModel stats { get; set; }
        public int participantId { get; set; }
        public List<RuneModel> runes { get; set; }
    }
}
