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
    public class TeamsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Teams
        public ActionResult Index()
        {
            try
            {
                return View(db.Teams.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string edit)
        {
            try
            {
                List<Team> teams = new ServiceSearch().SearchTeams(edit);
                return View(teams);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Team team = db.Teams.Find(id);
                if (team == null)
                {
                    return HttpNotFound();
                }
                return View(team);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamId,NameSportString,Name,Country,City,NameStadium,FoundationDate")] Team team, HttpPostedFileBase upload)
        {
            try
            {
                string fileName = "";
                if (upload != null)
                {
                    fileName = System.IO.Path.GetFileName(upload.FileName);
                    int index = fileName.LastIndexOf(".");
                    fileName = fileName.Substring(index).ToLower();
                    if (fileName != ".jpg" && fileName != ".png" && fileName != ".jpeg" && fileName != ".gif")
                    {
                        ModelState.AddModelError("TeamId", "Allowed only: jpg, png, jpeg, gif");
                    }
                }
                if (string.IsNullOrEmpty(team.Name))
                {
                    ModelState.AddModelError("Name", "Enter the Name");
                }
                if (string.IsNullOrEmpty(team.Country))
                {
                    ModelState.AddModelError("Country", "Enter the Country");
                }

                if (ModelState.IsValid)
                {
                    db.Teams.Add(team);
                    db.SaveChanges();

                    if (fileName != "")
                    {
                        int last = (from c in db.Teams select c.TeamId).ToList().Last();
                        upload.SaveAs(Server.MapPath("~/images/teams/" + last + ".jpg"));
                    }
                    return RedirectToAction("Index");
                }

                return View(team);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Team team = db.Teams.Find(id);
                if (team == null)
                {
                    return HttpNotFound();
                }
                return View(team);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,NameSportString,Name,Country,City,NameStadium,FoundationDate")] Team team, HttpPostedFileBase upload)
        {
            try
            {
                string fileName = "";
                if (upload != null)
                {
                    fileName = System.IO.Path.GetFileName(upload.FileName);
                    int index = fileName.LastIndexOf(".");
                    fileName = fileName.Substring(index).ToLower();
                    if (fileName != ".jpg" && fileName != ".png" && fileName != ".jpeg" && fileName != ".gif")
                    {
                        ModelState.AddModelError("TeamId", "Allowed only: jpg, png, jpeg, gif");
                    }
                }
                if (string.IsNullOrEmpty(team.Name))
                {
                    ModelState.AddModelError("Name", "Enter the Name");
                }
                if (string.IsNullOrEmpty(team.Country))
                {
                    ModelState.AddModelError("Country", "Enter the Country");
                }

                if (ModelState.IsValid)
                {
                    db.Entry(team).State = EntityState.Modified;
                    db.SaveChanges();
                    if (fileName != "")
                    {
                        upload.SaveAs(Server.MapPath("~/images/teams/" + team.TeamId + ".jpg"));
                    }
                    return RedirectToAction("Index");
                }
                return View(team);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Team team = db.Teams.Find(id);
                if (team == null)
                {
                    return HttpNotFound();
                }
                return View(team);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Team team = db.Teams.Find(id);
                db.Teams.Remove(team);
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
