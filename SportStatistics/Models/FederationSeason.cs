using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportStatistics.Models
{
    public class FederationSeason
    {
        public int FederationSeasonId { get; set; }

        [Column("NameSport")]
        public string NameSportString
        {
            get { return NameSport.ToString(); }
            private set { NameSport = value.ParseEnum<NameSport>(); }
        }

        [NotMapped]        
        public NameSport NameSport { get; set; }
        public string Country { get; set; }
        public string TournamentString
        {
            get { return Tournament.ToString(); }
            private set { Tournament = value.ParseEnum<Tournament>(); }
        }
        public Tournament Tournament { get; set; }
        public string NameTournament { get; set; }
        public Season Season { get; set; }
        
        public int SportFederationId { get; set; }
        public virtual SportFederation SportFederation { get; set; }
        public virtual ICollection<TeamSeason> TeamSeasons { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}