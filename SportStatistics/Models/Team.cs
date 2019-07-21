using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportStatistics.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public int SportFederationId { get; set; }
        public virtual SportFederation SportFederation { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string NameStadium { get; set; }        
        public DateTime FoundationDate { get; set; }
        public virtual ICollection<TeamSeason> TeamSeasons { get; set; }
    }
}