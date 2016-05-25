using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Models.CurrentGame
{
    public class CurrentMatchModel
    {
        public int gameLength { get; set; }
        public string gameMode { get; set; }
        public int mapId { get; set; }
        public List<Models.CurrentGame.BannedChampionModel> bannedChampions { get; set; }
        public string gameType { get; set; }
        public int gameId { get; set; }
        public Models.CurrentGame.ObserversModel observers { get; set; }
        public int gameQueueConfigId { get; set; }
        public long gameStartTime { get; set; }
        public List<Models.CurrentGame.ParticipantModel> participants { get; set; }
        public string platformId { get; set; }
    }
}
