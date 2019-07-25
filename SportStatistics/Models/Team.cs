using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportStatistics.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        [Column("NameSport")]
        public string NameSportString
        {
            get { return NameSport.ToString(); }
            private set { NameSport = value.ParseEnum<NameSport>(); }
        }

        [NotMapped]        
        public NameSport NameSport { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string NameStadium { get; set; }        
        public string FoundationDate { get; set; }        
        public virtual ICollection<TeamSeason> TeamSeasons { get; set; }
    }
}