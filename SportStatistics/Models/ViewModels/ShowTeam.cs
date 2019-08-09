using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportStatistics.Models.ViewModels
{
    public class TeamPlayer
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CM { get; set; }
        public int KG { get; set; }
        public int Apps { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public Position Pos { get; set; }
    }

    public class TeamStatistics
    {
        public string Sport { get; set; }
        public string Federation { get; set; }
        public int FederationSeasonId { get; set; }
        public Season Season { get; set; }
        public string Tournament { get; set; }
        public int Apps { get; set; }        
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Loss { get; set; }
        public int Goals { get; set; }
    }

    public class ShowTeam
    {
        public Team Team { get; set; }
        public List<TeamPlayer> Players { get; set; }
        public List<TeamStatistics> Statistics { get; set; }
    }
}