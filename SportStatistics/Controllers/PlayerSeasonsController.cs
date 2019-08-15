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
    [Authorize(Roles = "Admin")]
    public class PlayerSeasonsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: PlayerSeasons
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
                var playerSeasons = db.PlayerSeasons.Include(p => p.Player).Include(p => p.TeamSeason);
                return View(playerSeasons.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string edit, FormCollection collection)
        {
            try
            {
                string season = collection[1];
                List<PlayerSeason> PlayerSeasons = new ServiceSearch().SearchPlayerSeasons(edit, season);

                int[] array = new int[Enum.GetValues(typeof(Season)).Length];
                int i = 0;
                foreach (Season item in Enum.GetValues(typeof(Season)))
                {
                    array[i] = (int)item;
                    i++;
                }

                ViewData["myList"] = new SelectList(array.Select(x => new { value = x, text = x + "/" + (x + 1) }),
                                                    "value", "text", season);
                return View(PlayerSeasons);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: PlayerSeasons/Details/5
        public ActionResult Details(int? id)
        {
            try
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
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: PlayerSeasons/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.PlayerId = new SelectList(db.Players, "PlayerId", "NameSportString");
                ViewBag.TeamSeasonId = new SelectList(db.TeamSeasons, "TeamSeasonId", "NameTeam");
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: PlayerSeasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerSeasonId,SeasonString,GamedMatches,Goals,Assists,PlayerId,TeamSeasonId")] PlayerSeason playerSeason)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PlayerSeasons.Add(playerSeason);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.PlayerId = new SelectList(db.Players, "PlayerId", "NameSportString", playerSeason.PlayerId);
                ViewBag.TeamSeasonId = new SelectList(db.TeamSeasons, "TeamSeasonId", "NameTeam", playerSeason.TeamSeasonId);
                return View(playerSeason);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: PlayerSeasons/Edit/5
        public ActionResult Edit(int? id)
        {
            try
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
                ViewBag.TeamSeasonId = new SelectList(db.TeamSeasons, "TeamSeasonId", "NameTeam", playerSeason.TeamSeasonId);
                return View(playerSeason);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: PlayerSeasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerSeasonId,SeasonString,GamedMatches,Goals,Assists,PlayerId,TeamSeasonId")] PlayerSeason playerSeason)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PlayerSeason find = db.PlayerSeasons.Find(playerSeason.PlayerSeasonId);
                    find.Assists = playerSeason.Assists;
                    find.GamedMatches = playerSeason.GamedMatches;
                    find.Goals = playerSeason.Goals;
                    db.Entry(find).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                Player player = new Player();
                player.Name = (string)TempData["Name"];
                player.Surname = (string)TempData["Surname"];
                playerSeason.Player = player;
                return View(playerSeason);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: PlayerSeasons/Delete/5
        public ActionResult Delete(int? id)
        {
            try
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
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: PlayerSeasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PlayerSeason playerSeason = db.PlayerSeasons.Find(id);
                db.PlayerSeasons.Remove(playerSeason);
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
