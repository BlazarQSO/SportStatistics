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
        public ActionResult LeagueGroups(string sport, string fed, string tour, int? fedSeason)
        {
            try
            {                
                if (fedSeason == null)
                {
                    if (sport == null && fed == null && tour == null)
                    {
                        sport = "Football";
                        fed = "UEFA";
                        tour = "League";
                    }
                    fedSeason = new ServiceDatabase().Find(sport, fed, tour);
                }
                List<string> group = new List<string>();
                List<List<Score>> score = new List<List<Score>>();                
                List<List<Standings>> standingsGroups = new ServiceDatabase().CreateModelStandingsGroups(Convert.ToInt32(fedSeason), ref score, null, ref group);
                ViewBag.Score = score;
                ViewBag.Group = group;
                return View("LeagueGroups", standingsGroups.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }            
        }

        [HttpPost]
        public ActionResult LeagueGroups(int? fedSeason, string season, FormCollection collection)
        {
            try
            {
                int fedId = 0;
                if (fedSeason != null)
                {
                    fedId = Convert.ToInt32(fedSeason);
                }
                else
                {
                    season = collection[0].Split(',')[0];
                    fedId = Convert.ToInt32(collection[1]);
                }
                List<string> group = new List<string>();
                List<List<Score>> score = new List<List<Score>>();
                List<List<Standings>> standingsGroups = new ServiceDatabase().CreateModelStandingsGroups(fedId, ref score, season, ref group);
                ViewBag.Score = score;
                ViewBag.Group = group;
                return View("LeagueGroups", standingsGroups.ToList());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Team(int? fedSeason, int? TeamId)
        {
            try
            {
                ShowTeam team = new ServiceDatabase().CreateModelsShowTeam(Convert.ToInt32(TeamId), Convert.ToInt32(fedSeason));
                return View(team);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Scorers(int? fedSeason)
        {
            try
            {
                string season = null;
                if (fedSeason == null)
                {                    
                    if (Request.Cookies["Id"] != null)
                    {
                        fedSeason = Convert.ToInt32(Request.Cookies["Id"].Value);
                        Response.Cookies["Id"].Expires = DateTime.Now.AddDays(-1);
                    }
                }             
                List<Scorer> scorers = new ServiceDatabase().Scorers(fedSeason, season);
                return View(scorers);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Scorers(int? fedSeason, FormCollection collection)
        {
            try
            {
                fedSeason = Convert.ToInt32(collection[1]);
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
        public ActionResult Player(int? fedSeason, int? PlayerId)
        {
            try
            {
                ShowPlayer player = new ShowPlayer();
                if (fedSeason != null && PlayerId != null)
                {
                    player = new ServiceDatabase().ShowPlayer(Convert.ToInt32(fedSeason), Convert.ToInt32(PlayerId));                    
                }
                
                return View(player);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ArchScorers(int? fedSeason)
        {
            try
            {
                List<Scorer> scorers = new List<Scorer>();
                if (fedSeason == null)
                {
                    scorers = new ServiceDatabase().ArchScorers(10);
                }
                else
                {
                    scorers = new ServiceDatabase().ArchScorers(Convert.ToInt32(fedSeason));
                }             
                return View(scorers);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Winners(int? fedSeason)
        {
            try
            {
                List<Winner> Winners = new ServiceDatabase().Winners(Convert.ToInt32(fedSeason));
                return View(Winners);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Progress(int? fedSeason)
        {
            try
            {
                List<Progress> progresses = new ServiceDatabase().Progresses(Convert.ToInt32(fedSeason));
                return View(progresses);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ProgressHome(int? fedSeason)
        {
            try
            {
                List<Progress> progresses = new ServiceDatabase().ProgressesHome(Convert.ToInt32(fedSeason));
                return View("Progress", progresses);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ProgressAway(int? fedSeason)
        {
            try
            {
                List<Progress> progresses = new ServiceDatabase().ProgressesAway(Convert.ToInt32(fedSeason));
                return View("Progress", progresses);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Match(int? MatchId)
        {
            try
            {
                ShowMatch Match = new ShowMatch();
                if (MatchId != null)
                {
                    Match = new ServiceDatabase().ShowMatch(Convert.ToInt32(MatchId));
                }                              
                return View(Match);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Result(int? fedSeason)
        {
            try
            {
                int countTour = 0;
                List<Score> result = new ServiceDatabase().Result(ref countTour, Convert.ToInt32(fedSeason), null);                
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

        [HttpPost]
        public ActionResult Search(string edit)
        {
            try
            {
                List<List<List<string>>> search = new ServiceDatabase().Search(edit);
                return View("Search", search);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
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

        public ActionResult Example()
        {
            return View();
        }
    }
}