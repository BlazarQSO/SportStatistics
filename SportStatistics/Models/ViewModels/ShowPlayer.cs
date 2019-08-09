using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportStatistics.Models.ViewModels
{
    public class Total
    {
        public string TournamentName { get; set; }
        public int TotalGames { get; set; }
        public int TotalGoals { get; set; }
        public int TotalAssists { get; set; }
        public int GsummA
        {
            get
            {
                return TotalGoals + TotalAssists;
            }
        }
    }

    public class PlayedGame
    {
        public string Date { get; set; }
        public string TournamentName { get; set; }
        public int MatchId { get; set; }
        public string ResultMatch { get; set; }
        public int GoalsInGame { get; set; }
        public int AssistsInGame { get; set; }
        public int GsummA
        {
            get
            {
                return GoalsInGame + AssistsInGame;
            }
        }
    }
    
    public class ShowPlayer
    {        
        public int FederationSeasonId { get; set; }
        public Season Season { get; set; }
        public Player Player { get; set; }
        public List<PlayedGame> Game { get; set; }
        public List<Total> AllGame { get; set; }
    }
}