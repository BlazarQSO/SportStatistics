using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportStatistics.Models
{
    public class TeamSeason
    {
        public int TeamSeasonId { get; set; }
		public string NameTeam { get; set; }
       
        [Column("Season")]
        public string SeasonString
        {
            get { return Season.ToString(); }
            private set { Season = value.ParseEnum<Season>(); }
        }

        [NotMapped]
        public Season Season { get; set; }        
		public int Played { get; set; }
		public int HomePlayed { get; set; }
        public int Point { get; set; }		
        public int HomePoint { get; set; }
        public int Win { get; set; }
        public int HomeWin { get; set; }
        public int Draw { get; set; }
        public int HomeDraw { get; set; }
        public int Lose { get; set; }
        public int HomeLose { get; set; }
        public int Goals { get; set; }
        public int HomeGoals { get; set; }
        public int GoalAgainst { get; set; }
        public int HomeGoalAgainst { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public int FederationSeasonId { get; set; }
        public virtual FederationSeason FederationSeason { get; set; }        
        public virtual ICollection<Match> Matches { get; set; }
        public virtual ICollection<PlayerSeason> PlayerSeasons { get; set; }
    }
}