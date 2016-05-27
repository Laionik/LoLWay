using System.Web.Mvc;
using RiotAPI;
using System.Collections.Generic;

namespace LoLWay.Controllers
{
    public class HomeController : Controller
    {
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
            string apiKey = "09e2ba8a-34b8-44d4-9445-262ed673da5d";
            var summonersList = Summoner.GetSummonersByName(apiKey, new List<string>() { nickname }, region);
            var currentGame = CurrentGame.GetCurrentGame(apiKey, summonersList[0].id, region);
            // wyciągnij informacje o graczach
            return View(currentGame);
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