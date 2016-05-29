using System.Web.Mvc;
using RiotAPI;
using System.Collections.Generic;
using System.Linq;

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
            var summonersList = Summoner.GetSummonerByName(apiKey, nickname, region);
            var currentGame = CurrentGame.GetCurrentGame(apiKey, summonersList.id, region);
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