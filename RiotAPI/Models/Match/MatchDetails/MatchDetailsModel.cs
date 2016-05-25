using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Match.MatchDetails
{
    public class MatchDetailsModel
    {
        public int matchId { get; set; }
        public string region { get; set; }
        public string platformId { get; set; }
        public string matchMode { get; set; }
        public string matchType { get; set; }
        public long matchCreation { get; set; }
        public int matchDuration { get; set; }
        public string queueType { get; set; }
        public int mapId { get; set; }
        public string season { get; set; }
        public string matchVersion { get; set; }
        public List<ParticipantModel> participants { get; set; }
        public List<ParticipantIdentityModel> participantIdentities { get; set; }
        public List<TeamModel> teams { get; set; }
    }
}
