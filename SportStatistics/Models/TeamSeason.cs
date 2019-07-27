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

        [Column("NameSport")]
        public string NameSportString
        {
            get { return NameSport.ToString(); }
            private set { NameSport = value.ParseEnum<NameSport>(); }
        }

        [NotMapped]        
        public NameSport NameSport { get; set; }
        public string TournamentString
        {
            get { return Tournament.ToString(); }
            private set { Tournament = value.ParseEnum<Tournament>(); }
        }
        public Tournament Tournament { get; set; }
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