using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SportStatistics.Models.ViewModels;

namespace SportStatistics.Models.ServiceClasses
{
    public class ServiceDatabase
    {
        private DatabaseContext db = new DatabaseContext();
        private Season currentSeason = Season._2018_2019;

        public List<Standings> CreateModelStandings(string sport, string federation, string tournament, int? fedSeason, bool group = false)
        {
            List<Standings> list = new List<Standings>();
            List<FederationSeason> search = new List<FederationSeason>();

            if (fedSeason != null && tournament != null)
            {
                search = (from c in db.FederationSeasons
                          where c.FederationSeasonId == fedSeason
                          select c).ToList();

                string season = Enum.Format(typeof(Season), Convert.ToInt32(tournament), "G");
                string tourn = search[0].TournamentString;
                string country = search[0].SportFederation.Country;
                string nameSport = search[0].SportFederation.NameSportString;
                search = (from c in db.FederationSeasons
                          where 
                          c.SeasonString == season &&
                          c.TournamentString == tourn &&
                          c.SportFederation.Country == country &&
                          c.SportFederation.NameSportString == nameSport
                          select c).ToList();
            }
            else
            {
                if (fedSeason == null)
                {
                    string season = currentSeason.ToString();
                    search = (from c in db.FederationSeasons
                              where
                              c.SeasonString == season &&
                              c.TournamentString == tournament &&
                              c.SportFederation.Country == federation &&
                              c.SportFederation.Sport.NameSportString == sport
                              select c).ToList();
                }
                else
                {
                    search = (from c in db.FederationSeasons
                              where c.FederationSeasonId == fedSeason
                              select c).ToList();
                }
            }

            if (search.Count() > 0)
            {
                foreach (var item in search.First().TeamSeasons)
                {
                    Standings standings = new Standings();
                    standings.Form = new List<Form>();                    
                    standings.TeamId = item.TeamId;
                    standings.FederationSeasonId = item.FederationSeasonId;
                    standings.Season = item.Season;
                    standings.NameTeam = item.Team.Name;
                    standings.GamedMatches = item.Played;
                    standings.Win = item.Win;
                    standings.Draw = item.Draw;
                    standings.Lose = item.Lose;
                    standings.Goals = item.Goals;                    
                    standings.GoalAgainst = item.GoalAgainst;
                    standings.Point = item.Point;
                    List<Match> matches = item.Matches.ToList();

                    if (!group)
                    {
                        matches.Reverse();
                    }
                    foreach(var item2 in matches)
                    {
                        Form form = new Form();
                        form.MatchId = item2.MatchId;
                        if (item2.HomeTeam == item.Team.Name)
                        {
                            form.WinDL = item2.HomeTeamResult;
                        }
                        else
                        {
                            form.WinDL = item2.AwayTeamResult;
                        }
                        form.Result = item2.HomeTeam + " " + item2.HomeTeamGoal + "-" + item2.AwayTeamGoal + " " + item2.AwayTeam;
                        standings.Form.Add(form);
                        if (standings.Form.Count() == 6)
                        {
                            break;
                        }
                    }
                    list.Add(standings);
                }
            }

            return list;
        }       

        public List<Standings> CreateModelStandingsHome(int fedSeason)
        {
            List<Standings> list = new List<Standings>();
            var search  = from c in db.FederationSeasons
                          where
                          c.FederationSeasonId == fedSeason
                          select c;

            if (search.Count() > 0)
            {
                foreach (var item in search.First().TeamSeasons)
                {
                    Standings standings = new Standings();
                    standings.Form = new List<Form>();
                    
                    standings.TeamId = item.TeamId;
                    standings.FederationSeasonId = item.FederationSeasonId;
                    standings.Season = item.Season;
                    standings.NameTeam = item.Team.Name;
                    standings.GamedMatches = item.HomeWin + item.HomeDraw + item.HomeLose;
                    standings.Win = item.HomeWin;
                    standings.Draw = item.HomeDraw;
                    standings.Lose = item.HomeLose;
                    standings.Goals = item.HomeGoals;
                    standings.GoalAgainst = item.HomeGoalAgainst;
                    standings.Point = item.HomePoint;
                    List<Match> matches = item.Matches.ToList();
                    matches.Reverse();
                    foreach (var item2 in matches)
                    {
                        Form form = new Form();
                        if (item2.HomeTeam == item.Team.Name)
                        {
                            form.MatchId = item2.MatchId;
                            form.WinDL = item2.HomeTeamResult;                            
                            form.Result = item2.HomeTeam + " " + item2.HomeTeamGoal + "-" + item2.AwayTeamGoal + " " + item2.AwayTeam;
                            standings.Form.Add(form);
                            if (standings.Form.Count() == 6)
                            {
                                break;
                            }
                        }
                    }
                    list.Add(standings);
                }
            }

            return list;
        }
        
        public List<Standings> CreateModelStandingsAway(int fedSeason)
        {
            List<Standings> list = new List<Standings>();
            var search  = from c in db.FederationSeasons
                          where c.FederationSeasonId == fedSeason                          
                          select c;

            if (search.Count() > 0)
            {
                foreach (var item in search.First().TeamSeasons)
                {
                    Standings standings = new Standings();
                    standings.Form = new List<Form>();
                    
                    standings.TeamId = item.TeamId;
                    standings.FederationSeasonId = item.FederationSeasonId;
                    standings.Season = item.Season;
                    standings.NameTeam = item.Team.Name;
                    standings.GamedMatches = item.Matches.Count() - (item.HomeWin + item.HomeDraw + item.HomeLose);
                    standings.Win = item.Win - item.HomeWin;
                    standings.Draw = item.Draw - item.HomeDraw;
                    standings.Lose = item.Lose - item.HomeLose;
                    standings.Goals = item.Goals- item.HomeGoals;
                    standings.GoalAgainst = item.GoalAgainst - item.HomeGoalAgainst;
                    standings.Point = item.Point - item.HomePoint;
                    List<Match> matches = item.Matches.ToList();
                    matches.Reverse();
                    foreach (var item2 in matches)
                    {
                        Form form = new Form();
                        if (item2.AwayTeam == item.Team.Name)
                        {
                            form.MatchId = item2.MatchId;
                            form.WinDL = item2.AwayTeamResult;
                            form.Result = item2.HomeTeam + " " + item2.HomeTeamGoal + "-" + item2.AwayTeamGoal + " " + item2.AwayTeam;
                            standings.Form.Add(form);
                            if (standings.Form.Count() == 6)
                            {
                                break;
                            }
                        }
                    }
                    list.Add(standings);
                }
            }

            return list;
        }

        public int Find(string sport, string fed, string tour)
        {
            int fedId = 0;
            string season = currentSeason.ToString();
            var search = from c in db.FederationSeasons
                         where
                         c.SportFederation.NameSportString == sport &&
                         c.SportFederation.Country == fed &&
                         c.TournamentString == tour &&
                         c.SeasonString == season
                         select c;

            if (search.Count() > 0)
            {
                fedId = search.First().FederationSeasonId;
            }
            return fedId;
        }

        private FederationSeason FindFed(int fedSeason, string season)
        {
            List<FederationSeason> search = new List<FederationSeason>();
            if (season == null)
            {
                search = (from c in db.FederationSeasons
                          where c.FederationSeasonId == fedSeason
                          select c).ToList();
            }
            else
            {
                search = (from c in db.FederationSeasons
                          where c.FederationSeasonId == fedSeason
                          select c).ToList();

                string seasonS = Enum.Format(typeof(Season), Convert.ToInt32(season), "G");
                string tourn = search[0].TournamentString;
                string country = search[0].SportFederation.Country;
                string nameSport = search[0].SportFederation.NameSportString;
                search = (from c in db.FederationSeasons
                          where
                          c.SeasonString == seasonS &&
                          c.TournamentString == tourn &&
                          c.SportFederation.Country == country &&
                          c.SportFederation.NameSportString == nameSport
                          select c).ToList();
            }
            return search.First();
        }

        public List<List<Standings>> CreateModelStandingsGroups(int fedSeason, ref List<List<Score>> score, string season, ref List<string> nameGroup)
        {
            FederationSeason fed = FindFed(fedSeason, season);

            List<string> toursName = new List<string>();
            List<string> group = new List<string>();
            List<Match> matches = fed.Matches.ToList();
            foreach(var item in matches)
            {
                string all = string.Join(",", toursName);   
                if (all.IndexOf(item.Tour) < 0)
                {
                    toursName.Add(item.Tour);
                    if (item.Tour.Substring(0, 5) == "Group")
                    {
                        group.Add(item.Tour);
                    }
                }
            }

            group.Sort();
            List<Standings> standings = CreateModelStandings(null, null, season, fedSeason, true);
            List<List<Standings>> listStandings = new List<List<Standings>>();
            List<List<string>> teamInGropup = new List<List<string>>();
            for (int i = 0; i < group.Count(); i++)
            {
                List<string> temp = new List<string>();
                foreach (var item in matches)
                {
                    if (item.Tour == group[i])
                    {
                        string all = string.Join(",", temp);
                        if (all.IndexOf(item.HomeTeam) < 0)
                        {
                            temp.Add(item.HomeTeam);
                        }
                    }
                }
                teamInGropup.Add(temp);
            }
            
            foreach(var item in teamInGropup)
            {
                List<Standings> temp = new List<Standings>();
                foreach(var item2 in item)
                {
                    for (int i = 0; i < standings.Count(); i++)
                    {
                        if (item2 == standings[i].NameTeam)
                        {
                            standings[i].Form.Reverse();
                            temp.Add(standings[i]);
                            break;
                        }
                    }
                }
                listStandings.Add(temp);
            }
            

            int count = toursName.Count() - group.Count();

            for (int i = 0; i < count; i++)
            {
                group.Add(TourName(i, count));
            }

            for (int i = 0; i < count; i++)
            {
                List<Score> temp = new List<Score>();

                foreach(var item in matches)
                {
                    if (item.Tour == TourName(i, count))
                    {                        
                        Score tempS = new Score();                        
                        tempS.AwayTeam = item.AwayTeam;

                        string name = item.AwayTeam;
                        string sport = fed.SportFederation.NameSportString;
                        string country = fed.SportFederation.Country;
                        var tempId = from c in db.Teams
                                     where
                                     c.Name == name &&
                                     (c.Country == country || country == "UEFA") &&
                                     c.NameSportString == sport
                                     select c;

                        tempS.AwayTeamId = tempId.First().TeamId;

                        tempS.HomeTeam = item.HomeTeam;

                        string nameH = item.HomeTeam;
                        string sportH = fed.SportFederation.NameSportString;
                        string countryH = fed.SportFederation.Country;
                        var tempIdH = from c in db.Teams
                                      where
                                     c.Name == nameH &&
                                     (c.Country == countryH || countryH == "UEFA") &&
                                     c.NameSportString == sportH
                                      select c;

                        tempS.HomeTeamId = tempIdH.First().TeamId;

                        tempS.FederationSeasonId = fed.FederationSeasonId;
                        tempS.Date = item.Date;
                        tempS.MatchId = item.MatchId;
                        tempS.Season = item.Season;
                        tempS.Result = item.HomeTeamGoal + " - " + item.AwayTeamGoal;

                        temp.Add(tempS);
                    }
                }
                score.Add(temp);
            }
            

            nameGroup = group;
            return listStandings;
        }

        private string TourName(int i, int count)
        {
            count = 6 - count;
            i = i + count;
            string index = Convert.ToString(i);
            switch (index)
            {
                case "0":
                    {
                        return "Round of 64";
                    }
                case "1":
                    {
                        return "Round of 32";
                    }
                case "2":
                    {
                        return "Round of 16";
                    }
                case "3":
                    {
                        return "Quarter-finals";
                    }
                case "4":
                    {
                        return "Semi-finals";
                    }
                case "5":
                    {
                        return "Final";
                    }
            }

            return null;
        }

        public ShowTeam CreateModelsShowTeam(int TeamId, int fedSeason, string Season)
        {
            FederationSeason fed = FindFed(fedSeason, Season);
            if (Season == null)
            {
                Season = fed.SeasonString;
            }

            ShowTeam show = new ShowTeam();
            show.Players = new List<TeamPlayer>();
            show.Statistics = new List<TeamStatistics>();
                       
            var search = from c in db.Teams
                         where c.TeamId == TeamId
                         select c;

            if (search.Count() > 0)
            {
                show.Team = search.First();
            }
            else
            {
                return null;
            }

            if (Season.Count() < 6)
            {
                Season = Enum.Format(typeof(Season), Convert.ToInt32(Season), "G");
            }

            var season = from c in db.TeamSeasons
                         where c.TeamId == TeamId && c.SeasonString == Season
                         select c;
            
            if (season.Count() > 0)
            {
                foreach (var item in season)
                {
                    TeamStatistics stat = new TeamStatistics();
                    stat.Federation = item.FederationSeason.SportFederation.Country;
                    stat.Sport = item.FederationSeason.SportFederation.Sport.NameSport.ToString();
                    stat.FederationSeasonId = item.FederationSeasonId;
                    stat.Tournament = item.FederationSeason.NameTournament;
                    stat.Season = item.Season;
                    
                    if (stat.Tournament != "UEFA Champions League")
                    {
                        stat.Apps = item.Played;
                        stat.Win = item.Win;
                        stat.Draw = item.Draw;
                        stat.Loss = item.Lose;
                        stat.Goals = item.Goals;
                    }
                    else
                    {
                        stat.Apps = item.Matches.Count();
                        foreach(var match in item.Matches)
                        {
                            if (item.NameTeam == match.HomeTeam)
                            {
                                if (match.HomeTeamResult == Models.Result.Win)
                                {
                                    stat.Win++;
                                } 
                                else if (match.HomeTeamResult == Models.Result.Draw)
                                {
                                    stat.Draw++;
                                } 
                                else
                                {
                                    stat.Loss++;
                                }
                                stat.Goals += match.HomeTeamGoal;
                            }
                            else
                            {
                                if (match.AwayTeamResult == Models.Result.Win)
                                {
                                    stat.Win++;
                                }
                                else if (match.AwayTeamResult == Models.Result.Draw)
                                {
                                    stat.Draw++;
                                }
                                else
                                {
                                    stat.Loss++;
                                }
                                stat.Goals += match.AwayTeamGoal;
                            }
                        }
                    }
                    show.Statistics.Add(stat);                    
                }
            }

            List<TeamPlayer> listPlayer = new List<TeamPlayer>();
            foreach (var item in season)
            {
                if (item.FederationSeason.NameTournament == fed.NameTournament)
                {
                    foreach (var item2 in item.PlayerSeasons)
                    {
                        bool check = false;
                        int index = -1;
                        for (int i = 0; i < listPlayer.Count(); i++)
                        {
                            if (listPlayer[i].Name == item2.Player.Name + " " + item2.Player.Surname ||
                                listPlayer[i].Name + " " == item2.Player.Name + " " + item2.Player.Surname
                                )
                            {
                                check = true;
                                index = i;
                                break;
                            }
                        }

                        if (check)
                        {
                            listPlayer[index].Apps += item2.Matches.Count();
                            listPlayer[index].Goals += item2.Goals;
                            listPlayer[index].Assists += item2.Assists;
                        }
                        else
                        {
                            TeamPlayer player = new TeamPlayer();
                            player.PlayerId = item2.PlayerId;
                            player.Name = item2.Player.Name + " " + item2.Player.Surname;
                            player.Age = item2.Player.Age;
                            player.CM = item2.Player.Height;
                            player.KG = item2.Player.Weight;
                            player.Pos = item2.Player.Position;
                            player.Apps = item2.GamedMatches;
                            player.Goals = item2.Goals;
                            player.Assists = item2.Assists;
                            listPlayer.Add(player);
                        }
                    }
                }
            }
        
            show.Players = listPlayer;
            return show;
        }

        public List<Scorer> Scorers(int? fedSeason, string season)
        {
            List<Scorer> scorers = new List<Scorer>();
            List<FederationSeason> search = new List<FederationSeason>();
            if (season == null)
            {            
                search = (from c in db.FederationSeasons
                          where c.FederationSeasonId == fedSeason
                          select c).ToList();
            }
            else
            {
                search = (from c in db.FederationSeasons
                          where c.FederationSeasonId == fedSeason
                          select c).ToList();

                string seasonS = Enum.Format(typeof(Season), Convert.ToInt32(season), "G");
                string tourn = search[0].TournamentString;
                string country = search[0].SportFederation.Country;
                string nameSport = search[0].SportFederation.NameSportString;
                search = (from c in db.FederationSeasons
                          where
                          c.SeasonString == seasonS &&
                          c.TournamentString == tourn &&
                          c.SportFederation.Country == country &&
                          c.SportFederation.NameSportString == nameSport
                          select c).ToList();
            }

            if (search.Count() > 0)
            {
                foreach(var item in search.First().TeamSeasons)
                {
                    foreach(var player in item.PlayerSeasons)
                    {
                        if (player.Goals > 0)
                        {
                            Scorer scorer = new Scorer();
                            scorer.PlayerId = player.PlayerId;
                            scorer.FederationSeasonId = search.First().FederationSeasonId;
                            scorer.Season = player.Season;
                            scorer.Name = player.Player.Name + " " + player.Player.Surname;
                            scorer.NameTeam = item.Team.Name;
                            scorer.TeamId = item.TeamId;
                            scorer.Apps = player.GamedMatches;
                            scorer.Goals = player.Goals;
                            scorer.Assists = player.Assists;
                            scorers.Add(scorer);
                        }
                    }
                }                 
            }                        
            return scorers;
        }

        public ShowPlayer ShowPlayer(int fedSeason, int PlayerId, string Season)
        {
            ShowPlayer showPlayer = new ShowPlayer();
            showPlayer.Game = new List<PlayedGame>();
            showPlayer.AllGame = new List<Total>();

            var playerSearch = from c in db.Players
                               where c.PlayerId == PlayerId
                               select c;

            if (playerSearch.Count() < 1)
            {
                return null;
            }
            Player player = playerSearch.First();
            showPlayer.Player = player;
            showPlayer.FederationSeasonId = fedSeason;            
                        
            var search = FindFed(fedSeason, Season);
            showPlayer.Season = search.Season;

            if (search != null)
            {
                string season = search.Season.ToString();
                var playerSeason = from c in db.PlayerSeasons
                                   where c.PlayerId == PlayerId &&
                                   c.SeasonString == season
                                   select c;

                if (playerSeason.Count() > 0)
                {
                    foreach (var item in playerSeason)
                    {
                        if (item.Matches.Count() == 0 && item.TeamSeason.FederationSeason.NameTournament == search.NameTournament)
                        {
                            PlayedGame playedGame = new PlayedGame();
                            playedGame.TournamentName = item.TeamSeason.FederationSeason.NameTournament;
                            int date = (int)item.Season;
                            playedGame.Date = date + "/" + (Convert.ToInt32(date) + 1);
                            playedGame.ResultMatch = Convert.ToString(item.GamedMatches);
                            playedGame.GoalsInGame = item.Goals;
                            playedGame.AssistsInGame = item.Assists;
                            showPlayer.Game.Add(playedGame);
                        }
                        else
                        {
                            foreach (var game in item.Matches)
                            {
                                PlayedGame playedGame = new PlayedGame();
                                playedGame.TournamentName = item.TeamSeason.FederationSeason.NameTournament;
                                playedGame.Date = game.Date;
                                playedGame.MatchId = game.MatchId;
                                playedGame.ResultMatch = game.HomeTeam + " " + game.HomeTeamGoal + "-" +
                                    game.AwayTeamGoal + " " + game.AwayTeam;

                                List<string> timeLineTeam = new List<string>();
                                if (item.TeamSeason.Team.Name == game.HomeTeam)
                                {
                                    timeLineTeam = game.ListTimeLineHome;
                                }
                                else
                                {
                                    timeLineTeam = game.ListTimeLineAway;
                                }

                                if (timeLineTeam != null)
                                {
                                    foreach (var timeLine in timeLineTeam)
                                    {
                                        string[] arr = timeLine.Split(':');
                                        if (arr.Count() > 2)
                                        {
                                            if (arr[2] == (player.Name + " " + player.Surname) ||
                                                (arr[2] == player.Name && player.Surname == ""))
                                            {
                                                if (arr[0] == "G")
                                                {
                                                    playedGame.GoalsInGame++;
                                                }
                                                if (arr[0] == "A")
                                                {
                                                    playedGame.AssistsInGame++;
                                                }
                                            }
                                        }
                                    }
                                }
                                showPlayer.Game.Add(playedGame);
                            }
                        }
                    }
                }
            }

            string namePlayer = player.Name + " " + player.Surname;
            var searchPlayer = from c in db.PlayerSeasons
                               where c.Player.Name + " " + c.Player.Surname == namePlayer
                               select c;

            List<string> nameTournament = new List<string>();
            foreach (var item in searchPlayer)
            {
                string name = item.TeamSeason.FederationSeason.NameTournament;
                bool check = true;
                foreach(var find in nameTournament)
                {
                    if (find == name)
                    {
                        check = false;
                    }
                }
                
                if (check)
                {
                    nameTournament.Add(name);
                }                    
            }
            
            foreach(var tour in nameTournament)
            {
                Total total = new Total();
                total.TournamentName = tour;
                string tourS = tour;
                var allSeasonTour = from c in db.PlayerSeasons
                                  where c.PlayerId == PlayerId &&
                                  c.TeamSeason.FederationSeason.NameTournament == tourS
                                  select c;

                if (allSeasonTour.Count() > 0)
                {
                    foreach (var item in allSeasonTour)
                    {
                        total.TotalGames += item.GamedMatches;
                        total.TotalGoals += item.Goals;
                        total.TotalAssists += item.Assists;
                    }
                    if (total.TotalGames > 0)
                    {
                        showPlayer.AllGame.Add(total);
                    }
                }
            }

            return showPlayer;
        }

        public List<Scorer> ArchScorers(int fedSeason)
        {
            List<Scorer> scorers = new List<Scorer>();
            var fed = db.FederationSeasons.Find(fedSeason);

            var search = from c in db.FederationSeasons
                         where c.SportFederationId == fed.SportFederationId &&
                         c.TournamentString == fed.TournamentString
                         select c;

            foreach(var item in search)
            {
                PlayerSeason playerSeason = new PlayerSeason();
                Scorer scorer = new Scorer();
                foreach(var seasonTeam in item.TeamSeasons)
                {
                    foreach (var seasonPlayer in seasonTeam.PlayerSeasons)
                    {
                        if (seasonPlayer.Goals > playerSeason.Goals)
                        {
                            playerSeason = seasonPlayer;
                        }
                    }
                }
                if (playerSeason.TeamSeason != null)
                {
                    scorer.FederationSeasonId = playerSeason.TeamSeason.FederationSeasonId;
                    scorer.PlayerId = playerSeason.PlayerId;
                    scorer.TeamId = playerSeason.TeamSeason.TeamId;
                    scorer.Season = playerSeason.Season;
                    scorer.Name = playerSeason.Player.Name + " " + playerSeason.Player.Surname;
                    scorer.NameTeam = playerSeason.TeamSeason.Team.Name;
                    scorer.Apps = playerSeason.GamedMatches;
                    scorer.Goals = playerSeason.Goals;
                    scorer.Assists = playerSeason.Assists;
                    scorers.Add(scorer);
                }
            }

            return scorers;
        }

        public List<Winner> Winners(int fedSeason)
        {
            List<Winner> winners = new List<Winner>();

            var fed = db.FederationSeasons.Find(fedSeason);
            
            if (fed == null)
            {
                return null;
            }
                        
            var search = from c in db.FederationSeasons
                         where c.SportFederationId == fed.SportFederationId &&
                         c.TournamentString == fed.TournamentString
                         select c;

            if (search == null)
            {
                return null;
            }

            foreach(var item in search)
            {
                TeamSeason team = new TeamSeason();
                foreach (var season in item.TeamSeasons)
                {
                    if (season.Matches.Count() > 14)
                    {
                        if (season.Point > team.Point)
                        {
                            team = season;
                        }
                    }
                    else
                    {        
                        foreach (var t in season.Matches)
                        {
                            if (t.Tour == "Final")
                            {
                                if ((t.HomeTeam == season.NameTeam &&
                                    t.HomeTeamResultString == "Win") ||
                                    t.AwayTeam == season.NameTeam &&
                                    t.AwayTeamResultString == "Win")
                                {
                                    team = season;
                                }
                            }
                        }
                    }
                }
                Winner winner = new Winner();
                winner.FederationSeasonId = fedSeason;
                if (team.Team != null)
                {
                    winner.TeamId = team.TeamId;
                    winner.TeamName = team.Team.Name;
                    winner.Season = team.Season;
                    winners.Add(winner);
                }
            }
            return winners;
        }

        public List<Progress> Progresses(int fedSeason)
        {
            List<Progress> progresses = new List<Progress>();
            var fed = db.FederationSeasons.Find(fedSeason);

            if (fed == null)
            {
                return null;
            }

            var list = from c in fed.TeamSeasons
                       orderby c.Point descending
                       select c;

            foreach (var item in list)
            {
                Progress progress = new Progress();
                progress.Form = new List<Form>();

                progress.FederationSeasonId = fedSeason;
                progress.Season = item.Season;
                progress.NameTeam = item.Team.Name;
                progress.TeamId = item.TeamId;

                foreach (var item2 in item.Matches)
                {
                    progress.Count++;
                    Form form = new Form();
                    form.MatchId = item2.MatchId;
                    if (item2.HomeTeam == item.Team.Name)
                    {
                        form.WinDL = item2.HomeTeamResult;
                    }
                    else
                    {
                        form.WinDL = item2.AwayTeamResult;
                    }
                    form.Result = item2.HomeTeam + " " + item2.HomeTeamGoal + "-" + item2.AwayTeamGoal + " " + item2.AwayTeam;
                    progress.Form.Add(form);
                }
                progresses.Add(progress);
            }
            
            return progresses;
        }

        public List<Progress> ProgressesHome(int fedSeason)
        {
            List<Progress> progresses = new List<Progress>();
            var fed = db.FederationSeasons.Find(fedSeason);

            if (fed == null)
            {
                return null;
            }

            var list = from c in fed.TeamSeasons
                       orderby c.HomePoint descending
                       select c;

            foreach (var item in list)                
            {
                Progress progress = new Progress();
                progress.Form = new List<Form>();

                progress.FederationSeasonId = fedSeason;
                progress.Season = item.Season;
                progress.NameTeam = item.Team.Name;
                progress.TeamId = item.TeamId;

                foreach (var item2 in item.Matches)
                {                    
                    if (item2.HomeTeam == item.Team.Name)
                    {
                        progress.Count++;
                        Form form = new Form();
                        form.MatchId = item2.MatchId;
                        form.WinDL = item2.HomeTeamResult;
                        form.Result = item2.HomeTeam + " " + item2.HomeTeamGoal + "-" + item2.AwayTeamGoal + " " + item2.AwayTeam;
                        progress.Form.Add(form);
                    }
                }
                progresses.Add(progress);
            }
            return progresses;
        }

        public List<Progress> ProgressesAway(int fedSeason)
        {
            List<Progress> progresses = new List<Progress>();
            var fed = db.FederationSeasons.Find(fedSeason);

            if (fed == null)
            {
                return null;
            }

            var list = from c in fed.TeamSeasons
                       orderby c.Point - c.HomePoint descending
                       select c;

            foreach (var item in list)                
            {
                Progress progress = new Progress();
                progress.Form = new List<Form>();

                progress.FederationSeasonId = fedSeason;
                progress.Season = item.Season;
                progress.NameTeam = item.Team.Name;
                progress.TeamId = item.TeamId;

                foreach (var item2 in item.Matches)
                {
                    if (item2.AwayTeam == item.Team.Name)
                    {
                        progress.Count++;
                        Form form = new Form();
                        form.MatchId = item2.MatchId;
                        form.WinDL = item2.AwayTeamResult;
                        form.Result = item2.HomeTeam + " " + item2.HomeTeamGoal + "-" + item2.AwayTeamGoal + " " + item2.AwayTeam;
                        progress.Form.Add(form);
                    }
                }
                progresses.Add(progress);
            }
            return progresses;
        }

        public ShowMatch ShowMatch(int MatchId)
        {
            ShowMatch match = new ShowMatch();
            match.IdMatch = new IdMatch();
            match.IdMatch.AwayTeamPlayersId = new List<int>();
            match.IdMatch.HomeTeamPlayersId = new List<int>();
            
            Match search = db.Matches.Find(MatchId);

            if (search == null)
            {
                return null;
            }
            match.NameSportString = search.NameSportString;
            match.Country = search.Country;            
            match.Date = search.Date;
            match.HomeTeam = search.HomeTeam;
            match.HomeTeamGoal = search.HomeTeamGoal;            
            match.ListAwayPlayers = search.ListAwayPlayers;            
            match.ListHomePlayers = search.ListHomePlayers;            
            match.ListTimeLineHome = search.ListTimeLineHome;            
            match.ListTimeLineAway = search.ListTimeLineAway;
            match.AwayTeam = search.AwayTeam;
            match.AwayTeamGoal = search.AwayTeamGoal;

            string name = search.HomeTeam;
            string nameSport = search.NameSport.ToString();
            
            var team = from c in db.Teams
                       where
                           c.Name == name &&
                           c.NameSportString == nameSport                            
                       select c;
            if (team.Count() > 0)
            {
                match.NameStadium = team.First().NameStadium;
                match.City = team.First().City;
            }
            
            match.Season = search.Season;
            if (search.Tour != null)
            {
                match.Tour = search.Tour;
            }
            else
            {
                match.Tour = "";
            }
            match.Tournament = search.Tournament;
            
            if (search.NameTournament != null)
            {
                match.NameTournament = search.NameTournament;
            }
            else
            {
                match.NameTournament = search.TeamSeasons.First().FederationSeason.NameTournament;
            }                     

            if (search.TeamSeasons.First().NameTeam == match.HomeTeam)
            {
                match.IdMatch.TeamHomeId = search.TeamSeasons.First().TeamId;
                match.IdMatch.TeamAwayId = search.TeamSeasons.Last().TeamId;
            }
            else
            {
                match.IdMatch.TeamHomeId = search.TeamSeasons.Last().TeamId; 
                match.IdMatch.TeamAwayId = search.TeamSeasons.First().TeamId;
            }
            
            match.IdMatch.FederationSeasonId = search.TeamSeasons.First().FederationSeasonId;

            if (match.ListHomePlayers != null)
            {
                foreach (var item in match.ListHomePlayers)
                {
                    string nameS = item;
                    var temp = from c in db.Players
                               where c.Name + " " + c.Surname == nameS
                               select c;
                    if (temp.Count() > 0)
                    {
                        match.IdMatch.HomeTeamPlayersId.Add(temp.First().PlayerId);
                    }
                }
            }

            if (match.ListAwayPlayers != null)
            {
                foreach (var item in match.ListAwayPlayers)
                {
                    string nameS = item;
                    var temp = from c in db.Players
                               where c.Name + " " + c.Surname == nameS
                               select c;
                    if (temp.Count() > 0)
                    {
                        match.IdMatch.AwayTeamPlayersId.Add(temp.First().PlayerId);
                    }
                }
            }
            return match;
        }

        public List<Score> Result(ref int CountTour, int fedSeason, string season, int tour = 0)
        {
            List<Score> score = new List<Score>();            
            FederationSeason fed = new FederationSeason();

            if (season == null)
            {
                fed = db.FederationSeasons.Find(fedSeason);
            }
            else
            {
                var search = (from c in db.FederationSeasons
                          where c.FederationSeasonId == fedSeason
                          select c).ToList();

                string seasonS = Enum.Format(typeof(Season), Convert.ToInt32(season), "G");
                string tourn = search[0].TournamentString;
                string country = search[0].SportFederation.Country;
                string nameSport = search[0].SportFederation.NameSportString;
                search = (from c in db.FederationSeasons
                          where
                          c.SeasonString == seasonS &&
                          c.TournamentString == tourn &&
                          c.SportFederation.Country == country &&
                          c.SportFederation.NameSportString == nameSport
                          select c).ToList();

                if (fed == null)
                {
                    return null;
                }

                fed = search.First();
            }

            if (fed == null)
            {
                return null;
            }
            CountTour = (fed.TeamSeasons.Count() - 1) * 2;
            int index = 0;
            if (tour == 0)
            {
                int max = 0;
                foreach (var item in fed.TeamSeasons)
                {
                    if (item.Matches.Count() > max)
                    {
                        max = item.Matches.Count();
                    }
                }
                index = max;
                CountTour = max;
            }
            else
            {
                index = tour;
            }

            List<Match> list = new List<Match>();
            foreach (var item in fed.TeamSeasons)
            {
                List<Match> matches = item.Matches.ToList();
                if (matches.Count() >= index && index >= 1)
                {
                    if (item.Team.Name == matches[index - 1].HomeTeam)
                    {
                        Score temp = new Score();
                        temp.AwayTeam = matches[index - 1].AwayTeam;

                        string name = temp.AwayTeam;
                        string sport = fed.SportFederation.NameSportString;
                        string country = fed.SportFederation.Country;
                        var tempId = from c in db.Teams
                                     where
                                     c.Name == name &&
                                     c.Country == country &&
                                     c.NameSportString == sport
                                     select c;

                        temp.AwayTeamId = tempId.First().TeamId;

                        temp.HomeTeam = matches[index - 1].HomeTeam;

                        string nameH = temp.HomeTeam;
                        string sportH = fed.SportFederation.NameSportString;
                        string countryH = fed.SportFederation.Country;
                        var tempIdH = from c in db.Teams
                                     where
                                     c.Name == nameH &&
                                     c.Country == countryH &&
                                     c.NameSportString == sportH
                                     select c;

                        temp.HomeTeamId = tempIdH.First().TeamId;

                        temp.FederationSeasonId = fed.FederationSeasonId;
                        temp.Date = matches[index - 1].Date;
                        temp.MatchId = matches[index - 1].MatchId;
                        temp.Season = matches[index - 1].Season;
                        temp.Result = matches[index - 1].HomeTeamGoal + " - " + matches[index - 1].AwayTeamGoal;

                        score.Add(temp);
                    }
                }
            }            

            return score;
        }

        public List<string> Stub(string stub)
        {
            List<string> list;
            switch (stub)
            { 
                case "Copa del Rey":
                    {
                        list = new List<string>()
                        {
                            "2018/2019", "Sevilla",     "6","11",
                            "2017/2018", "Barcelona",   "1","11",
                            "2016/2017", "Barcelona",   "1","11",
                            "2015/2016", "Barcelona",   "1","11",
                            "2014/2015", "Barcelona",   "1","11",
                            "2013/2014", "Real Madrid", "3","11",
                            "2013/2014", "Real Madrid", "3","11",
                            "2011/2012", "Barcelona",   "1","11",
                            "2010/2011", "Real Madrid", "3","11",
                            "2009/2010", "Sevilla",     "6","11",
                            "2008/2009", "Barcelona",   "1","11",
                        };      
                        return list;
                    }
                case "Supercopa":
                    {
                        list = new List<string>()
                        {
                            "2018/2019", "Barcelona", "1",      "11",    
                            "2017/2018", "Real Madrid", "3",    "11",
                            "2016/2017", "Barcelona", "1",      "11",
                            "2015/2016", "Athletic Bilbao", "8","11",
                            "2014/2015", "Atlético Madrid", "2","11",
                            "2013/2014", "Barcelona", "1",      "11",
                            "2012/2013", "Real Madrid", "3",    "11",
                            "2011/2012", "Barcelona", "1",      "11",
                            "2010/2011", "Barcelona", "1",      "11",
                            "2009/2010", "Barcelona", "1",      "11",
                            "2008/2009", "Real Madrid", "3",    "11",
                        };
                        return list;
                    }
                case "FA Cup":
                    {
                        list = new List<string>()
                        {
                            "2018/2019", "Manchester City", "41",  "21",
                            "2017/2018", "Chelsea", "43",          "21",
                            "2016/2017", "Arsenal", "45",          "21",
                            "2015/2016", "Manchester United", "46","21",
                            "2014/2015", "Arsenal", "45",          "21",
                            "2013/2014", "Arsenal", "45",          "21",
                            "2012/2013", "Wigan", "70",            "21",
                            "2011/2012", "Chelsea", "43",          "21",
                            "2010/2011", "Manchester City", "41",  "21",
                            "2009/2010", "Chelsea", "43",          "21",
                            "2008/2009", "Chelsea", "43",          "21",
                        };
                        return list;
                    }
                case "League Cup":
                    {
                        list = new List<string>()
                        {
                            "2018/2019", "Manchester City", "41",   "21",
                            "2017/2018", "Manchester City", "41",   "21",
                            "2016/2017", "Manchester United", "46", "21",
                            "2015/2016", "Manchester City", "41",   "21",
                            "2014/2015", "Chelsea", "43",           "21",
                            "2013/2014", "Manchester City", "46",   "21",
                            "2012/2013", "Swansea", "61",           "21",
                            "2011/2012", "Liverpool", "42",         "21",
                            "2010/2011", "Birmingham City", "74",   "21",
                            "2009/2010", "Manchester United", "46", "21",
                            "2008/2009", "Manchester United", "46", "21",
                        };
                        return list;
                    }
                case "Super Cup":
                    {
                        list = new List<string>()
                        {
                            "2018/2019", "Manchester City", "41",   "21",
                            "2017/2018", "Arsenal", "45",           "21",
                            "2016/2017", "Manchester United", "46", "21",
                            "2015/2016", "Arsenal", "45",           "21",
                            "2014/2015", "Arsenal", "45",           "21",
                            "2013/2014", "Manchester United", "46", "21",
                            "2012/2013", "Manchester City", "41",   "21",
                            "2011/2012", "Manchester United", "46", "21",
                            "2010/2011", "Manchester United", "46", "21",
                            "2009/2010", "Chelsea", "43",           "21",
                            "2008/2009", "Manchester United", "46", "21",
                        };
                        return list;
                    }
            }
            return null;
        }

        public List<List<List<string>>> Search(string edit)
        {
            List<List<List<string>>> search = new List<List<List<string>>>();
            
            string name = edit;
            var searchTeams = from c in db.Teams
                             where c.Name == name
                             select c;

            if (searchTeams.Count() > 0)
            {
                List<List<string>> temp_ = new List<List<string>>();
                foreach (var item in searchTeams)
                {
                    var fed = from c in db.FederationSeasons
                              where c.SeasonString == currentSeason.ToString() &&
                              c.SportFederation.NameSportString == item.NameSportString &&
                              c.SportFederation.Country == item.Country
                              select c;
                    int? fedId = null;
                    if (fed.Count() > 0)
                    {
                        fedId = fed.First().FederationSeasonId;
                    }

                    List<string> temp = new List<string>();
                    temp.Add("Team");               
                    temp.Add(item.Name);                    
                    temp.Add(Convert.ToString(fedId));
                    temp.Add(Convert.ToString(item.TeamId));
                    temp_.Add(temp);
                }
                search.Add(temp_);
            }
                        
            var searchPlayers = from c in db.Players
                               where c.Name == name ||
                               c.Surname == name || (c.Name + " " + c.Surname == name)
                               select c;

            if (searchPlayers.Count() > 0)
            {
                List<List<string>> temp_ = new List<List<string>>();
                foreach (var item in searchPlayers)
                {
                    List<string> temp = new List<string>();
                    temp.Add("Player");
                    if (item.Surname != "")
                    {
                        temp.Add(item.Name + " " + item.Surname);
                    }
                    else
                    {
                        temp.Add(item.Name);
                    }
                    
                    string country = item.PlayerSeasons.ToList().Last().TeamSeason.FederationSeason.SportFederation.Country;
                    var fed = from c in db.FederationSeasons
                              where c.SeasonString == currentSeason.ToString() &&
                              c.SportFederation.NameSportString == item.NameSportString &&
                              c.TournamentString == "League" &&
                              c.SportFederation.Country == country
                              select c;
                    int fedId = 0;
                    if (fed.Count() > 0)
                    {
                        fedId = fed.First().FederationSeasonId;
                    }

                    temp.Add(Convert.ToString(fedId));
                    temp.Add(Convert.ToString(item.PlayerId));
                    temp_.Add(temp);
                }
                search.Add(temp_);
            }

            return search;
        }
    }
}