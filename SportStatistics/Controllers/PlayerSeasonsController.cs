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
    public class PlayerSeasonsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: PlayerSeasons
        public ActionResult Index()
        {
            var playerSeasons = db.PlayerSeasons.Include(p => p.Player).Include(p => p.TeamSeason);
            return View(playerSeasons.ToList());
        }

        // GET: PlayerSeasons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerSeason playerSeason = db.PlayerSeasons.Find(id);
            if (playerSeason == null)
            {
                return HttpNotFound();
            }
            return View(playerSeason);
        }

        // GET: PlayerSeasons/Create
        public ActionResult Create()
        {
            ViewBag.PlayerId = new SelectList(db.Players, "PlayerId", "NameSportString");
            ViewBag.TeamSeasonId = new SelectList(db.TeamSeasons, "TeamSeasonId", "NameSportString");
            return View();
        }

        // POST: PlayerSeasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerSeasonId,TournamentString,Tournament,Season,GamedMatches,Goals,Assists,PlayerId,TeamSeasonId")] PlayerSeason playerSeason)
        {
            if (ModelState.IsValid)
            {
                db.PlayerSeasons.Add(playerSeason);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlayerId = new SelectList(db.Players, "PlayerId", "NameSportString", playerSeason.PlayerId);
            ViewBag.TeamSeasonId = new SelectList(db.TeamSeasons, "TeamSeasonId", "NameSportString", playerSeason.TeamSeasonId);
            return View(playerSeason);
        }

        // GET: PlayerSeasons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerSeason playerSeason = db.PlayerSeasons.Find(id);
            if (playerSeason == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlayerId = new SelectList(db.Players, "PlayerId", "NameSportString", playerSeason.PlayerId);
            ViewBag.TeamSeasonId = new SelectList(db.TeamSeasons, "TeamSeasonId", "NameSportString", playerSeason.TeamSeasonId);
            return View(playerSeason);
        }

        // POST: PlayerSeasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerSeasonId,TournamentString,Tournament,Season,GamedMatches,Goals,Assists,PlayerId,TeamSeasonId")] PlayerSeason playerSeason)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playerSeason).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayerId = new SelectList(db.Players, "PlayerId", "NameSportString", playerSeason.PlayerId);
            ViewBag.TeamSeasonId = new SelectList(db.TeamSeasons, "TeamSeasonId", "NameSportString", playerSeason.TeamSeasonId);
            return View(playerSeason);
        }

        // GET: PlayerSeasons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerSeason playerSeason = db.PlayerSeasons.Find(id);
            if (playerSeason == null)
            {
                return HttpNotFound();
            }
            return View(playerSeason);
        }

        // POST: PlayerSeasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlayerSeason playerSeason = db.PlayerSeasons.Find(id);
            db.PlayerSeasons.Remove(playerSeason);
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
