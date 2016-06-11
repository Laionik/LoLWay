using System.Web.Mvc;
using RiotAPI;
using System.Collections.Generic;
using System.Linq;
using System;
using LoLWay.Models;
using System.Threading;

namespace LoLWay.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public List<CurrentGameBan> GetBanList(List<RiotAPI.Models.CurrentGame.BannedChampionModel> bans, lolwayEntities db)
        {
            List<CurrentGameBan> gameBanList = new List<CurrentGameBan>();
            foreach (var championBan in bans)
            {
                gameBanList.Add(new CurrentGameBan(championBan.teamId, db.champion.FirstOrDefault(x => x.id == championBan.championId).image));
            }
            return gameBanList;
        }

        [HttpPost]
        public ActionResult Index(string nickname, string region)
        {
            try
            {
                lolwayEntities db = new lolwayEntities();
                var currentGame = CurrentGame.GetCurrentGame(Summoner.GetSummonerByName(nickname, region).id, region);
                var summonerIds = currentGame.participants.Select(x => x.summonerId.ToString()).ToList();
                List<CurrentGameBan> gameBanList = new List<CurrentGameBan>();
                var masteriesList = db.mastery.Select(x => x.id).ToList();
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    gameBanList = GetBanList(currentGame.bannedChampions, db);
                }).Start();

                List<SummonerStats> summonerList = new List<SummonerStats>();
                foreach (var summonerId in summonerIds)
                {
                    var summonerStats = Summoner.GetSummonerStats(summonerId, region);
                    var summoner = currentGame.participants.FirstOrDefault(p => p.summonerId == summonerStats.summonerId);

                    string spell1Img = db.spell.FirstOrDefault(x => x.id == summoner.spell1Id).image;
                    string spell2Img = db.spell.FirstOrDefault(x => x.id == summoner.spell2Id).image;
                    string championImg = db.champion.FirstOrDefault(x => x.id == summoner.championId).image;
                    string division = "BRAK";
                    var summonerMasteries = summoner.masteries.Select(x => x.masteryId).ToList();
                    int? mastery = masteriesList.FirstOrDefault(x => summonerMasteries.Contains(x));
                    string masteryImg = mastery != null ? mastery.ToString() + ".png" : "0.png";

                    SummonerStats cls = new SummonerStats(summonerStats.summonerId, summoner.summonerName, spell1Img, spell2Img, championImg, division, masteryImg);
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
                ViewBag.BanList = gameBanList;
                ViewBag.Participants = summonerList;
                // wyciągnij informacje o graczach
                return View(currentGame);
            }
            catch (Exception ex)
            {
                var title = "Nieoczekiwany błąd :(";
                var message = "Skontaktuj się z administracją! Proszę podaj sytuację kiedy wystąpił błąd i załącz poniższą treść";
                var errorMessage = ex.Message;
                return RedirectToAction("Error", new { messageTitle = title, messageMain = message, messageError = errorMessage });
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error(string messageTitle, string messageMain, string messageError)
        {

            ViewBag.Title = messageTitle;
            ViewBag.Message = messageMain;
            ViewBag.errorMessage = messageError;
            return View();
        }
    }
}