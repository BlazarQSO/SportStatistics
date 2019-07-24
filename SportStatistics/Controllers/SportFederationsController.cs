using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportStatistics.Models;

namespace SportStatistics.Controllers
{
    public class SportFederationsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SportFederations
        public ActionResult Index()
        {
            var sportFederation = db.SportFederation.Include(s => s.Sport);
            return View(sportFederation.ToList());
        }

        // GET: SportFederations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SportFederation sportFederation = db.SportFederation.Find(id);
            if (sportFederation == null)
            {
                return HttpNotFound();
            }
            return View(sportFederation);
        }

        // GET: SportFederations/Create
        public ActionResult Create()
        {
            ViewBag.SportId = new SelectList(db.Sports, "SportId", "NameSportString");
            return View();
        }

        // POST: SportFederations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SportFederationId,Country,FoundationDate,NamePresident,SportId")] SportFederation sportFederation)
        {
            if (ModelState.IsValid)
            {
                db.SportFederation.Add(sportFederation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SportId = new SelectList(db.Sports, "SportId", "NameSportString", sportFederation.SportId);
            return View(sportFederation);
        }

        // GET: SportFederations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SportFederation sportFederation = db.SportFederation.Find(id);
            if (sportFederation == null)
            {
                return HttpNotFound();
            }
            ViewBag.SportId = new SelectList(db.Sports, "SportId", "NameSportString", sportFederation.SportId);
            return View(sportFederation);
        }

        // POST: SportFederations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SportFederationId,Country,FoundationDate,NamePresident,SportId")] SportFederation sportFederation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sportFederation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SportId = new SelectList(db.Sports, "SportId", "NameSportString", sportFederation.SportId);
            return View(sportFederation);
        }

        // GET: SportFederations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SportFederation sportFederation = db.SportFederation.Find(id);
            if (sportFederation == null)
            {
                return HttpNotFound();
            }
            return View(sportFederation);
        }

        // POST: SportFederations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SportFederation sportFederation = db.SportFederation.Find(id);
            db.SportFederation.Remove(sportFederation);
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
