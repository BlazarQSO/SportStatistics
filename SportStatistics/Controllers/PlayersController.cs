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
    public class PlayersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Players
        public ActionResult Index()
        {
            try
            {
                return View(db.Players.ToList());
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
                List<Player> players = new ServiceSearch().SearchPlayers(edit);
                return View(players);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Player player = db.Players.Find(id);
                if (player == null)
                {
                    return HttpNotFound();
                }
                return View(player);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerId,NameSportString,Name,Surname,Birthday,Age,Nationality,Weight,Height,PositionString")] Player player, HttpPostedFileBase upload)
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
                        ModelState.AddModelError("PlayerId", "allowed only: .jpg, .png, .jpeg, .gif");
                    }
                }
                if (string.IsNullOrEmpty(player.Name))
                {
                    ModelState.AddModelError("Name", "Enter the Name");
                }

                if (ModelState.IsValid)
                {
                    db.Players.Add(player);
                    db.SaveChanges();
                    int last = (from c in db.Players select c.PlayerId).ToList().Last();
                    if (fileName != "")
                    {
                        upload.SaveAs(Server.MapPath("~/images/players/" + last + ".jpg"));
                    }
                    return RedirectToAction("Index");
                }

                return View(player);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Player player = db.Players.Find(id);
                if (player == null)
                {
                    return HttpNotFound();
                }
                return View(player);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerId,NameSportString,Name,Surname,Birthday,Age,Nationality,Weight,Height,PositionString")] Player player, HttpPostedFileBase upload)
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
                        ModelState.AddModelError("PlayerId", "allowed only: .jpg, .png, .jpeg, .gif");
                    }
                }
                if (string.IsNullOrEmpty(player.Name))
                {
                    ModelState.AddModelError("Name", "Enter the Name");
                }
                if (ModelState.IsValid)
                {
                    db.Entry(player).State = EntityState.Modified;
                    db.SaveChanges();
                    if (fileName != "")
                    {
                        upload.SaveAs(Server.MapPath("~/images/players/" + player.PlayerId + ".jpg"));
                    }
                    return RedirectToAction("Index");
                }
                return View(player);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Player player = db.Players.Find(id);
                if (player == null)
                {
                    return HttpNotFound();
                }
                return View(player);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Player player = db.Players.Find(id);
                db.Players.Remove(player);
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
