using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class FederationSeason
    {
        public int FederationSeasonId { get; set; }
        public int SportFederationId { get; set; }
        public virtual SportFederation SportFederation { get; set; }        
        public string TournamentString
        {
            get { return Tournament.ToString(); }
            private set { Tournament = value.ParseEnum<Tournament>(); }
        }
        public Tournament Tournament { get; set; }
        public string NameTournament { get; set; }
        public Season Season { get; set; }
        public virtual ICollection<TeamSeason> TeamSeasons { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}