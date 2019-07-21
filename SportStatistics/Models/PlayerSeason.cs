using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportStatistics.Models
{
    public class PlayerSeason
    {
        public int PlayerSeasonId { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public int TeamSeasonId { get; set; }
        public virtual TeamSeason TeamSeason { get; set; }
        public string TournamentString
        {
            get { return Tournament.ToString(); }
            private set { Tournament = value.ParseEnum<Tournament>(); }
        }
        public Tournament Tournament { get; set; }        
        public Season Season { get; set; }
        public byte GamedMatches { get; set; }
        public byte Goals { get; set; }
        public byte Assists { get; set; }
        public virtual ICollection<Match> Matches { get; set; }        
    }
}