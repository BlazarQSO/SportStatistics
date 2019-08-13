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
    public class MatchesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Matches
        public ActionResult Index()
        {
            try
            {
                return View(db.Matches.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Index(string edit)
        {
            try
            {
                List<Match> matches = new ServiceSearch().SearchMatches(edit);
                return View(matches);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Matches/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Match match = db.Matches.Find(id);
                if (match == null)
                {
                    return HttpNotFound();
                }
                return View(match);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Matches/Create
        public ActionResult Create()
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
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MatchId,NameSportString,Country,SeasonString,TournamentString,NameTournament,Tour,Date,NameStadium,HomeTeam,AwayTeam,HomeTeamGoal,AwayTeamGoal,HomeTeamResultString,AwayTeamResultString,HomePlayers,AwayPlayers,TimeLineHome,TimeLineAway")] Match match, FormCollection collection)
        {
            try
            {
                string season = collection[4];
                if (string.IsNullOrEmpty(match.HomeTeam))
                {
                    ModelState.AddModelError("HomeTeam", "Enter the data");
                }
                else
                {
                    string hometeam = match.HomeTeam;
                    var search = from c in db.Teams
                                 where c.Name == hometeam
                                 select c;
                    if (search.Count() < 1)
                    {
                        ModelState.AddModelError("HomeTeam", "Team not found");
                    }
                }

                if (string.IsNullOrEmpty(match.AwayTeam))
                {
                    ModelState.AddModelError("AwayTeam", "Enter the data");
                }
                else
                {
                    string awayteam = match.AwayTeam;
                    var search = from c in db.Teams
                                 where c.Name == awayteam
                                 select c;
                    if (search.Count() < 1)
                    {
                        ModelState.AddModelError("AwayTeam", "Team not found");
                    }
                }
                if (string.IsNullOrEmpty(match.Date))
                {
                    ModelState.AddModelError("Date", "Enter the date");
                }
                if (string.IsNullOrEmpty(match.Country))
                {
                    ModelState.AddModelError("Country", "Enter the data");
                }
                else
                {
                    string country = match.Country;
                    var search = from c in db.SportFederations
                                 where c.Country == country
                                 select c;
                    if (search.Count() < 1)
                    {
                        ModelState.AddModelError("Country", "Federation not found");
                    }
                }
                if (season == "" || season == null)
                {
                    ModelState.AddModelError("Season", "Choose a season");
                }

                if (ModelState.IsValid)
                {
                    match.Season = season.ParseEnum<Season>();
                    db.Matches.Add(match);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                int[] array = new int[Enum.GetValues(typeof(Season)).Length];
                int i = 0;
                foreach (Season item in Enum.GetValues(typeof(Season)))
                {
                    array[i] = (int)item;
                    i++;
                }

                ViewData["myList"] = new SelectList(array.Select(x => new { value = x, text = x + "/" + (x + 1) }),
                                                    "value", "text", season);

                return View(match);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Matches/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Match match = db.Matches.Find(id);
                if (match == null)
                {
                    return HttpNotFound();
                }

                int[] array = new int[Enum.GetValues(typeof(Season)).Length];
                int i = 0;
                foreach (Season item in Enum.GetValues(typeof(Season)))
                {
                    array[i] = (int)item;
                    i++;
                }
                string season = (int)match.Season + "";
                ViewData["myList"] = new SelectList(array.Select(x => new { value = x, text = x + "/" + (x+1) }),
                                                    "value", "text", season);

                return View(match);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MatchId,NameSportString,Country,SeasonString,TournamentString,NameTournament,Tour,Date,NameStadium,HomeTeam,AwayTeam,HomeTeamGoal,AwayTeamGoal,HomeTeamResultString,AwayTeamResultString,HomePlayers,AwayPlayers,TimeLineHome,TimeLineAway")] Match match, FormCollection collection)
        {
            try
            {
                string season = collection[4];
                if (string.IsNullOrEmpty(match.HomeTeam))
                {
                    ModelState.AddModelError("HomeTeam", "Enter the data");
                }
                else
                {
                    string hometeam = match.HomeTeam;
                    var search = from c in db.Teams
                                 where c.Name == hometeam
                                 select c;
                    if (search.Count() < 1)
                    {
                        ModelState.AddModelError("HomeTeam", "Team not found");
                    }
                }

                if (string.IsNullOrEmpty(match.AwayTeam))
                {
                    ModelState.AddModelError("AwayTeam", "Enter the data");
                }
                else
                {
                    string awayteam = match.AwayTeam;
                    var search = from c in db.Teams
                                 where c.Name == awayteam
                                 select c;
                    if (search.Count() < 1)
                    {
                        ModelState.AddModelError("AwayTeam", "Team not found");
                    }
                }
                if (string.IsNullOrEmpty(match.Date))
                {
                    ModelState.AddModelError("Date", "Enter the date");
                }
                if (string.IsNullOrEmpty(match.Country))
                {
                    ModelState.AddModelError("Country", "Enter the data");
                }
                else
                {
                    string country = match.Country;
                    var search = from c in db.SportFederations
                                 where c.Country == country
                                 select c;
                    if (search.Count() < 1)
                    {
                        ModelState.AddModelError("Country", "Federation not found");
                    }
                }
                if (season == "" || season == null)
                {
                    ModelState.AddModelError("Season", "Choose a season");
                }

                if (ModelState.IsValid)
                {
                    Match find = db.Matches.Find(match.MatchId);
                    find.AwayPlayers = match.AwayPlayers;
                    find.AwayTeam = match.AwayTeam;
                    find.AwayTeamGoal = match.AwayTeamGoal;
                    find.AwayTeamResult = match.AwayTeamResult;
                    find.Country = match.Country;
                    find.Date = match.Date;
                    find.HomePlayers = match.HomePlayers;
                    find.HomeTeam = match.HomeTeam;
                    find.HomeTeamGoal = match.HomeTeamGoal;
                    find.HomeTeamResult = match.HomeTeamResult;                    
                    find.NameSport = match.NameSport;
                    find.NameStadium = match.NameStadium;
                    find.NameTournament = match.NameTournament;
                    find.Season = season.ParseEnum<Season>();
                    find.TimeLineAway = match.TimeLineAway;
                    find.TimeLineHome = match.TimeLineHome;
                    find.Tour = match.Tour;
                    find.Tournament = match.Tournament;

                    db.Entry(find).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                int[] array = new int[Enum.GetValues(typeof(Season)).Length];
                int i = 0;
                foreach (Season item in Enum.GetValues(typeof(Season)))
                {
                    array[i] = (int)item;
                    i++;
                }

                ViewData["myList"] = new SelectList(array.Select(x => new { value = x, text = x + "/" + (x + 1) }),
                                                    "value", "text", season);
                return View(match);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Matches/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Match match = db.Matches.Find(id);
                if (match == null)
                {
                    return HttpNotFound();
                }
                return View(match);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Match match = db.Matches.Find(id);
                db.Matches.Remove(match);
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
