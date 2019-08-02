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

        public List<Standings> CreateModelStandings(string sport, string federation, string tournament, int? fedSeason)
        {
            List<Standings> list = new List<Standings>();
            List<FederationSeason> search = new List<FederationSeason>();            
            if (fedSeason == null)
            {
                string season = currentSeason.ToString();
                search  = (from c in db.FederationSeasons
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
                    matches.Reverse();
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
                        if (standings.Form.Count() == 5)
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
                            if (standings.Form.Count() == 5)
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
                            if (standings.Form.Count() == 5)
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

        public ShowTeam CreateModelsShowTeam(int TeamId, int fedSeason)
        {
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

            var season = from c in db.TeamSeasons
                         where c.TeamId == TeamId && c.FederationSeasonId == fedSeason
                         select c;

            var seasons = season.ToList();
            if (seasons.Count() > 0)
            {
                foreach (var item in seasons)
                {
                    TeamStatistics stat = new TeamStatistics();
                    stat.Federation = item.FederationSeason.SportFederation.Country;
                    stat.Sport = item.FederationSeason.SportFederation.Sport.NameSport.ToString();
                    stat.FederationSeasonId = item.FederationSeasonId;
                    stat.Tournament = item.FederationSeason.Tournament;                    
                    stat.Apps = item.Played;
                    stat.Win = item.Win;
                    stat.Draw = item.Draw;
                    stat.Loss = item.Lose;
                    stat.Goals = item.Goals;
                    show.Statistics.Add(stat);                    
                }
            }

            List<TeamPlayer> listPlayer = new List<TeamPlayer>();
            foreach (var item in seasons)
            {
                foreach(var item2 in item.PlayerSeasons)
                {                    
                    bool check = false;
                    int index = -1;
                    for(int i = 0; i < listPlayer.Count(); i++)
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
            show.Players = listPlayer;
            return show;
        }

        public List<Scorer> Scorers(int fedSeason)
        {
            List<Scorer> scorers = new List<Scorer>();
            var search = from c in db.FederationSeasons
                         where c.FederationSeasonId == fedSeason
                         select c;

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
                            scorer.FederationSeasonId = fedSeason;
                            scorer.Season = player.Season;
                            scorer.Name = player.Player.Name + " " + player.Player.Surname;
                            scorer.NameTeam = item.Team.Name;
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

        public ShowPlayer ShowPlayer(int fedSeason, int PlayerId)
        {
            ShowPlayer showPlayer = new ShowPlayer();
            showPlayer.Game = new List<PlayedGame>();
            showPlayer.AllGame = new List<Total>();

            Player player = (from c in db.Players
                            where c.PlayerId == PlayerId
                            select c).ToList().First();
            showPlayer.Player = player;
            showPlayer.FederationSeasonId = fedSeason;

            var search = db.FederationSeasons.Find(fedSeason);
            string season = search.Season.ToString();
            var playerSeason = from c in db.PlayerSeasons
                                   where c.PlayerId == PlayerId &&
                                   c.SeasonString == season
                                   select c;

            foreach (var item in playerSeason)
            {
                foreach (var game in item.Matches)
                {
                    PlayedGame playedGame = new PlayedGame();
                    playedGame.Tournament = item.TeamSeason.FederationSeason.Tournament;
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
                    showPlayer.Game.Add(playedGame);
                }
            }
                        
            foreach(Tournament tour in Enum.GetValues(typeof(Tournament)))
            {
                Total total = new Total();
                total.Tournament = tour;
                string tourS = tour.ToString();
                var allSeasonTour = from c in db.PlayerSeasons
                                  where c.PlayerId == PlayerId && 
                                  c.TeamSeason.FederationSeason.TournamentString == tourS
                                  select c;
                foreach(var item in allSeasonTour)
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
                        
            var search = from c in db.FederationSeasons
                         where c.SportFederationId == fed.SportFederationId &&
                         c.TournamentString == fed.TournamentString
                         select c;

            foreach(var item in search)
            {
                TeamSeason team = new TeamSeason();
                foreach (var season in item.TeamSeasons)
                {
                    if (season.Point > team.Point)
                    {
                        team = season;
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
            string country = search.Country;
            var team = from c in db.Teams
                       where
                           c.Name == name &&
                           c.NameSportString == nameSport &&
                           c.Country == country
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
                match.NameTournament = search.Tournament.ToString();
            }                     

            match.IdMatch.TeamHomeId = search.TeamSeasons.First().TeamId;
            match.IdMatch.TeamAwayId = search.TeamSeasons.Last().TeamId;
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
    }
}