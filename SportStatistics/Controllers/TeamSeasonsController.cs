using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportStatistics.Models;
using SportStatistics.Models.ServiceClasses;

namespace SportStatistics.Controllers
{
    public class TeamSeasonsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: TeamSeasons
        public ActionResult Index()
        {
            try
            {
                int[] array = new int[Enum.GetValues(typeof(Season)).Length];
                int i = 0;
                foreach (Season item in Enum.GetValues(typeof(Season)))
                {
                    array[i] = (int)item;
                    i++;
                }

                ViewData["myList"] = new SelectList(array.Select(x => new { value = x, text = x + "/" + (x + 1) }),
                                                    "value", "text", "2018");
                var teamSeasons = db.TeamSeasons.Include(t => t.FederationSeason).Include(t => t.Team);
                return View(teamSeasons.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Index(string edit, FormCollection collection)
        {
            try
            {
                string season = collection[1];
                List<TeamSeason> TeamSeasons = new ServiceSearch().SearchTeamSeasons(edit, season);

                int[] array = new int[Enum.GetValues(typeof(Season)).Length];
                int i = 0;
                foreach (Season item in Enum.GetValues(typeof(Season)))
                {
                    array[i] = (int)item;
                    i++;
                }

                ViewData["myList"] = new SelectList(array.Select(x => new { value = x, text = x + "/" + (x + 1) }),
                                                    "value", "text", season);
                return View(TeamSeasons);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: TeamSeasons/Details/5
        public ActionResult Details(int? id)
        {
            try
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
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: TeamSeasons/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.FederationSeasonId = new SelectList(db.FederationSeasons, "FederationSeasonId", "TournamentString");
                ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "NameSportString");
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: TeamSeasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamSeasonId,NameTeam,SeasonString,Played,HomePlayed,Point,HomePoint,Win,HomeWin,Draw,HomeDraw,Lose,HomeLose,Goals,HomeGoals,GoalAgainst,HomeGoalAgainst,TeamId,FederationSeasonId")] TeamSeason teamSeason)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.TeamSeasons.Add(teamSeason);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.FederationSeasonId = new SelectList(db.FederationSeasons, "FederationSeasonId", "TournamentString", teamSeason.FederationSeasonId);
                ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "NameSportString", teamSeason.TeamId);
                return View(teamSeason);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: TeamSeasons/Edit/5
        public ActionResult Edit(int? id)
        {
            try
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
                ViewBag.FederationSeasonId = new SelectList(db.FederationSeasons, "FederationSeasonId", "TournamentString", teamSeason.FederationSeasonId);
                ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "NameSportString", teamSeason.TeamId);
                return View(teamSeason);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: TeamSeasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamSeasonId,NameTeam,SeasonString,Played,HomePlayed,Point,HomePoint,Win,HomeWin,Draw,HomeDraw,Lose,HomeLose,Goals,HomeGoals,GoalAgainst,HomeGoalAgainst,TeamId,FederationSeasonId")] TeamSeason teamSeason)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TeamSeason find = db.TeamSeasons.Find(teamSeason.TeamSeasonId);

                    find.Draw = teamSeason.Draw;
                    find.GoalAgainst = teamSeason.GoalAgainst;
                    find.Goals = teamSeason.Goals;
                    find.HomeDraw = teamSeason.HomeDraw;
                    find.HomeGoalAgainst = teamSeason.HomeGoalAgainst;
                    find.HomeGoals = teamSeason.HomeGoals;
                    find.HomeLose = teamSeason.HomeLose;
                    find.HomePlayed = teamSeason.HomePlayed;
                    find.HomePoint = teamSeason.HomePoint;
                    find.HomeWin = teamSeason.HomeWin;
                    find.Lose = teamSeason.Lose;
                    find.Played = teamSeason.Played;
                    find.Point = teamSeason.Point;
                    find.Win = teamSeason.Win;
                    db.Entry(find).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.FederationSeasonId = new SelectList(db.FederationSeasons, "FederationSeasonId", "TournamentString", teamSeason.FederationSeasonId);
                ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "NameSportString", teamSeason.TeamId);
                return View(teamSeason);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: TeamSeasons/Delete/5
        public ActionResult Delete(int? id)
        {
            try
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
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: TeamSeasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                TeamSeason teamSeason = db.TeamSeasons.Find(id);
                db.TeamSeasons.Remove(teamSeason);
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
