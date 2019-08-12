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
    public class FederationSeasonsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: FederationSeasons
        public ActionResult Index()
        {
            try
            {
                var federationSeasons = db.FederationSeasons.Include(f => f.SportFederation);
                return View(federationSeasons.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: FederationSeasons/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                FederationSeason federationSeason = db.FederationSeasons.Find(id);
                if (federationSeason == null)
                {
                    return HttpNotFound();
                }
                return View(federationSeason);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: FederationSeasons/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.SportFederationId = new SelectList(db.SportFederations, "SportFederationId", "NameSportString");
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: FederationSeasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FederationSeasonId,TournamentString,NameTournament,SeasonString,SportFederationId")] FederationSeason federationSeason)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.FederationSeasons.Add(federationSeason);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.SportFederationId = new SelectList(db.SportFederations, "SportFederationId", "NameSportString", federationSeason.SportFederationId);
                return View(federationSeason);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: FederationSeasons/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                FederationSeason federationSeason = db.FederationSeasons.Find(id);
                if (federationSeason == null)
                {
                    return HttpNotFound();
                }
                ViewBag.SportFederationId = new SelectList(db.SportFederations, "SportFederationId", "NameSportString", federationSeason.SportFederationId);
                return View(federationSeason);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: FederationSeasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FederationSeasonId,TournamentString,NameTournament,SeasonString,SportFederationId")] FederationSeason federationSeason)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(federationSeason).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.SportFederationId = new SelectList(db.SportFederations, "SportFederationId", "NameSportString", federationSeason.SportFederationId);
                return View(federationSeason);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: FederationSeasons/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                FederationSeason federationSeason = db.FederationSeasons.Find(id);
                if (federationSeason == null)
                {
                    return HttpNotFound();
                }
                return View(federationSeason);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: FederationSeasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                FederationSeason federationSeason = db.FederationSeasons.Find(id);
                db.FederationSeasons.Remove(federationSeason);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
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
