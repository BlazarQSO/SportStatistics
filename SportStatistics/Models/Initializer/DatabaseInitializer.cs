using System;
using System.Collections.Generic;
using System.Data.Entity;
namespace SportStatistics.Models.Initializer
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Sport sport = new Sport()
            {
                SportId = 1,
                NameSport = NameSport.Football,
            };
            SportFederation sportFederation = new SportFederation()
            {
                SportFederationId = 1,                
                Sport = sport,
                Country = "Spain",
                FoundationDate = new DateTime(1913, 9, 29),
                NamePresident = "Luis Manuel Rubiales",
            };
            FederationSeason federationSeason = new FederationSeason()
            {
                FederationSeasonId = 1,                
                SportFederation = sportFederation,
                Season = Season.Nineteenth,
                Tournament = Tournament.League,
                NameTournament = "La Liga",                
                // ListTeams = new List<string>() { "Athletic Bilbao", "Atletico Madrid", "Barcelona", "Celta Vigo", "Deportivo Alaves", "Eibar", "Espanyol", "Getafe", "Girona", "Leganes", "Levante", "Rayo Vallecano", "Real Betis", "Real Madrid", "Real Sociedad", "Real Valladolid", "SD Huesca", "Sevilla", "Valencia", "Villarreal" },                
            };
            Team team = new Team()
            {
                TeamId = 1,                
                //SportFederation = sportFederation,
                Country = "Spain",
                City = "Barcelona",
                Name = "Barcelona",
                FoundationDate = new DateTime(1899, 10, 29),
                NameStadium = "Camp Nou",                
            };
            TeamSeason teamSeason = new TeamSeason()
            {
                TeamSeasonId = 1,               
                Team = team,
                FederationSeason = federationSeason,                      
                Season = Season.Nineteenth,
                Tournament = Tournament.League,
                Point = 3,
                HomePoint = 0,
                Win = 1,
                HomeWin = 0,
                Draw = 0,
                HomeDraw = 0,
                Lose = 0,
                HomeLose = 0,
                Goals = 4,
                HomeGoals = 0,
                GoalAgainst = 2,
                HomeGoalAgainst = 0,                
                //ListPlayers = new List<string>() { "Lionel Messi", "Jasper Cillessen", "Luis Suárez", "Coutinho", "Jordi Alba", "Sergio Busquets", "Juan Miranda", "Gerard Piqué", "Rafinha", "Ousmane Dembélé", "Ivan Rakitic", "Sergi Roberto", "Clément Lenglet", "Marc-André ter Stegen", "Arthur", "Nélson Semedo", "Arturo Vidal", "Thomas Vermaelen", "Malcom", "Carles Aleñá", "Munir El Haddadi", "Denis Suárez", "Samuel Umtiti" },
            };
            Team team2 = new Team()
            {
                TeamId = 2,
                Country = "Spain",
                City = "Sevilla",
                Name = "Sevilla",
                FoundationDate = new DateTime(1890, 1, 25),
                NameStadium = "Camp Nou",
            };
            TeamSeason teamSeason2 = new TeamSeason()
            {
                TeamSeasonId = 2,
                Team = team2,
                FederationSeason = federationSeason,
                Season = Season.Nineteenth,
                Tournament = Tournament.League,
                Point = 0,
                HomePoint = 0,
                Win = 0,
                HomeWin = 0,
                Draw = 0,
                HomeDraw = 0,
                Lose = 1,
                HomeLose = 1,
                Goals = 2,
                HomeGoals = 2,
                GoalAgainst = 4,
                HomeGoalAgainst = 4,
            };
            Player player = new Player()
            {
                PlayerId = 1,
                Name = "Lionel",
                Surname = "Messi",
                Birthday = new DateTime(1987, 6, 24),
                Age = 32,
                Nationality = "Argentina",
                Position = Position.Forward,
                Height = 170,
                Weight = 72,                
            };
            PlayerSeason playerSeason = new PlayerSeason()
            {
                PlayerSeasonId = 1,                           
                Player = player,                
                Tournament = Tournament.League,
                GamedMatches = 1,
                Goals = 3,
                Assists = 1,
                Season = Season.Nineteenth,
            };
            Match match = new Match()
            {
                MatchId = 1,
                //TeamSeasons = new List<TeamSeason>() { teamSeason2, teamSeason },                
                //PlayerSeason = playerSeason,  
                //Country = "Spain",
                //NameSport = NameSport.Football,
                Season = Season.Nineteenth,
                Tournament = Tournament.League,
                Tour = "25",
                Date = new DateTime(2019, 2, 23, 19, 30, 00),
                NameStadium = "Ramón Sánchez Pizjuán",
                HomeTeam = "Sevilla",
                HomeTeamGoal = 2,
                HomeTeamPoint = Point.Zero,
                HomeTeamResult = Result.Lose,
                AwayTeam = "Barcelona",
                AwayTeamGoal = 4,
                AwayTeamPoint = Point.Tree,
                AwayTeamResult = Result.Win,
                ListHomePlayers = new List<string>() { "Tomás Vaclik", "Gabriel Mercado", "Simon Kjaer", "Sergi Gómez", "Maximilian Wöber", "Jesús Navas", "Éver Banega", "Marko Rog", "Quincy Promes", "Pablo Sarabia", "Wissam Ben Yedder", "Franco Vázquez", "Ibrahim Amadou", "Roque Mesa" },
                ListAwayPlayers = new List<string>() { "Marc-André ter Stegen", "Nélson Semedo","Samuel Umtiti","Gerard Piqué","Jordi Alba","Arturo Vidal","Ivan Rakitic","Sergio Busquets","Lionel Messi","Luis Suárez","Coutinho","Ousmane Dembélé","Sergi Roberto", "Carles Aleñá" },
                ListTimeLine = new List<string>() { "A:22:Wissam Ben Yedder", "G:22:Jesús Navas", "A:26:Ivan Rakitic", "G:26:Lionel Messi", "A:42:Pablo Sarabia", "G:42:Gabriel Mercado", "A:67:Ousmane Dembélé", "G:67:Lionel Messi", "G:85:Lionel Messi", "A:90+2:Lionel Messi", "G:90+2:Luis Suárez" }
            };
                        
            context.Sports.Add(sport);
            context.SportFederation.Add(sportFederation);
            context.FederationSeasons.Add(federationSeason);            
            context.Teams.Add(team);
            context.Teams.Add(team2);
            context.TeamSeasons.Add(teamSeason);
            context.TeamSeasons.Add(teamSeason2);
            context.Matches.Add(match);
            context.Players.Add(player);
            context.PlayerSeasons.Add(playerSeason);

            base.Seed(context);
        }
    }
}