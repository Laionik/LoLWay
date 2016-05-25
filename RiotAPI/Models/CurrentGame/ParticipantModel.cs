using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.CurrentGame
{
    public class ParticipantModel
    {
        public List<Models.CurrentGame.MasteryModel> masteries { get; set; }
        public bool bot { get; set; }
        public List<Models.CurrentGame.RuneModel> runes { get; set; }
        public int spell2Id { get; set; }
        public int profileIconId { get; set; }
        public string summonerName { get; set; }
        public int championId { get; set; }
        public int teamId { get; set; }
        public int summonerId { get; set; }
        public int spell1Id { get; set; }
    }
}
