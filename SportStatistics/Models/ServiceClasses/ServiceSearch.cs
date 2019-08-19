using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SportStatistics.Models.ServiceClasses
{
    public class ServiceSearch
    {
        DatabaseContext db = new DatabaseContext();

        public List<Team> SearchTeams(string edit)
        {
            List<Team> list = new List<Team>();
            string search = edit;
            search = Regex.Replace(search, "[ ]+", " ");
            search = search.Trim();
            if (search != "" && search != null)
            {
                var searchTeams = from c in db.Teams
                                  where c.Name.ToLower().IndexOf(search) >= 0
                                  select c;
                if (searchTeams.Count() > 0)
                {
                    list = searchTeams.ToList();
                }
            }
            else
            {
                return db.Teams.ToList();
            }
          
            return list;
        }

        public List<Player> SearchPlayers(string edit)
        {
            List<Player> list = new List<Player>();
            string search = edit;
            search = Regex.Replace(search, "[ ]+", " ");
            search = search.Trim();
            if (search != "" && search != null)
            {
                var searchTeams = from c in db.Players
                                  where c.Name.ToLower().IndexOf(search) >= 0 ||
                                  c.Surname.ToLower().IndexOf(search) >= 0 ||
                                  (c.Name + " " + c.Surname).ToLower().IndexOf(search) >=0 ||
                                  (c.Surname + " " + c.Name).ToLower().IndexOf(search) >=0
                                  select c;
                if (searchTeams.Count() > 0)
                {
                    list = searchTeams.ToList();
                }
            }
            else
            {
                return db.Players.ToList();
            }

            return list;
        }

        public List<Match> SearchMatches(string edit)
        {
            List<Match> list = new List<Match>();
            string search = edit;
            search = Regex.Replace(search, "[ ]+", " ");
            search = search.Trim();
            if (search != "" && search != null)
            {
                var searchTeams = from c in db.Matches
                                  where 
                                  c.HomeTeam.ToLower().IndexOf(search) >= 0 ||
                                  c.AwayTeam.ToLower().IndexOf(search) >= 0 ||
                                  (c.HomeTeam.ToLower() + " " + c.AwayTeam.ToLower()).IndexOf(search) >= 0 
                                  select c;
                if (searchTeams.Count() > 0)
                {
                    list = searchTeams.ToList();
                }
            }
            else
            {
                return db.Matches.ToList();
            }

            return list;
        }

        public List<FederationSeason> SearchFederationSeasons(string edit, string season)
        {
            List<FederationSeason> list = new List<FederationSeason>();
            string search = edit;
            search = Regex.Replace(search, "[ ]+", " ");
            search = search.Trim();
            if (search != "" && search != null)
            {
                string searchSeason = Enum.Format(typeof(Season), Convert.ToInt32(season), "G");
                var searchTeams = from c in db.FederationSeasons
                                  where
                                  c.SeasonString == searchSeason &&
                                  c.SportFederation.Country.ToLower().IndexOf(search) >= 0
                                  select c;
                if (searchTeams.Count() > 0)
                {
                    list = searchTeams.ToList();
                }
            }
            else
            {
                return db.FederationSeasons.ToList();
            }

            return list;
        }

        public List<TeamSeason> SearchTeamSeasons(string edit, string season)
        {
            List<TeamSeason> list = new List<TeamSeason>();
            string search = edit;
            search = Regex.Replace(search, "[ ]+", " ");
            search = search.Trim();
            if (search != "" && search != null)
            {
                string searchSeason = Enum.Format(typeof(Season), Convert.ToInt32(season), "G");
                var searchTeams = from c in db.TeamSeasons
                                  where
                                  c.SeasonString == searchSeason &&
                                  c.NameTeam.ToLower().IndexOf(search) >= 0
                                  select c;
                if (searchTeams.Count() > 0)
                {
                    list = searchTeams.ToList();
                }
            }
            else
            {
                return db.TeamSeasons.ToList();
            }

            return list;
        }

        public List<PlayerSeason> SearchPlayerSeasons(string edit, string season)
        {
            List<PlayerSeason> list = new List<PlayerSeason>();
            string search = edit;
            search = Regex.Replace(search, "[ ]+", " ");
            search = search.Trim();
            if (search != "" && search != null)
            {
                string searchSeason = Enum.Format(typeof(Season), Convert.ToInt32(season), "G");
                var searchTeams = from c in db.PlayerSeasons
                                  where
                                  c.SeasonString == searchSeason &&
                                  (c.Player.Name.ToLower().IndexOf(search) >= 0 ||
                                  c.Player.Surname.ToLower().IndexOf(search) >= 0 ||
                                  (c.Player.Name + " " + c.Player.Surname).ToLower().IndexOf(search) >= 0 ||
                                  (c.Player.Surname + " " + c.Player.Name).ToLower().IndexOf(search) >= 0)
                                  select c;
                if (searchTeams.Count() > 0)
                {
                    list = searchTeams.ToList();
                }
            }
            else
            {
                return db.PlayerSeasons.ToList();
            }

            return list;
        }

        public string NewPassword(int count = 10)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-*";
            char[] chars = new char[count];
            Random rd = new Random();
            for (int i = 0; i < 10; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }
    }    
}