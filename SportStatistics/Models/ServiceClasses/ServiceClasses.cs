using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace SportStatistics.Models.ServiceClasses
{
    public class ServiceClasses
    {
        DatabaseContext db = new DatabaseContext();

        public void Add(List<HttpPostedFileBase> files)
        {
            string result = "";
            if (files != null)
            {
                if (files[0] != null)
                {
                    result = new StreamReader(files[0].InputStream).ReadToEnd();
                    AddSportFederation(result);
                }
                if (files[1] != null)
                {                    
                    result = new StreamReader(files[1].InputStream).ReadToEnd();
                    AddFederationSeason(result);
                }
                if (files[2] != null)
                {                    
                    result = new StreamReader(files[2].InputStream).ReadToEnd();
                    AddPlayer(result);
                }
                if (files[3] != null)
                {                    
                    result = new StreamReader(files[3].InputStream).ReadToEnd();
                    AddTeam(result);
                }
                if (files[4] != null)
                {                 
                    result = new StreamReader(files[4].InputStream).ReadToEnd();
                    AddMatch(result);
                }                
            }
        }

        public void AddSportFederation(string data)
        {
            List<string> dataList = data.Split('&').ToList();

            foreach (string item in dataList)
            {
                SportFederation sportFederation = new SportFederation();
                string[] list = item.Split(',');

                var search = from c in db.SportFederation
                             where c.NameSport == list[0].ParseEnum<NameSport>() &&
                             c.Country == list[1]
                             select c;

                if (search.Count() == 0)
                {
                    sportFederation.NameSport = list[0].ParseEnum<NameSport>();
                    sportFederation.Country = list[1];
                    sportFederation.FoundationDate = list[2];
                    sportFederation.NamePresident = list[3];

                    Sport sport = (from c in db.Sports
                                   where c.NameSport == sportFederation.NameSport
                                   select c).First();
                    sportFederation.SportId = sport.SportId;
                    sportFederation.Sport = sport;

                    db.SportFederation.Add(sportFederation);
                    db.SaveChanges();
                }
            }            
        }

        public void AddFederationSeason(string data)
        {
            List<string> dataList = data.Split('&').ToList();

            foreach (string item in dataList)
            {
                FederationSeason federationSeason = new FederationSeason();
                string[] list = item.Split(',');

                var search = from c in db.FederationSeasons
                             where
                             c.NameSport == list[0].ParseEnum<NameSport>() &&
                             c.Country == list[1] &&
                             c.Season == list[4].ParseEnum<Season>()
                             select c;

                if (search.Count() == 0)
                {
                    federationSeason.NameSport = list[0].ParseEnum<NameSport>();
                    federationSeason.Country = list[1];
                    federationSeason.Tournament = list[2].ParseEnum<Tournament>();
                    federationSeason.NameTournament = list[3];
                    federationSeason.Season = list[4].ParseEnum<Season>();

                    SportFederation sportFed = (from c in db.SportFederation
                                                where c.Country == federationSeason.Country &&
                                   c.NameSport == federationSeason.NameSport
                                                select c).First();

                    federationSeason.SportFederationId = sportFed.SportFederationId;
                    federationSeason.SportFederation = sportFed;

                    db.FederationSeasons.Add(federationSeason);
                    db.SaveChanges();
                }
            }            
        }

        public void AddPlayer(string data)
        {
            List<string> dataList = data.Split('&').ToList();

            foreach (string item in dataList)
            {
                Player player = new Player();
                string[] list = item.Split(',');

                var search = from c in db.Players
                             where
                             c.NameSport == list[0].ParseEnum<NameSport>() &&
                             c.Name == list[1] &&
                             c.Surname == list[2] &&
                             c.Age == Convert.ToInt32(list[4])
                             select c;

                if (search.Count() == 0)
                {
                    player.NameSport = list[0].ParseEnum<NameSport>();
                    player.Name = list[1];
                    player.Surname = list[2];
                    player.Birthday = list[3];
                    player.Age = Convert.ToInt32(list[4]);
                    player.Nationality = list[5];
                    player.Weight = Convert.ToInt32(list[6]);
                    player.Height = Convert.ToInt32(list[7]);
                    player.Position = list[8].ParseEnum<Position>();

                    db.Players.Add(player);
                    db.SaveChanges();
                }
            }            
        }

        public void AddTeam(string data)
        {
            List<string> dataList = data.Split('&').ToList();

            foreach (string item in dataList)
            {
                Team team = new Team();
                string[] list = item.Split(',');

                var search = from c in db.Teams
                             where
                             c.NameSport == list[0].ParseEnum<NameSport>() &&
                             c.Name == list[1] &&
                             c.Country == list[2]
                             select c;

                if (search.Count() == 0)
                {
                    team.NameSport = list[0].ParseEnum<NameSport>();
                    team.Name = list[1];
                    team.Country = list[2];
                    team.City = list[3];
                    team.NameStadium = list[4];
                    team.FoundationDate = list[5];

                    db.Teams.Add(team);
                    db.SaveChanges();
                }
            }            
        }

        public void AddMatch(string data)
        {
            List<string> dataList = data.Split('&').ToList();

            foreach (string item in dataList)
            {
                string[] itemList = item.Split('|');

                Match match = new Match();
                string[] list = itemList[0].Split(',');

                var search = from c in db.Matches
                             where c.NameSport == list[0].ParseEnum<NameSport>() &&
                             c.HomeTeam == list[8] &&
                             c.AwayTeam == list[9] &&
                             c.Date == list[6]
                             select c;

                if (search.Count() > 0)
                {
                    continue;
                }

                match.NameSport = list[0].ParseEnum<NameSport>();
                match.Country = list[1];
                match.City = list[2];
                string season = Enum.Format(typeof(Season), Convert.ToInt32(list[3]), "G");
                match.Season = season.ParseEnum<Season>();
                match.Tournament = list[4].ParseEnum<Tournament>();
                match.Tour = list[5];
                match.Date = list[6];
                match.NameStadium = list[7];
                match.HomeTeam = list[8];
                match.AwayTeam = list[9];
                match.HomeTeamGoal = Convert.ToInt32(list[10]);
                match.AwayTeamGoal = Convert.ToInt32(list[11]);
                match.HomeTeamResult = list[12].ParseEnum<Result>();
                match.AwayTeamResult = list[13].ParseEnum<Result>();

                List<string> player = itemList[1].Split(',').ToList();                
                match.ListHomePlayers.AddRange(player);

                player = itemList[2].Split(',').ToList();
                match.ListAwayPlayers.AddRange(player);

                match.ListTimeLineHome = itemList[3].Split(',').ToList();
                match.ListTimeLineAway = itemList[4].Split(',').ToList();

                // FederationSeason

                FederationSeason fedSeason = new FederationSeason();
                var searchFS = from c in db.FederationSeasons
                               where c.Country == match.Country &&
                               c.NameSport == match.NameSport &&
                               c.Season == match.Season
                               select c;

                if (searchFS.Count() > 0)
                {
                    fedSeason = searchFS.First();
                    fedSeason.Matches.Add(match);

                    db.Entry(fedSeason).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    CreateFederationSeason(match);
                }

                // Team Home

                TeamSeason homeTeam = new TeamSeason();

                var searchHT = from c in db.TeamSeasons
                                      where
                                      c.FederationSeason.NameSport == match.NameSport &&
                                      c.FederationSeason.Country == match.Country &&
                                      c.Team.Name == match.HomeTeam
                                      select c;

                if (searchHT.Count() > 0)
                {
                    homeTeam = searchHT.First();
                }
                else
                {
                    homeTeam = CreateTeamSeason(match, "home");
                }

                if (match.HomeTeamResult == Result.Win)
                {
                    homeTeam.Win++;
                    homeTeam.HomeWin++;
                    homeTeam.Point += 3;
                    homeTeam.HomePoint += 3;
                }
                else
                if (match.HomeTeamResult == Result.Draw)
                {
                    homeTeam.Draw++;
                    homeTeam.HomeDraw++;
                    homeTeam.Point += 1;
                    homeTeam.HomePoint += 1;
                }
                else
                if (match.HomeTeamResult == Result.Lose)
                {
                    homeTeam.Lose++;
                    homeTeam.HomeLose++;
                }

                homeTeam.Goals += match.HomeTeamGoal;
                homeTeam.HomeGoals += match.HomeTeamGoal;

                homeTeam.GoalAgainst += match.AwayTeamGoal;
                homeTeam.HomeGoalAgainst += match.AwayTeamGoal;

                homeTeam.Matches.Add(match);
                db.Entry(homeTeam).State = EntityState.Modified;
                db.SaveChanges();

                match.TeamSeasons.Add(homeTeam);                

                // Team Away

                TeamSeason awayTeam = new TeamSeason();
                var searchAT = from c in db.TeamSeasons
                               where
                               c.FederationSeason.NameSport == match.NameSport &&
                               c.FederationSeason.Country == match.Country &&
                               c.Team.Name == match.AwayTeam
                               select c;

                if (searchAT.Count() > 0)
                {
                    awayTeam = searchAT.First();
                }
                else
                {
                    awayTeam = CreateTeamSeason(match, "away");
                }

                if (match.AwayTeamResult == Result.Win)
                {
                    awayTeam.Win++;
                    awayTeam.Point += 3;
                }
                else
                if (match.AwayTeamResult == Result.Draw)
                {
                    awayTeam.Draw++;
                    awayTeam.Point += 1;
                }
                else
                if (match.AwayTeamResult == Result.Lose)
                {
                    awayTeam.Lose++;
                    awayTeam.HomeLose++;
                }

                awayTeam.Goals += match.AwayTeamGoal;
                awayTeam.HomeGoals += match.AwayTeamGoal;

                awayTeam.GoalAgainst += match.HomeTeamGoal;

                awayTeam.Matches.Add(match);
                db.Entry(awayTeam).State = EntityState.Modified;
                db.SaveChanges();

                match.TeamSeasons.Add(awayTeam);                                        
                
                db.Matches.Add(match);
                db.SaveChanges();
                
                // PlayerSeason

                foreach (string homeplayer in match.ListHomePlayers)
                {
                    PlayerSeason playerSeason = new PlayerSeason();
                    var searchPSH = from c in db.PlayerSeasons
                                    where c.TeamSeason == homeTeam &&
                                                 c.Player.Name + " " + c.Player.Surname == homeplayer
                                    select c;

                    if (searchPSH.Count() > 0)
                    {
                        playerSeason = searchPSH.First();
                    }
                    else
                    {
                        playerSeason = CreatePlayerSeason(match, homeplayer);
                    }

                    playerSeason.GamedMatches++;
                    playerSeason.Matches.Add(match);

                    if (match.ListTimeLineHome != null)
                    {
                        foreach (string itemLine in match.ListTimeLineHome)
                        {
                            string[] actTimeLine = itemLine.Split(':');
                            if (actTimeLine[2] == homeplayer)
                            {
                                if (actTimeLine[0] == "G")
                                {
                                    playerSeason.Goals++;
                                }
                                if (actTimeLine[0] == "A")
                                {
                                    playerSeason.Assists++;
                                }
                            }
                        }
                    }

                    db.Entry(player).State = EntityState.Modified;
                    db.SaveChanges();
                }

                foreach (string awayplayer in match.ListAwayPlayers)
                {
                    PlayerSeason playerSeason = new PlayerSeason();
                    var searchPSA = from c in db.PlayerSeasons
                                                 where c.TeamSeason == homeTeam &&
                                                 c.Player.Name + " " + c.Player.Surname == awayplayer
                                                 select c;

                    if (searchPSA.Count() > 0)
                    {
                        playerSeason = searchPSA.First();
                    }
                    else
                    {
                        playerSeason = CreatePlayerSeason(match, awayplayer);
                    }


                    playerSeason.GamedMatches++;
                    playerSeason.Matches.Add(match);

                    if (match.ListTimeLineAway != null)
                    {
                        foreach (string itemLine in match.ListTimeLineAway)
                        {
                            string[] actTimeLine = itemLine.Split(':');
                            if (actTimeLine[2] == awayplayer)
                            {
                                if (actTimeLine[0] == "G")
                                {
                                    playerSeason.Goals++;
                                }
                                if (actTimeLine[0] == "A")
                                {
                                    playerSeason.Assists++;
                                }
                            }
                        }
                    }

                    db.Entry(player).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }    
        
        private PlayerSeason CreatePlayerSeason(Match match, string player)
        {
            PlayerSeason playerSeason = new PlayerSeason();
            Player playerChange = new Player();
            TeamSeason teamSeasonChange = new TeamSeason();

            var search = from c in db.Players
                         where c.Name + " " + c.Surname == player
                         select c;
                        
            if (search.Count() > 0)
            {
                playerChange = search.First();
            }

            var searchTS = from c in db.TeamSeasons
                           where c.Season == match.Season &&
                           c.Team.Country == match.Country &&                           
                           c.NameSport == match.NameSport &&
                           (c.Team.Name == match.HomeTeam || c.Team.Name == match.AwayTeam)
                           select c;

            if (searchTS.Count() > 0)
            {
                teamSeasonChange = searchTS.First();
            }

            if (teamSeasonChange != null && playerChange != null)
            {
                playerSeason.TeamSeasonId = teamSeasonChange.TeamSeasonId;
                playerSeason.TeamSeason = teamSeasonChange;
                playerSeason.Player = playerChange;
                playerSeason.PlayerId = playerChange.PlayerId;

                playerSeason.Assists = 0;
                playerSeason.GamedMatches = 0;
                playerSeason.Goals = 0;
                playerSeason.Matches.Add(match);
                playerSeason.Season = match.Season;
                playerSeason.Tournament = match.Tournament;

                db.PlayerSeasons.Add(playerSeason);
                db.SaveChanges();

                playerChange.PlayerSeasons.Add(playerSeason);
                db.Entry(playerSeason).State = EntityState.Modified;
                
                teamSeasonChange.PlayerSeasons.Add(playerSeason);
                db.Entry(teamSeasonChange).State = EntityState.Modified;
                db.SaveChanges();
            }

            return playerSeason;
        }

        private TeamSeason CreateTeamSeason(Match match, string NameTeam)
        {
            TeamSeason teamSeason = new TeamSeason();
            string nameTeam = "";
            if (NameTeam == "home")
            {
                nameTeam = match.HomeTeam;
            }
            else
            {
                nameTeam = match.AwayTeam;
            }

            var search = from c in db.Teams
                         where c.Country == match.Country &&
                         c.NameSport == match.NameSport &&
                         c.Name == nameTeam
                         select c;

            var searchFS = from c in db.FederationSeasons
                           where c.Country == match.Country &&
                           c.NameSport == match.NameSport &&
                           c.Season == match.Season &&
                           c.Tournament == match.Tournament
                           select c;

            if (search.Count() > 0 && searchFS.Count() > 0)
            {
                Team team = search.First();
                FederationSeason fedSeason = searchFS.First();

                teamSeason.Draw = 0;
                teamSeason.GoalAgainst = 0;
                teamSeason.Goals = 0;
                teamSeason.HomeDraw = 0;
                teamSeason.HomeGoalAgainst = 0;
                teamSeason.HomeGoals = 0;
                teamSeason.HomeLose = 0;
                teamSeason.HomePoint = 0;
                teamSeason.HomeWin = 0;
                teamSeason.Lose = 0;
                teamSeason.Matches.Add(match);
                teamSeason.NameSport = match.NameSport;
                teamSeason.Point = 0;
                teamSeason.Season = match.Season;
                teamSeason.Team = team;
                teamSeason.TeamId = team.TeamId;
                teamSeason.Tournament = match.Tournament;
                teamSeason.Win = 0;
                teamSeason.FederationSeasonId = fedSeason.FederationSeasonId;
                teamSeason.FederationSeason = fedSeason;

                db.TeamSeasons.Add(teamSeason);
                db.SaveChanges();

                team.TeamSeasons.Add(teamSeason);
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();

                fedSeason.TeamSeasons.Add(teamSeason);
                db.Entry(fedSeason).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                return null;
            }

            return teamSeason;
        }

        private void CreateFederationSeason(Match match)
        {
            SportFederation spFed = new SportFederation();
            var searchSF = from c in db.SportFederation
                           where c.NameSport == match.NameSport &&
                                   c.Country == match.Country
                           select c;

            if (searchSF.Count() > 0)
            {
                FederationSeason fedSeason = new FederationSeason();
                spFed = searchSF.First();

                fedSeason.NameSport = match.NameSport;
                fedSeason.Country = match.Country;
                fedSeason.NameTournament = match.Tournament.ToString();
                fedSeason.Season = match.Season;
                fedSeason.SportFederation = spFed;
                fedSeason.SportFederationId = spFed.SportFederationId;
                fedSeason.Tournament = match.Tournament;
                fedSeason.Matches = new List<Match>();
                fedSeason.Matches.Add(match);

                db.FederationSeasons.Add(fedSeason);
                db.SaveChanges();

                spFed.FederationSeasons.Add(fedSeason);
                db.Entry(spFed).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}