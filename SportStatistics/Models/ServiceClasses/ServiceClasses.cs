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

        public void Add(List<HttpPostedFileBase> filesSF, List<HttpPostedFileBase> filesP,
            List<HttpPostedFileBase> filesT, List<HttpPostedFileBase> filesFS, List<HttpPostedFileBase> filesTS,
            List<HttpPostedFileBase> filesPS, List<HttpPostedFileBase> filesM)
        {
            string result = "";

            if (filesSF[0] != null)
            {
                for (int i = 0; i < filesSF.Count(); i++)
                {
                    result = new StreamReader(filesSF[i].InputStream).ReadToEnd();
                    AddSportFederation(result);
                }
            }
            if (filesP[0] != null)
            {
                for (int i = 0; i < filesP.Count(); i++)
                {
                    result = new StreamReader(filesP[i].InputStream).ReadToEnd();
                    AddPlayer(result);
                }
            }
            if (filesT[0] != null)
            {
                for (int i = 0; i < filesT.Count(); i++)
                {
                    result = new StreamReader(filesT[i].InputStream).ReadToEnd();
                    AddTeam(result);
                }
            }
            if (filesFS[0] != null)
            {
                for (int i = 0; i < filesFS.Count(); i++)
                {
                    result = new StreamReader(filesFS[i].InputStream).ReadToEnd();
                    AddFederationSeason(result);
                }
            }
            if (filesTS[0] != null)
            {
                for (int i = 0; i < filesTS.Count(); i++)
                {
                    result = new StreamReader(filesTS[i].InputStream).ReadToEnd();
                    AddTeamSeason(result);
                }
            }
            if (filesPS[0] != null)
            {
                for (int i = 0; i < filesPS.Count(); i++)
                {
                    result = new StreamReader(filesPS[i].InputStream).ReadToEnd();
                    AddPlayerSeason(result);
                }
            }
            if (filesM[0] != null)
            {
                for (int i = 0; i < filesM.Count(); i++)
                {
                    result = new StreamReader(filesM[i].InputStream).ReadToEnd();
                    AddMatch(result);
                }
            }
        }

        public void AddTeamSeason(string data)
        {
            data = data.Replace("\n", "&");
            data = data.Replace("\r", "");
            List<string> dataList = data.Split('&').ToList();

            foreach (string item in dataList)
            {
                TeamSeason teamSeason = new TeamSeason();
                string[] list = item.Split(',');
                if (list.Count() < 15)
                {
                    continue;
                }

                string nameSport = list[0];
                string name = list[4];
                var search = from c in db.Teams
                             where
                             c.NameSportString == nameSport &&
                             c.Name == name
                             select c;

                Team team = new Team();
                if (search.Count() < 1)
                {
                    continue;
                }
                else
                {
                    team = search.First();
                }

                string season = Enum.Format(typeof(Season), Convert.ToInt32(list[3]), "G");
                string country = list[1];
                string tournament = list[2];

                var searchFS = from c in db.FederationSeasons
                               where                               
                               c.SportFederation.Country == country &&                               
                               c.SportFederation.NameSportString == nameSport &&
                               c.SeasonString == season &&
                               c.TournamentString == tournament
                               select c;

                FederationSeason fedSeason = new FederationSeason();
                if (searchFS.Count() > 0)
                {
                    fedSeason = searchFS.First();
                }
                else
                {
                    continue;
                }
                
                teamSeason.Draw = Convert.ToInt32(list[7]);
                teamSeason.FederationSeason = fedSeason;
                teamSeason.FederationSeasonId = fedSeason.FederationSeasonId;
                teamSeason.GoalAgainst = Convert.ToInt32(list[10]);
                teamSeason.Goals = Convert.ToInt32(list[9]);
                teamSeason.HomeDraw = Convert.ToInt32(list[14]);
                teamSeason.HomeGoalAgainst = Convert.ToInt32(list[17]);
                teamSeason.HomeGoals = Convert.ToInt32(list[16]);
                teamSeason.HomeLose = Convert.ToInt32(list[15]);
                teamSeason.HomePlayed = Convert.ToInt32(list[12]);
                teamSeason.HomePoint = Convert.ToInt32(list[18]);
                teamSeason.HomeWin = Convert.ToInt32(list[13]);
                teamSeason.Lose = Convert.ToInt32(list[8]);                
                teamSeason.NameTeam = team.Name;
                teamSeason.Played = Convert.ToInt32(list[5]);
                teamSeason.Point = Convert.ToInt32(list[11]);
                teamSeason.Season = season.ParseEnum<Season>();
                teamSeason.Team = team;
                teamSeason.TeamId = team.TeamId;                
                teamSeason.Win = Convert.ToInt32(list[6]);

                db.TeamSeasons.Add(teamSeason);
                db.SaveChanges();

                team.TeamSeasons.Add(teamSeason);
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();

                fedSeason.TeamSeasons.Add(teamSeason);
                db.Entry(fedSeason).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void AddPlayerSeason(string data)
        {
            data = data.Replace("\n", "&");
            data = data.Replace("\r", "");
            List<string> dataList = data.Split('&').ToList();

            foreach (string item in dataList)
            {
                PlayerSeason playerSeason = new PlayerSeason();
                string[] list = item.Split(',');

                if (list.Count() < 7)
                {
                    continue;
                }

                string name = list[4] + " " + list[5];
                string nameSport = list[0];
                var search = from c in db.Players
                             where
                             c.Name + " " + c.Surname == name &&
                             c.NameSportString == nameSport
                             select c;

                Player player = new Player();
                if (search.Count() < 1)
                {
                    continue;
                }
                else
                {
                    player = search.First();
                }

                string season = Enum.Format(typeof(Season), Convert.ToInt32(list[2]), "G");
                string tournament = list[1];
                string nameTeam = list[3];

                var searchTeamSeason = from c in db.TeamSeasons
                                       where                                       
                                       c.FederationSeason.SportFederation.NameSportString == nameSport &&                                       
                                       c.FederationSeason.TournamentString == tournament &&
                                       c.SeasonString == season &&
                                       c.Team.Name == nameTeam
                                       select c;

                TeamSeason teamSeason = new TeamSeason();
                if (searchTeamSeason.Count() < 1)
                {
                }
                else
                {
                    teamSeason = searchTeamSeason.First();
                }

                playerSeason.Assists = Convert.ToInt32(list[8]);
                playerSeason.GamedMatches = Convert.ToInt32(list[6]);
                playerSeason.Goals = Convert.ToInt32(list[7]);                
                playerSeason.Player = player;
                playerSeason.PlayerId = player.PlayerId;
                playerSeason.Season = season.ParseEnum<Season>();
                playerSeason.TeamSeason = teamSeason;
                playerSeason.TeamSeasonId = teamSeason.TeamSeasonId;                

                db.PlayerSeasons.Add(playerSeason);
                db.SaveChanges();

                teamSeason.PlayerSeasons.Add(playerSeason);
                db.Entry(teamSeason).State = EntityState.Modified;
                db.SaveChanges();

                player.PlayerSeasons.Add(playerSeason);
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void AddSportFederation(string data)
        {
            data = data.Replace("\n", "&");
            data = data.Replace("\r", "");
            List<string> dataList = data.Split('&').ToList();

            foreach (string item in dataList)
            {
                SportFederation sportFederation = new SportFederation();
                string[] list = item.Split(',');
                if (list.Count() < 2)
                {
                    continue;
                }
                string nameSport = list[0];
                string country = list[1];
                var search = from c in db.SportFederations
                             where 
                             c.NameSportString == nameSport && c.Country == country
                             select c;
                
                if (search.Count() == 0)
                {
                    sportFederation.NameSport = list[0].ParseEnum<NameSport>();
                    sportFederation.Country = list[1];
                    sportFederation.FoundationDate = list[2];
                    sportFederation.NamePresident = list[3];
                                        
                    Sport sport = new Sport();
                    var searchS = from c in db.Sports
                                  where c.NameSportString == nameSport
                                   select c;
                    if(searchS.Count() > 0)
                    {
                        sport = searchS.First();
                    }
                    else
                    {
                        continue;
                    }

                    sportFederation.SportId = sport.SportId;
                    sportFederation.Sport = sport;

                    db.SportFederations.Add(sportFederation);
                    db.SaveChanges();
                }
            }            
        }

        public void AddFederationSeason(string data)
        {
            data = data.Replace("\n", "&");
            data = data.Replace("\r", "");
            List<string> dataList = data.Split('&').ToList();

            foreach (string item in dataList)
            {
                FederationSeason federationSeason = new FederationSeason();
                string[] list = item.Split(',');
                if (list.Count() < 5)
                {
                    continue;
                }
                string nameSport = list[0];
                string country = list[1];
                string season = Enum.Format(typeof(Season), Convert.ToInt32(list[4]), "G");
                                
                var search = from c in db.FederationSeasons
                             where                             
                             c.SportFederation.NameSportString == nameSport &&                             
                             c.SportFederation.Country == country &&
                             c.SeasonString == season
                             select c;

                if (search.Count() == 0)
                {                    
                    federationSeason.Tournament = list[2].ParseEnum<Tournament>();
                    federationSeason.NameTournament = list[3];
                    federationSeason.Season = list[4].ParseEnum<Season>();

                    string countryF = list[1];
                    string nameSportF = list[0];
                    SportFederation sportFed = new SportFederation();
                    var searchSF = from c in db.SportFederations
                                   where c.Country == countryF &&
                                   c.NameSportString == nameSportF
                                   select c;

                    if (searchSF.Count() > 0)
                    {
                        sportFed = searchSF.First();
                    }
                    else
                    {
                        continue;
                    }
                    federationSeason.SportFederationId = sportFed.SportFederationId;
                    federationSeason.SportFederation = sportFed;

                    db.FederationSeasons.Add(federationSeason);
                    db.SaveChanges();
                }
            }            
        }

        public void AddPlayer(string data)
        {
            data = data.Replace("\n", "&");
            data = data.Replace("\r", "");
            List<string> dataList = data.Split('&').ToList();

            foreach (string item in dataList)
            {
                Player player = new Player();
                string[] list = item.Split(',');
                if (list.Count() < 5)
                {
                    continue;
                }
                string nameSport = list[0];
                string name = list[1];
                string surname = list[2];
                int age = Convert.ToInt32(list[4]);
                var search = from c in db.Players
                             where
                             c.NameSportString == nameSport &&
                             c.Name == name &&
                             c.Surname == surname &&
                             c.Age == age
                             select c;

                if (search.Count() == 0)
                {
                    player.NameSport = list[0].ParseEnum<NameSport>();
                    player.Name = list[1];
                    player.Surname = list[2];
                    if (list.Count() > 7)
                    { 
                        player.Birthday = list[7];
                    }
                    if (list.Count() > 8)
                    {
                        player.Nationality = list[8];
                    }
                    player.Age = Convert.ToInt32(list[4]);                    
                    player.Weight = Convert.ToInt32(list[5]);
                    player.Height = Convert.ToInt32(list[6]);
                    player.Position = list[3].ParseEnum<Position>();

                    db.Players.Add(player);
                    db.SaveChanges();
                }
            }            
        }

        public void AddTeam(string data)
        {
            data = data.Replace("\n", "&");
            data = data.Replace("\r", "");
            List<string> dataList = data.Split('&').ToList();

            foreach (string item in dataList)
            {
                Team team = new Team();
                string[] list = item.Split(',');
                if (list.Count() < 4)
                {
                    continue;
                }
                string nameSport = list[0];
                string name = list[1];
                string country = list[2];
                var search = from c in db.Teams
                             where
                             c.NameSportString == nameSport &&
                             c.Name == name &&
                             c.Country == country
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
            data = data.Replace("\n", "&");
            data = data.Replace("\r", "");
            List<string> dataList = data.Split('&').ToList();

            foreach (string item in dataList)
            {
                if (item == null || item == "")
                {
                    return;
                }
                string[] itemList = item.Split('|');

                Match match = new Match();
                string[] list = itemList[0].Split(',');

                string nameSportM = list[0];
                string homeTeamM = list[5];
                string awayTeamM = list[6];
                string dateM = list[4];
                var search = from c in db.Matches
                             where c.NameSportString == nameSportM &&
                             c.HomeTeam == homeTeamM &&
                             c.AwayTeam == awayTeamM &&
                             c.Date == dateM
                             select c;

                if (search.Count() > 0)
                {
                    continue;
                }         

                match.NameSport = list[0].ParseEnum<NameSport>();
                match.Country = list[1];                
                string season = Enum.Format(typeof(Season), Convert.ToInt32(list[3]), "G");
                match.Season = season.ParseEnum<Season>();
                match.Tournament = list[2].ParseEnum<Tournament>();
                match.Date = list[4];                

                match.HomeTeam = list[5];
                match.AwayTeam = list[6];
                match.HomeTeamGoal = Convert.ToInt32(list[7]);
                match.AwayTeamGoal = Convert.ToInt32(list[8]);

                if (list[9] == "A")
                {
                    match.HomeTeamResult = Result.Lose;
                    match.AwayTeamResult = Result.Win;
                }
                else if (list[9] == "D")
                {
                    match.HomeTeamResult = Result.Draw;
                    match.AwayTeamResult = Result.Draw;
                }
                else if (list[9] == "H")
                {
                    match.HomeTeamResult = Result.Win;
                    match.AwayTeamResult = Result.Lose;
                }

                if (list.Count() > 10)
                {
                    match.Tour = list[10];
                }

                List<string> player = new List<string>();

                match.ListAwayPlayers = new List<string>();
                match.ListHomePlayers = new List<string>();
                match.ListTimeLineHome = new List<string>();
                match.ListTimeLineAway = new List<string>();
                if (itemList.Count() > 1)
                {
                    player = itemList[1].Split(',').ToList();                    
                    match.ListHomePlayers.AddRange(player);
                }
                if (itemList.Count() > 2)
                {
                    player = itemList[2].Split(',').ToList();                    
                    match.ListAwayPlayers.AddRange(player);
                }

                if (itemList.Count() == 5)
                {                    
                    match.ListTimeLineHome = itemList[3].Split(',').ToList();                    
                    match.ListTimeLineAway = itemList[4].Split(',').ToList();
                }

                // FederationSeason

                string countryFS = match.Country;
                
                FederationSeason fedSeason = new FederationSeason();
                var searchFS = from c in db.FederationSeasons
                               where 
                               c.SportFederation.Country == countryFS &&
                               c.SportFederation.NameSportString == nameSportM &&
                               c.SeasonString == season 
                               select c;

                if (searchFS.Count() > 0)
                {                    
                }
                else
                {
                    CreateFederationSeason(match);
                }
                
                // Team Home

                TeamSeason homeTeam = new TeamSeason();

                string countryH = match.Country;
                string homeTeamH = match.HomeTeam;
                string seasonH = match.Season.ToString();
                string nameSportH = match.NameSport.ToString();
                var searchHT = from c in db.TeamSeasons                               
                               where
                               c.FederationSeason.SportFederation.NameSportString == nameSportH &&
                               c.FederationSeason.SportFederation.Country == countryH &&
                               c.Team.Name == homeTeamH &&
                               c.SeasonString == season
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

                homeTeam.Played++;
                homeTeam.HomePlayed++;
                homeTeam.Goals += match.HomeTeamGoal;
                homeTeam.HomeGoals += match.HomeTeamGoal;
                
                homeTeam.GoalAgainst += match.AwayTeamGoal;
                homeTeam.HomeGoalAgainst += match.AwayTeamGoal;
                                
                db.Entry(homeTeam).State = EntityState.Modified;
                db.SaveChanges();

                match.TeamSeasons = new List<TeamSeason>();
                match.TeamSeasons.Add(homeTeam);                

                // Team Away

                
                TeamSeason awayTeam = new TeamSeason();
                string teamAway = match.AwayTeam;
                string countryA = match.Country;
                var searchAT = from c in db.TeamSeasons
                               where
                               c.FederationSeason.SportFederation.NameSportString == nameSportM &&
                               c.FederationSeason.SportFederation.Country == countryA &&
                               c.Team.Name == teamAway &&
                               c.SeasonString == season
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
                }

                awayTeam.Played++;                
                awayTeam.Goals += match.AwayTeamGoal;                
                
                awayTeam.GoalAgainst += match.HomeTeamGoal;
                
                db.Entry(awayTeam).State = EntityState.Modified;
                db.SaveChanges();

                match.TeamSeasons.Add(awayTeam);                                        
                
                db.Matches.Add(match);
                db.SaveChanges();

                if (searchFS.Count() > 0)
                {
                    fedSeason = searchFS.First();
                    fedSeason.Matches.Add(match);

                    db.Entry(fedSeason).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
                // PlayerSeason

                if (match.ListHomePlayers != null)
                {
                    foreach (string homeplayer in match.ListHomePlayers)
                    {
                        PlayerSeason playerSeason = new PlayerSeason();
                        string playerName = homeplayer;
                        string nameSport = match.NameSport.ToString();

                        var searchPlayer = from c in db.Players
                                           where
                                           (c.Name + " " + c.Surname == playerName ||
                                           c.Name + " " + c.Surname == playerName + " ") &&
                                           c.NameSportString == nameSport 
                                           select c;

                        if (searchPlayer.Count() == 0)
                        {
                            continue;
                        }

                        string seasonPSH = match.Season.ToString();
                        string teamH = homeTeam.NameTeam;
                        var searchPSH = from c in db.PlayerSeasons
                                        where 
                                        c.TeamSeason.NameTeam == teamH &&
                                        (c.Player.Name + " " + c.Player.Surname == playerName ||
                                        c.Player.Name + " " + c.Player.Surname == playerName + " ") &&
                                        c.Player.NameSportString == nameSport &&
                                        c.TeamSeason.SeasonString == seasonPSH
                                        select c;

                        if (searchPSH.Count() > 0)
                        {
                            playerSeason = searchPSH.First();
                        }
                        else
                        {                            
                            playerSeason = CreatePlayerSeason(match, searchPlayer.First(), homeTeam);
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

                        db.Entry(playerSeason).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }


                if (match.ListAwayPlayers != null)
                {
                    foreach (string awayplayer in match.ListAwayPlayers)
                    {
                        PlayerSeason playerSeason = new PlayerSeason();
                        string playerName = awayplayer;
                        string nameSport = match.NameSport.ToString();

                        var searchPlayer = from c in db.Players
                                           where
                                           (c.Name + " " + c.Surname == playerName ||
                                           c.Name + " " + c.Surname == playerName + " ") &&
                                           c.NameSportString == nameSport
                                           select c;

                        if (searchPlayer.Count() == 0)
                        {
                            continue;
                        }

                        string seasonPSH = match.Season.ToString();
                        string teamA = homeTeam.NameTeam;
                        var searchPSA = from c in db.PlayerSeasons
                                        where
                                        c.TeamSeason.NameTeam == teamA &&
                                        (c.Player.Name + " " + c.Player.Surname == playerName ||
                                        c.Player.Name + " " + c.Player.Surname == playerName + " ") &&
                                        c.Player.NameSportString == nameSport &&
                                        c.TeamSeason.SeasonString == seasonPSH
                                        select c;

                        if (searchPSA.Count() > 0)
                        {
                            playerSeason = searchPSA.First();
                        }
                        else
                        {
                            playerSeason = CreatePlayerSeason(match, searchPlayer.First(), awayTeam);
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

                        db.Entry(playerSeason).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
        }    
        
        private PlayerSeason CreatePlayerSeason(Match match, Player player, TeamSeason teamSeason)
        {
            PlayerSeason playerSeason = new PlayerSeason();
                        
            if (player != null && teamSeason != null)
            {
                playerSeason.TeamSeasonId = teamSeason.TeamSeasonId;
                playerSeason.TeamSeason = teamSeason;
                playerSeason.Player = player;
                playerSeason.PlayerId = player.PlayerId;

                playerSeason.Assists = 0;
                playerSeason.GamedMatches = 0;
                playerSeason.Goals = 0;
                playerSeason.Matches = new List<Match>();            
                playerSeason.Season = match.Season;                

                db.PlayerSeasons.Add(playerSeason);
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

            string nameSport = match.NameSport.ToString();
            string country = match.Country;
            string name = nameTeam;
            var search = from c in db.Teams
                         where c.Country == country &&
                         c.NameSportString == nameSport &&
                         c.Name == name
                         select c;

            string season = match.Season.ToString();
            string tournament = match.Tournament.ToString();
            var searchFS = from c in db.FederationSeasons
                           where 
                           c.SportFederation.Country == country &&
                           c.SportFederation.NameSportString == nameSport &&
                           c.SeasonString == season &&
                           c.TournamentString == tournament
                           select c;

            if (search.Count() > 0 && searchFS.Count() > 0)
            {
                Team team = search.First();
                FederationSeason fedSeason = searchFS.First();

                teamSeason.NameTeam = nameTeam;
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
                teamSeason.Point = 0;
                teamSeason.Season = match.Season;                                
                teamSeason.Win = 0;                
                teamSeason.FederationSeasonId = fedSeason.FederationSeasonId;
                teamSeason.FederationSeason = fedSeason;
                teamSeason.Team = team;
                teamSeason.TeamId = team.TeamId;

                db.TeamSeasons.Add(teamSeason);
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

            string country = match.Country;
            string nameSport = match.NameSport.ToString();
            var searchSF = from c in db.SportFederations
                           where c.NameSportString == nameSport &&
                                   c.Country == country
                           select c;

            if (searchSF.Count() > 0)
            {
                FederationSeason fedSeason = new FederationSeason();
                spFed = searchSF.First();
                                
                fedSeason.NameTournament = LeagueName.Name(match.Country + match.NameSport.ToString() + match.Tournament.ToString());                
                fedSeason.Season = match.Season;
                fedSeason.SportFederation = spFed;
                fedSeason.SportFederationId = spFed.SportFederationId;
                fedSeason.Tournament = match.Tournament;
                fedSeason.Matches = new List<Match>();
                
                db.FederationSeasons.Add(fedSeason);
                db.SaveChanges();                
            }
        }
    }
}