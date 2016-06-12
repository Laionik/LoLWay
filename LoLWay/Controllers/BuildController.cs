using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LoLWay.Models;
using MoreLinq;
using Microsoft.AspNet.Identity;
using LoLWay.Helpers;
using System;
using System.Collections.Generic;

namespace LoLWay.Controllers
{
    public class BuildController : Controller
    {
        private lolwayEntities db = new lolwayEntities();

        // GET: builds
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var builds = db.build.Where(x => x.aspnetusers.Id == userId).Include(b => b.aspnetusers).Include(b => b.champion).Include(b => b.mastery).ToList();
            ViewBag.championFilter = builds.DistinctBy(x => x.championId).ToList();

            builds = RiotImageHelper.GetChampionImages(builds);
            return View(builds);
        }

        // GET: builds/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            build builds = db.build.Find(id);
            if (builds == null)
            {
                return HttpNotFound();
            }
            return View(builds);
        }

        // GET: builds/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.aspnetusers, "Id", "Email");
            ViewBag.championId = new SelectList(db.champion, "id", "name");
            ViewBag.masteryId = new SelectList(db.mastery, "id", "name");

            var items = RiotImageHelper.GetItemsImages(db.item.ToList());
            var runes = RiotImageHelper.GetRunesImages(db.rune.ToList());

            ViewBag.runes = runes;
            ViewBag.itemsDescription = items;

            ViewBag.items = new BuildModels(new SelectList(items, "id", "image", "name", new object()));
            ViewBag.marks = new BuildModels(new SelectList(runes.Where(x => x.type == "red"), "id", "image", "name"));
            ViewBag.seals = new BuildModels(new SelectList(runes.Where(x => x.type == "yellow"), "id", "image", "name"));
            ViewBag.quins = new BuildModels(new SelectList(runes.Where(x => x.type == "black"), "id", "image", "name"));
            ViewBag.glyphs = new BuildModels(new SelectList(runes.Where(x => x.type == "blue"), "id", "image", "name"));
            return View();
        }

        // POST: builds/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "id,userId,championId,masteryId,gameVersion,modificationDate,notes")] build newBuild, IEnumerable<int> items, IEnumerable<int> quins, IEnumerable<int> marks, IEnumerable<int> seals, IEnumerable<int> glyphs)
        {
            if (ModelState.IsValid)
            {
                if (items != null) BuildHelper.itemsAdd(ref newBuild, items.Take(6), db);
                if (marks != null) BuildHelper.runesAdd(ref newBuild, marks.Take(6), db);
                if (glyphs != null) BuildHelper.runesAdd(ref newBuild, glyphs.Take(6), db);
                if (seals != null) BuildHelper.runesAdd(ref newBuild, seals.Take(6), db);
                if (quins != null) BuildHelper.runesAdd(ref newBuild, quins.Take(6), db);
                var userId = User.Identity.GetUserId();
                newBuild.userId = userId;
                newBuild.aspnetusers = db.aspnetusers.FirstOrDefault(x => x.Id == userId);
                newBuild.champion = db.champion.FirstOrDefault(x => x.id == newBuild.championId);
                newBuild.mastery = db.mastery.FirstOrDefault(x => x.id == newBuild.masteryId);

                newBuild.modificationDate = DateTime.Now;
                db.build.Add(newBuild);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.aspnetusers, "Id", "Email", newBuild.userId);
            ViewBag.championId = new SelectList(db.champion, "id", "name", newBuild.championId);
            ViewBag.masteryId = new SelectList(db.mastery, "id", "name", newBuild.masteryId);
            return View(newBuild);
        }

        // GET: builds/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            build builds = db.build.Find(id);
            if (builds == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.aspnetusers, "Id", "Email", builds.userId);
            ViewBag.championId = new SelectList(db.champion, "id", "name", builds.championId);
            ViewBag.masteryId = new SelectList(db.mastery, "id", "name", builds.masteryId);
            return View(builds);
        }

        // POST: builds/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "id,userId,championId,masteryId,gameVersion,modificationDate,notes")] build builds)
        {
            if (ModelState.IsValid)
            {
                db.Entry(builds).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.aspnetusers, "Id", "Email", builds.userId);
            ViewBag.championId = new SelectList(db.champion, "id", "name", builds.championId);
            ViewBag.masteryId = new SelectList(db.mastery, "id", "name", builds.masteryId);
            return View(builds);
        }

        // GET: builds/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            build builds = db.build.Find(id);
            if (builds == null)
            {
                return HttpNotFound();
            }
            return View(builds);
        }

        // POST: builds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            build builds = db.build.Find(id);
            db.build.Remove(builds);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
