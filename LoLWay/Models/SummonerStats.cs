using RiotAPI.Models.Match.MatchDetails;
using RiotAPI.Models.Summoner;
using RiotAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using LoLWay.Helpers;

namespace LoLWay.Models
{
    public class SummonerModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int summonerLevel { get; set; }
        public string summonerDivision { get; set; }
        public ChampionStatsModel mostPlayedChampion { get; set; }
        public ChampionStatsModel totalStats { get; set; }
        public List<SummonerMatchStats> matchStatsLsist { get; set; }
        //ogólne statystyki

        public SummonerModel(int id, string name, int summonerLevel, string summonerDivision, ChampionStatsModel mostPlayedChampion, ChampionStatsModel totalStats, List<SummonerMatchStats> matchStatsLsist)
        {
            this.id = id;
            this.name = name;
            this.summonerLevel = summonerLevel;
            this.summonerDivision = summonerDivision;
            this.mostPlayedChampion = mostPlayedChampion;
            this.totalStats = totalStats;
            this.matchStatsLsist = matchStatsLsist;
        }
    }

    public class SummonerMatchStats
    {
        public int matchId { get; set; }
        public string matchDate { get; set; }
        public string matchTime { get; set; }
        public bool matchResult { get; set; }
        public List<SummonerInfo> summonerList { get; set; }
        public int kills { get; set; }
        public int assists { get; set; }
        public int deaths { get; set; }
        public int creepsKilled { get; set; }
        public double goldEarned { get; set; }
        public string role { get; set; }
        public string lane { get; set; }

        public SummonerMatchStats(MatchDetailsModel matchDetails, int mainSummonerId, string role, string lane)
        {
            matchId = matchDetails.matchId;
            matchDate = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(Convert.ToDouble(matchDetails.matchCreation)).ToLocalTime().ToShortTimeString();
            matchTime = TimeSpan.FromSeconds(matchDetails.matchDuration).ToString(@"hh\:mm\:ss");
            var participantId = matchDetails.participantIdentities.FirstOrDefault(x => x.player.summonerId == mainSummonerId).participantId;
            var teamId = matchDetails.participants.FirstOrDefault(x => x.participantId == participantId).teamId;
            matchResult = matchDetails.teams.FirstOrDefault(x => x.teamId == teamId).winner;
            lolwayEntities db = new lolwayEntities();
            summonerList = new List<SummonerInfo>();
            foreach (var summoner in matchDetails.participants)
            {
                var summonerName = matchDetails.participantIdentities.FirstOrDefault(x => x.participantId == summoner.participantId).player.summonerName;
                var summonerId = matchDetails.participantIdentities.FirstOrDefault(x => x.participantId == summoner.participantId).player.summonerId;
                var championImage = RiotImageHelper.GetImageUrl("champion", db.champion.FirstOrDefault(x => x.id == summoner.championId).image);
                summonerList.Add(new SummonerInfo(summonerId, summonerName, championImage, summoner.teamId.ToString()));
            }
            var summonerStats = matchDetails.participants.FirstOrDefault(x => x.participantId == participantId).stats;
            kills = summonerStats.kills;
            assists = summonerStats.assists;
            deaths = summonerStats.deaths;
            goldEarned = summonerStats.goldEarned;
            this.role = role;
            this.lane = lane;
        }
    }

    public class SummonerInfo
    {
        public int summonerId { get; set; }
        public string summonerName { get; set; }
        public string spell1 { get; set; }
        public string spell2 { get; set; }
        public string champion { get; set; }
        public List<string> runes { get; set; }
        public string mastery { get; set; }
        public double averageKills { get; set; }
        public double averageAssists { get; set; }
        public double averageDeaths { get; set; }
        public string division { get; set; }
        public string team { get; set; }

        // live game
        public SummonerInfo(int summonerId, string summonerName, string spell1, string spell2, string champion, string division, string mastery)
        {
            this.summonerId = summonerId;
            this.summonerName = summonerName;
            this.spell1 = spell1;
            this.spell2 = spell2;
            this.champion = champion;
            this.division = division;
            this.mastery = mastery;
        }

        // summoner stats
        public SummonerInfo(int summonerId, string summonerName, string champion, string team)
        {
            this.summonerId = summonerId;
            this.summonerName = summonerName;
            this.champion = champion;
            this.team = team;
        }
    }

    public class CurrentGameBan
    {
        public int team { get; set; }
        public string banName { get; set; }

        public CurrentGameBan(int team, string banName)
        {
            this.team = team;
            this.banName = banName;
        }
    }
}