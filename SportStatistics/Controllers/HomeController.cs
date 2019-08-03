using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportStatistics.Models.ViewModels;
using SportStatistics.Models.ServiceClasses;
using SportStatistics.Models;
using System.IO;
using System.Text;

namespace SportStatistics.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Standings(string sport, string fed, string tour, int? fedSeason)
        {
            try
            {
                List<Standings> standings = new ServiceDatabase().CreateModelStandings(sport, fed, tour, fedSeason);
                return View(standings.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = myException.Message + " " + e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Standings(FormCollection collection)
        {
            try
            {
                int fedSeason = Convert.ToInt32(collection[1]);
                string season = collection[0];
                List<Standings> standings = new ServiceDatabase().CreateModelStandings(null, null, season, fedSeason);
                return View(standings.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult StandingsHome(int fedSeason)
        {
            try
            {
                List<Standings> standings = new ServiceDatabase().CreateModelStandingsHome(fedSeason);
                return View("Standings", standings.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult StandingsAway(int fedSeason)
        {
            try
            {
                List<Standings> standings = new ServiceDatabase().CreateModelStandingsAway(fedSeason);
                return View("Standings", standings.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult LeagueGroups(int fedSeason)
        {
            try
            {
                //List<Standings> standingsGroups = new ServiceDatabase().CreateModelStandingsGroups(fedSeason);
                //return View("Standings", standings.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Team(int fedSeason, int TeamId)
        {
            try
            {
                ShowTeam team = new ServiceDatabase().CreateModelsShowTeam(TeamId, fedSeason);
                return View(team);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Scorers(int? fedSeason, FormCollection collection)
        {
            try
            {   
                if (fedSeason == null)
                {
                   // fedSeason = Convert.ToInt32(collection[1]);
                }             
                List<Scorer> scorers = new ServiceDatabase().Scorers(fedSeason, null);
                return View(scorers);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Scorers(FormCollection collection)
        {
            try
            {
                int fedSeason = Convert.ToInt32(collection[1]);
                string season = collection[0];
                List<Scorer> scorers = new ServiceDatabase().Scorers(fedSeason, season);
                return View(scorers);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Player(int fedSeason, int PlayerId)
        {
            try
            {
                ShowPlayer player = new ServiceDatabase().ShowPlayer(fedSeason, PlayerId);
                return View(player);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ArchScorers(int fedSeason)
        {
            try
            {
                List<Scorer> scorers = new ServiceDatabase().ArchScorers(fedSeason);
                return View(scorers);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Winners(int fedSeason)
        {
            try
            {
                List<Winner> Winners = new ServiceDatabase().Winners(fedSeason);
                return View(Winners);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Progress(int fedSeason)
        {
            try
            {
                List<Progress> progresses = new ServiceDatabase().Progresses(fedSeason);
                return View(progresses);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ProgressHome(int fedSeason)
        {
            try
            {
                List<Progress> progresses = new ServiceDatabase().ProgressesHome(fedSeason);
                return View("Progress", progresses);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ProgressAway(int fedSeason)
        {
            try
            {
                List<Progress> progresses = new ServiceDatabase().ProgressesAway(fedSeason);
                return View("Progress", progresses);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Match(int MatchId)
        {
            try
            {
                ShowMatch Match = new ServiceDatabase().ShowMatch(MatchId);
                return View(Match);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Result(int fedSeason)
        {
            try
            {
                int countTour = 0;
                List<Score> result = new ServiceDatabase().Result(ref countTour, fedSeason, null);                
                string[] array = new string[countTour];
                for (int i = 1; i <= countTour; i++)
                {
                    array[i - 1] = Convert.ToString(i);
                }               

                ViewData["myList"] = new SelectList(array.Select(x => new { value = x, text = x }),
                                                    "value", "text", countTour);
                return View(result);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Result(FormCollection collection)
        {
            try
            {
                string season = collection[0];
                int tour = Convert.ToInt32(collection[1]);
                int fedSeason = Convert.ToInt32(collection[2]);
                int countTour = 0;
                List<Score> result = new ServiceDatabase().Result(ref countTour, fedSeason, season, tour);
                
                string[] array = new string[countTour];
                for (int i = 1; i <= countTour; i++)
                {
                    array[i - 1] = Convert.ToString(i);
                }
                ViewData["myList"] = new SelectList(array.Select(x => new { value = x, text = x }),
                                                    "value", "text", tour);
                return View(result);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Stub(string stub)
        {
            List<string> list = new ServiceDatabase().Stub(stub);
            return View(list);
        }

        [HttpGet]
        public ActionResult AddData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddData(List<HttpPostedFileBase> filesSF, List<HttpPostedFileBase> filesP,
            List<HttpPostedFileBase> filesT, List<HttpPostedFileBase> filesFS, List<HttpPostedFileBase> filesTS,
            List<HttpPostedFileBase> filesPS, List<HttpPostedFileBase> filesM)
        {
            try
            {                
                new ServiceClasses().Add(filesSF, filesP, filesT, filesFS, filesTS, filesPS, filesM);                    
                return View();
            }
            catch(Exception e)
            {
                ViewBag.Error = myException.Message;
                ViewBag.Message = e.Message;
                return View("Error");
            }
        }
    }
}