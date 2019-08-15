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
    public class SportFederationsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SportFederations
        public ActionResult Index()
        {
            try
            {
                var sportFederations = db.SportFederations.Include(s => s.Sport);
                return View(sportFederations.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: SportFederations/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SportFederation sportFederation = db.SportFederations.Find(id);
                if (sportFederation == null)
                {
                    return HttpNotFound();
                }
                return View(sportFederation);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: SportFederations/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.SportId = new SelectList(db.Sports, "SportId", "NameSportString");
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: SportFederations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SportFederationId,NameSportString,Country,FoundationDate,NamePresident,SportId")] SportFederation sportFederation)
        {
            try
            {
                if (string.IsNullOrEmpty(sportFederation.Country))
                {
                    ModelState.AddModelError("Country", "Enter Country or Federation");
                }
                if (ModelState.IsValid)
                {
                    db.SportFederations.Add(sportFederation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.SportId = new SelectList(db.Sports, "SportId", "NameSportString", sportFederation.SportId);
                return View(sportFederation);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: SportFederations/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SportFederation sportFederation = db.SportFederations.Find(id);
                if (sportFederation == null)
                {
                    return HttpNotFound();
                }
                ViewBag.SportId = new SelectList(db.Sports, "SportId", "NameSportString", sportFederation.SportId);
                return View(sportFederation);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: SportFederations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SportFederationId,NameSportString,Country,FoundationDate,NamePresident,SportId")] SportFederation sportFederation)
        {
            try
            {
                if (string.IsNullOrEmpty(sportFederation.Country))
                {
                    ModelState.AddModelError("Country", "Enter Country or Federation");
                }
                if (ModelState.IsValid)
                {
                    db.Entry(sportFederation).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.SportId = new SelectList(db.Sports, "SportId", "NameSportString", sportFederation.SportId);
                return View(sportFederation);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: SportFederations/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SportFederation sportFederation = db.SportFederations.Find(id);
                if (sportFederation == null)
                {
                    return HttpNotFound();
                }
                return View(sportFederation);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: SportFederations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                SportFederation sportFederation = db.SportFederations.Find(id);
                db.SportFederations.Remove(sportFederation);
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
