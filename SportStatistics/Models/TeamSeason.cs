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
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public int FederationSeasonId { get; set; }
        public virtual FederationSeason FederationSeason { get; set; }
        public string TournamentString
        {
            get { return Tournament.ToString(); }
            private set { Tournament = value.ParseEnum<Tournament>(); }
        }
        public Tournament Tournament { get; set; }
        public Season Season { get; set; }
        public virtual ICollection<PlayerSeason> PlayerSeasons { get; set; }
        public virtual ICollection<Match> Matches { get; set; }        
        public byte Point { get; set; }
        public byte HomePoint { get; set; }
        public byte Win { get; set; }
        public byte HomeWin { get; set; }
        public byte Draw { get; set; }
        public byte HomeDraw { get; set; }
        public byte Lose { get; set; }
        public byte HomeLose { get; set; }
        public int Goals { get; set; }
        public int HomeGoals { get; set; }
        public int GoalAgainst { get; set; }
        public int HomeGoalAgainst { get; set; }
    }
}