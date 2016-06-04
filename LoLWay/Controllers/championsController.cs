using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LoLWay.Models;

namespace LoLWay.Controllers
{
    public class championsController : Controller
    {
        private lolwayEntities db = new lolwayEntities();

        // GET: champions
        public ActionResult Index()
        {
            return View(db.champion.ToList());
        }

        // GET: champions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            champion champion = db.champion.Find(id);
            if (champion == null)
            {
                return HttpNotFound();
            }
            return View(champion);
        }

        // GET: champions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: champions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,title,image")] champion champion)
        {
            if (ModelState.IsValid)
            {
                db.champion.Add(champion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(champion);
        }

        // GET: champions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            champion champion = db.champion.Find(id);
            if (champion == null)
            {
                return HttpNotFound();
            }
            return View(champion);
        }

        // POST: champions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,title,image")] champion champion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(champion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(champion);
        }

        // GET: champions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            champion champion = db.champion.Find(id);
            if (champion == null)
            {
                return HttpNotFound();
            }
            return View(champion);
        }

        // POST: champions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            champion champion = db.champion.Find(id);
            db.champion.Remove(champion);
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
