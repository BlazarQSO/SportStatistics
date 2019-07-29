using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportStatistics.Models
{
    public class FederationSeason
    {
        public int FederationSeasonId { get; set; }

        [Column("Tournament")]
        public string TournamentString
        {
            get { return Tournament.ToString(); }
            private set { Tournament = value.ParseEnum<Tournament>(); }
        }

        [NotMapped]
        public Tournament Tournament { get; set; }
        public string NameTournament { get; set; }

        [Column("Season")]
        public string SeasonString
        {            
            get { return Season.ToString(); }
            private set { Season = value.ParseEnum<Season>(); }
        }

        [NotMapped]
        public Season Season { get; set; }
        
        public int SportFederationId { get; set; }
        public virtual SportFederation SportFederation { get; set; }
        public virtual ICollection<TeamSeason> TeamSeasons { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}