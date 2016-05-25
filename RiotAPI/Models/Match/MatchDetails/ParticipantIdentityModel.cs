using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.Match.MatchDetails
{
    public class ParticipantIdentityModel
    {
        public int participantId { get; set; }
        public PlayerModel player { get; set; }
    }
}
