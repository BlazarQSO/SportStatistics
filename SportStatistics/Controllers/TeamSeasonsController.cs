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
    public class TeamSeasonsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: TeamSeasons
        public ActionResult Index()
        {
            var teamSeasons = db.TeamSeasons.Include(t => t.FederationSeason).Include(t => t.Team);
            return View(teamSeasons.ToList());
        }

        // GET: TeamSeasons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamSeason teamSeason = db.TeamSeasons.Find(id);
            if (teamSeason == null)
            {
                return HttpNotFound();
            }
            return View(teamSeason);
        }

        // GET: TeamSeasons/Create
        public ActionResult Create()
        {
            ViewBag.FederationSeasonId = new SelectList(db.FederationSeasons, "FederationSeasonId", "NameSportString");
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "NameSportString");
            return View();
        }

        // POST: TeamSeasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamSeasonId,NameSportString,TournamentString,Tournament,Season,Point,HomePoint,Win,HomeWin,Draw,HomeDraw,Lose,HomeLose,Goals,HomeGoals,GoalAgainst,HomeGoalAgainst,TeamId,FederationSeasonId")] TeamSeason teamSeason)
        {
            if (ModelState.IsValid)
            {
                db.TeamSeasons.Add(teamSeason);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FederationSeasonId = new SelectList(db.FederationSeasons, "FederationSeasonId", "NameSportString", teamSeason.FederationSeasonId);
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "NameSportString", teamSeason.TeamId);
            return View(teamSeason);
        }

        // GET: TeamSeasons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamSeason teamSeason = db.TeamSeasons.Find(id);
            if (teamSeason == null)
            {
                return HttpNotFound();
            }
            ViewBag.FederationSeasonId = new SelectList(db.FederationSeasons, "FederationSeasonId", "NameSportString", teamSeason.FederationSeasonId);
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "NameSportString", teamSeason.TeamId);
            return View(teamSeason);
        }

        // POST: TeamSeasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamSeasonId,NameSportString,TournamentString,Tournament,Season,Point,HomePoint,Win,HomeWin,Draw,HomeDraw,Lose,HomeLose,Goals,HomeGoals,GoalAgainst,HomeGoalAgainst,TeamId,FederationSeasonId")] TeamSeason teamSeason)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamSeason).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FederationSeasonId = new SelectList(db.FederationSeasons, "FederationSeasonId", "NameSportString", teamSeason.FederationSeasonId);
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "NameSportString", teamSeason.TeamId);
            return View(teamSeason);
        }

        // GET: TeamSeasons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamSeason teamSeason = db.TeamSeasons.Find(id);
            if (teamSeason == null)
            {
                return HttpNotFound();
            }
            return View(teamSeason);
        }

        // POST: TeamSeasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamSeason teamSeason = db.TeamSeasons.Find(id);
            db.TeamSeasons.Remove(teamSeason);
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
