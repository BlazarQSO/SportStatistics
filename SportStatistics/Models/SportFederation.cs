using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportStatistics.Models
{
    public class SportFederation
    {
        public int SportFederationId { get; set; }

        [Column("NameSport")]
        [Display(Name = "Name Sport")]
        public string NameSportString
        {
            get { return NameSport.ToString(); }
            private set { NameSport = value.ParseEnum<NameSport>(); }
        }

        [NotMapped]        
        public NameSport NameSport { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Enter the data")]        
        public string Country { get; set; }

        [Display(Name = "Foundation Date")]        
        public string FoundationDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "President")]
        public string NamePresident { get; set; }


        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }        
        public virtual ICollection<FederationSeason> FederationSeasons { get; set; }
    }
}