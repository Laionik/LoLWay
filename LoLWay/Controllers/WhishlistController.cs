using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LoLWay.Models;
using Microsoft.AspNet.Identity;
using System.Net;

namespace LoLWay.Controllers
{
    public class WhishlistController : Controller
    {
        private lolwayEntities db = new lolwayEntities();

        // GET: whishlists
        [Authorize]
        public ActionResult Whishlist()
        {
            var userId = User.Identity.GetUserId();
            var whishList = db.whishlist.Include(w => w.champion).Include(w => w.aspnetusers).ToList();
            if (whishList.FirstOrDefault(x => x.userId == userId) == null)
            {
                WhisListInitialize(userId);
            }
            return View(whishList);
        }

        private void WhisListInitialize(string userId)
        {
            try
            {
                foreach (var champion in db.champion)
                {
                    whishlist temp = new whishlist();
                    temp.championId = champion.id;
                    temp.userId = userId;
                    temp.owned = 0;
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
        public ActionResult WhishlistTable(int? owned)
        {
            List<whishlist> whishList = new List<whishlist>();
            if (owned.HasValue)
            {
                whishList = db.whishlist.Where(x => x.owned == owned).Include(w => w.champion).Include(w => w.aspnetusers).ToList();
            }
            else
            {
                whishList = db.whishlist.Include(w => w.champion).Include(w => w.aspnetusers).ToList();
            }
            return PartialView("~/Views/Whishlist/_Whishlist.cshtml", whishList);
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
