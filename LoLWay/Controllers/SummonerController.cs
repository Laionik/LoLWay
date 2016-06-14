using LoLWay.Helpers;
using LoLWay.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LoLWay.Controllers
{
    public class SummonerController : Controller
    {
        private lolwayEntities db = new lolwayEntities();
        // GET: Summoner
        public ActionResult Index()
        {
            return View();
        }

        // partial view - summoner stats
        public ActionResult SummonerLoad(string nickname, string region)
        {
            try
            {
                var summonerStats = StatsHelper.GetSummonerStats(nickname, region.ToLower());
                ViewBag.champion = db.champion.FirstOrDefault(x => x.id == summonerStats.mostPlayedChampion.id).name;
                ViewBag.summonerId = summonerStats.id;
                return PartialView("~/Views/Summoner/_Summoner.cshtml", summonerStats);
            }
            catch (Exception ex)
            {
                ViewBag.summonerStatus = true;
                ViewBag.errorMessage = ex;
                return PartialView("~/Views/Summoner/_Summoner.cshtml", null);
            }
        }
    }
}