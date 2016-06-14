using LoLWay.Models;
using RiotAPI;
using RiotAPI.Models.CurrentGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LoLWay.Helpers
{
    public static class StatsHelper
    {
        private static List<CurrentGameBan> GetBanList(List<RiotAPI.Models.CurrentGame.BannedChampionModel> bans, lolwayEntities db)
        {
            List<CurrentGameBan> gameBanList = new List<CurrentGameBan>();
            foreach (var championBan in bans)
            {
                gameBanList.Add(new CurrentGameBan(championBan.teamId, RiotImageHelper.GetImageUrl("champion", db.champion.FirstOrDefault(x => x.id == championBan.championId).image)));
            }
            return gameBanList;
        }

        public static CurrentMatchModel GetLiveGameStats(ref List<CurrentGameBan> gameBanList, ref List<SummonerInfo> summonerList, string nickname, string region)
        {
            lolwayEntities db = new lolwayEntities();
            var currentGame = CurrentGame.GetCurrentGame(Summoner.GetSummonerByName(nickname, region).id, region);
            var summonerIds = currentGame.participants.Select(x => x.summonerId.ToString()).ToList();
            var masteriesList = db.mastery.Select(x => x.id).ToList();
            List<CurrentGameBan> gameBanListToCreate = new List<CurrentGameBan>();
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                gameBanListToCreate = GetBanList(currentGame.bannedChampions, db);
            }).Start();

            foreach (var summonerId in summonerIds)
            {
                var summonerStats = Summoner.GetSummonerStats(summonerId, region);
                var summoner = currentGame.participants.FirstOrDefault(p => p.summonerId == summonerStats.summonerId);

                string spell1Img = RiotImageHelper.GetImageUrl("spell", db.spell.FirstOrDefault(x => x.id == summoner.spell1Id).image);
                string spell2Img = RiotImageHelper.GetImageUrl("spell", db.spell.FirstOrDefault(x => x.id == summoner.spell2Id).image);
                string championImg = RiotImageHelper.GetImageUrl("champion", db.champion.FirstOrDefault(x => x.id == summoner.championId).image);
                string division = "BRAK";
                var summonerMasteries = summoner.masteries.Select(x => x.masteryId).ToList();
                int masteryId = masteriesList.FirstOrDefault(x => summonerMasteries.Contains(x));
                string mastery = RiotImageHelper.GetImageUrl("mastery", db.mastery.FirstOrDefault(x => x.id == masteryId).image);

                SummonerInfo cls = new SummonerInfo(summonerStats.summonerId, summoner.summonerName, spell1Img, spell2Img, championImg, division, mastery);
                if (summonerStats.champions == null || summonerStats.champions.FirstOrDefault(x => x.id == summoner.championId) == null)
                {
                    cls.averageKills = 0;
                    cls.averageAssists = 0;
                    cls.averageDeaths = 0;
                }
                else
                {
                    var champion = summonerStats.champions.FirstOrDefault(x => x.id == summoner.championId);
                    var matches = champion.stats.totalSessionsPlayed;
                    cls.averageKills = Math.Round((double)champion.stats.totalChampionKills / matches, 1);
                    cls.averageAssists = Math.Round((double)champion.stats.totalAssists / matches, 1);
                    cls.averageDeaths = Math.Round((double)champion.stats.totalDeathsPerSession / matches, 1);
                }

                summonerList.Add(cls);
            }
            gameBanList = gameBanListToCreate;
            return currentGame;
        }


        public static SummonerModel GetSummonerStats(string nickname, string region)
        {
            region = region.ToLower();
            var summonerData = Summoner.GetSummonerByName(nickname, region);
            //matches
            var matchList = Match.GetMatchListBySummonerName(nickname, region).matches.Take(2);
            List<SummonerMatchStats> matchDetailsList = new List<SummonerMatchStats>();
            foreach (var match in matchList)
            {
                matchDetailsList.Add(new SummonerMatchStats(Match.GetMatchById(match.matchId.ToString(), region), summonerData.id, match.role, match.lane));
            }

            //champions
            var championStats = Summoner.GetSummonerStats(summonerData.id.ToString(), region).champions;
            var championStatsModel = championStats.Where(x => x.id != 0).OrderByDescending(x => x.stats.totalSessionsPlayed).FirstOrDefault();
            var totalStats = championStats.FirstOrDefault(x => x.id == 0);
            return new SummonerModel(summonerData.id, summonerData.name, summonerData.summonerLevel, "DREWNO 5", championStatsModel, totalStats, matchDetailsList);
        }
    }
}