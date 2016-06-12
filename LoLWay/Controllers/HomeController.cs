using System.Web.Mvc;
using RiotAPI;
using System.Collections.Generic;
using System.Linq;
using System;
using LoLWay.Models;
using System.Threading;
using LoLWay.Helpers;

namespace LoLWay.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string nickname, string region)
        {
            try
            {
                List<CurrentGameBan> gameBanList = new List<CurrentGameBan>();
                List<SummonerStats> summonerList = new List<SummonerStats>();

                var currentGame = LiveStatsHelper.GetLiveGameStats(ref gameBanList, ref summonerList, nickname, region);

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