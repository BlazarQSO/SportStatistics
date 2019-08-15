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
    [Authorize(Roles = "Admin")]
    public class SportsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Sports
        public ActionResult Index()
        {
            try
            {
                return View(db.Sports.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Sports/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sport sport = db.Sports.Find(id);
                if (sport == null)
                {
                    return HttpNotFound();
                }
                return View(sport);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Sports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SportId,NameSportString")] Sport sport)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string cmpr = sport.NameSportString;
                    var search = from c in db.Sports
                                 where c.NameSportString == cmpr
                                 select c;
                    if (search.Count() < 1)
                    {
                        db.Sports.Add(sport);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(sport);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Sports/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sport sport = db.Sports.Find(id);
                if (sport == null)
                {
                    return HttpNotFound();
                }
                return View(sport);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: Sports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SportId,NameSportString")] Sport sport)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(sport).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sport);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Sports/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sport sport = db.Sports.Find(id);
                if (sport == null)
                {
                    return HttpNotFound();
                }
                return View(sport);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: Sports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Sport sport = db.Sports.Find(id);
                db.Sports.Remove(sport);
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
