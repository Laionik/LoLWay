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
            var builds = db.build.Where(x => x.AspNetUsers.Id == userId).Include(b => b.AspNetUsers).Include(b => b.champion).Include(b => b.mastery).ToList();
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
            var buildDetails = new BuildDetails(builds);
            if (buildDetails == null)
            {
                return HttpNotFound();
            }
            return View(buildDetails);
        }

        // GET: builds/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.championId = new SelectList(db.champion, "id", "name");
            ViewBag.masteryId = new SelectList(db.mastery, "id", "name");

            var items = RiotImageHelper.GetItemsImages(db.item.ToList());
            var runes = RiotImageHelper.GetRunesImages(db.rune.ToList());

            ViewBag.runes = runes;
            ViewBag.itemsDescription = items;

            ViewBag.items = new BuildModels(new SelectList(items, "id", "image"));
            ViewBag.marks = new BuildModels(new SelectList(runes.Where(x => x.type == "red"), "id", "image"));
            ViewBag.seals = new BuildModels(new SelectList(runes.Where(x => x.type == "yellow"), "id", "image"));
            ViewBag.quins = new BuildModels(new SelectList(runes.Where(x => x.type == "black"), "id", "image"));
            ViewBag.glyphs = new BuildModels(new SelectList(runes.Where(x => x.type == "blue"), "id", "image"));
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
                if (items != null) BuildHelper.itemsAdd(ref newBuild, items.Take(6));
                if (marks != null) BuildHelper.runesAdd(ref newBuild, marks.Take(6));
                if (glyphs != null) BuildHelper.runesAdd(ref newBuild, glyphs.Take(6));
                if (seals != null) BuildHelper.runesAdd(ref newBuild, seals.Take(6));
                if (quins != null) BuildHelper.runesAdd(ref newBuild, quins.Take(6));

                newBuild.userId = User.Identity.GetUserId();
                newBuild.modificationDate = DateTime.Now;
                db.build.Add(newBuild);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.AspNetUsers, "Id", "Email", newBuild.userId);
            ViewBag.championId = new SelectList(db.champion, "id", "name", newBuild.championId);
            ViewBag.masteryId = new SelectList(db.mastery, "id", "name", newBuild.masteryId);

            var itemsList = RiotImageHelper.GetItemsImages(db.item.ToList());
            var runes = RiotImageHelper.GetRunesImages(db.rune.ToList());

            ViewBag.runes = runes;
            ViewBag.itemsDescription = itemsList;

            ViewBag.items = new BuildModels(new SelectList(itemsList, "id", "image"));
            ViewBag.marks = new BuildModels(new SelectList(runes.Where(x => x.type == "red"), "id", "image"));
            ViewBag.seals = new BuildModels(new SelectList(runes.Where(x => x.type == "yellow"), "id", "image"));
            ViewBag.quins = new BuildModels(new SelectList(runes.Where(x => x.type == "black"), "id", "image"));
            ViewBag.glyphs = new BuildModels(new SelectList(runes.Where(x => x.type == "blue"), "id", "image"));

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
            ViewBag.userId = new SelectList(db.AspNetUsers, "Id", "Email", builds.userId);
            ViewBag.championId = new SelectList(db.champion, "id", "name", builds.championId);
            ViewBag.masteryId = new SelectList(db.mastery, "id", "name", builds.masteryId);


            var items = RiotImageHelper.GetItemsImages(db.item.ToList());
            var runes = RiotImageHelper.GetRunesImages(db.rune.ToList());

            ViewBag.runes = runes;
            ViewBag.itemsDescription = items;
            ViewBag.items = new BuildModels(new SelectList(items, "id", "image"), builds.items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList());
            ViewBag.marks = new BuildModels(new SelectList(runes.Where(x => x.type == "red"), "id", "image"), builds.runes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList());
            ViewBag.seals = new BuildModels(new SelectList(runes.Where(x => x.type == "yellow"), "id", "image"), builds.runes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList());
            ViewBag.quins = new BuildModels(new SelectList(runes.Where(x => x.type == "black"), "id", "image"), builds.runes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList());
            ViewBag.glyphs = new BuildModels(new SelectList(runes.Where(x => x.type == "blue"), "id", "image"), builds.runes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList());
            return View(builds);
        }

        // POST: builds/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "id,userId,championId,masteryId,gameVersion,modificationDate,notes")] build newBuild, IEnumerable<int> items, IEnumerable<int> quins, IEnumerable<int> marks, IEnumerable<int> seals, IEnumerable<int> glyphs)
        {
            if (ModelState.IsValid)
            {
                if (items != null) BuildHelper.itemsAdd(ref newBuild, items.Take(6));
                if (marks != null) BuildHelper.runesAdd(ref newBuild, marks.Take(6));
                if (glyphs != null) BuildHelper.runesAdd(ref newBuild, glyphs.Take(6));
                if (seals != null) BuildHelper.runesAdd(ref newBuild, seals.Take(6));
                if (quins != null) BuildHelper.runesAdd(ref newBuild, quins.Take(6));
                newBuild.userId = User.Identity.GetUserId();
                newBuild.modificationDate = DateTime.Now;

                db.Entry(newBuild).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.AspNetUsers, "Id", "Email", newBuild.userId);
            ViewBag.championId = new SelectList(db.champion, "id", "name", newBuild.championId);
            ViewBag.masteryId = new SelectList(db.mastery, "id", "name", newBuild.masteryId);

            var itemsList = RiotImageHelper.GetItemsImages(db.item.ToList());
            var runes = RiotImageHelper.GetRunesImages(db.rune.ToList());

            ViewBag.runes = runes;
            ViewBag.itemsDescription = itemsList;
            ViewBag.items = new BuildModels(new SelectList(itemsList, "id", "image"), newBuild.items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList());
            ViewBag.marks = new BuildModels(new SelectList(runes.Where(x => x.type == "red"), "id", "image"), newBuild.runes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList());
            ViewBag.seals = new BuildModels(new SelectList(runes.Where(x => x.type == "yellow"), "id", "image"), newBuild.runes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList());
            ViewBag.quins = new BuildModels(new SelectList(runes.Where(x => x.type == "black"), "id", "image"), newBuild.runes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList());
            ViewBag.glyphs = new BuildModels(new SelectList(runes.Where(x => x.type == "blue"), "id", "image"), newBuild.runes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList());
            return View(newBuild);
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
            var buildDetails = new BuildDetails(builds);
            if (buildDetails == null)
            {
                return HttpNotFound();
            }
            return View(buildDetails);
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
