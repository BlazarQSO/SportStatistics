using System.Collections.Generic;

namespace SportStatistics.Models
{
    public class FederationSeason
    {
        public int FederationSeasonId { get; set; }        
        public NameSport NameSport { get; set; }
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