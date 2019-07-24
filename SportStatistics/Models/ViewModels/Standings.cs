using System;
using System.Collections.Generic;

namespace SportStatistics.Models.ViewModels
{
    public class Form
    {        
        public int MatchId { get; set; }
        public Result WinDL { get; set; }
        public string Result { get; set; }        
    }
    public class Standings
    {
        public int TeamId { get; set; }
        public int FederationSeasonId { get; set; }
        public Season Season { get; set; }
        public string NameTeam { get; set; }
        public int GamedMatches { get; set; }
        public int Win { get; set; }        
        public int Draw { get; set; }        
        public int Lose { get; set; }
        public int Goals { get; set; }
        public int GoalAgainst { get; set; }
        public string GoalDifference
        {
            get
            {
                if (Goals - GoalAgainst > 0)
                {
                    return "+" + Convert.ToString(Goals - GoalAgainst);
                }
                else
                {
                    return Convert.ToString(Goals - GoalAgainst);
                }
            }
        }
        public int Point { get; set; }
        public List<Form> Form { get; set; }
    }
}