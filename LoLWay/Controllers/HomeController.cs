using System.Web.Mvc;
using RiotAPI;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LoLWay.Controllers
{
    public class HomeController : Controller
    {
        private string apiKey = "09e2ba8a-34b8-44d4-9445-262ed673da5d";
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                IndexLogged();
            }

            return View();
        }

        public ActionResult IndexLogged()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string nickname, string region)
        {
            try
            {
                var currentGame = CurrentGame.GetCurrentGame(Summoner.GetSummonerByName(nickname, region).id, region);
                var summonerId = currentGame.participants.Select(x => x.summonerId.ToString()).ToList();
                List<RiotAPI.Models.Summoner.SummonerStatsModel> summonerList = new List<RiotAPI.Models.Summoner.SummonerStatsModel>();
                foreach (var summoner in summonerId)
                {
                    summonerList.Add(Summoner.GetSummonerStats(summoner, region));
                }

                ViewBag.Participants = summonerList;
                // wyciągnij informacje o graczach
                return View(currentGame);
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Nieoczekiwany błąd :(";
                ViewBag.Message = "Skontaktuj się z administracją! Proszę podaj sytuację kiedy wystąpił błąd i załącz poniższą treść";
                ViewBag.errorMessage = ex.Message;
                return View("Error");
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
    }
}