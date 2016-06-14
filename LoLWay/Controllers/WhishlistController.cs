using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LoLWay.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Collections.Generic;
using LoLWay.Helpers;

namespace LoLWay.Controllers
{
    public class WhishlistController : Controller
    {
        private lolwayEntities db = new lolwayEntities();

        // GET: whishlists
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var whishList = db.whishlist.Include(w => w.champion).Include(w => w.aspnetusers).ToList();
            if (whishList.FirstOrDefault(x => x.userId == userId) == null)
            {
                WhisListInitialize(userId);
            }
            whishList = RiotImageHelper.GetChampionImages(whishList);
            return View(whishList);
        }

        /// <summary>
        /// Initialize a whish list for a new user
        /// </summary>
        /// <param name="userId">User Id</param>
        private void WhisListInitialize(string userId)
        {
            try
            {
                foreach (var champion in db.champion)
                {
                    whishlist temp = new whishlist();
                    temp.championId = champion.id;
                    temp.userId = userId;
                    temp.owned = false;
                    temp.rank = 0;
                    db.whishlist.Add(temp);
                }
                db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var title = "Nieoczekiwany błąd podczas inicjalizacji whishlisty :(";
                var message = "Skontaktuj się z administracją! Proszę podaj sytuację kiedy wystąpił błąd i załącz poniższą treść";
                var errorMessage = ex.Message;
                RedirectToAction("Error", "Home", new { messageTitle = title, messageMain = message, messageError = errorMessage });
            }
        }

        [Authorize]
        public ActionResult WhishlistTable(bool? owned, bool? championSort, bool? statusSort, bool? rankSort)
        {
            var query = db.whishlist.Include(w => w.champion).Include(w => w.aspnetusers) as IQueryable<whishlist>;

            // building a query
            if (owned.HasValue)
            {
                query = query.Where(x => x.owned == owned);
                ViewBag.owned = owned;
            }

            if(championSort.HasValue)
            {
                ViewBag.championSort = championSort;
                if ((bool) championSort)
                {
                    query = query.OrderBy(x => x.champion.name);
                }
                else
                {
                    query = query.OrderByDescending(x => x.champion.name);
                }
            }

            if (statusSort.HasValue)
            {
                ViewBag.statusSort = statusSort;
                if ((bool)statusSort)
                {
                    query = query.OrderBy(x => x.owned);
                }
                else
                {
                    query = query.OrderByDescending(x => x.owned);
                }
            }

            if (rankSort.HasValue)
            {
                ViewBag.rankSort = rankSort;
                if ((bool)rankSort)
                {
                    query = query.OrderBy(x => x.rank);
                }
                else
                {
                    query = query.OrderByDescending(x => x.rank);
                }
            }
            return PartialView("~/Views/Whishlist/_Whishlist.cshtml", RiotImageHelper.GetChampionImages(query.ToList()));
        }

        // GET: whishlists/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            whishlist whishlist = db.whishlist.Where(x => x.userId == userId).FirstOrDefault(x => x.championId == id);
            if (whishlist == null)
            {
                return HttpNotFound();
            }
            ViewBag.championId = new SelectList(db.champion, "id", "name", whishlist.championId);
            ViewBag.userId = new SelectList(db.aspnetusers, "Id", "Email", whishlist.userId);
            return View(whishlist);
        }

        // POST: whishlists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "id,userId,championId,owned,rank")] whishlist whishlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(whishlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Whishlist");
            }
            ViewBag.championId = new SelectList(db.champion, "id", "name", whishlist.championId);
            ViewBag.userId = new SelectList(db.aspnetusers, "Id", "Email", whishlist.userId);
            return View(whishlist);
        }

    }
}
