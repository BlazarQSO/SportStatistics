using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportStatistics.Models.ViewModels;
using SportStatistics.Models.ServiceClasses;

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
        public ActionResult Scorers(int fedSeason)
        {
            try
            {
                List<Scorer> scorers = new ServiceDatabase().Scorers(fedSeason);
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
            return View();
        }

        [HttpGet]
        public ActionResult AddData()
        {
            return View();
        }
    }
}
