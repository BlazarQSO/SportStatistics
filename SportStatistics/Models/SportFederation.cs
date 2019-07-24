using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportStatistics.Models
{
    public class SportFederation
    {
        public int SportFederationId { get; set; }        

        [MaxLength(50)]
        [Required(ErrorMessage = "Enter the data")]
        [Display(Name = "Enter the name of the country")]
        public string Country { get; set; }

        [Display(Name = "Enter the date of the foundation")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FoundationDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Enter the name of the President")]
        public string NamePresident { get; set; }


        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }
        //public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<FederationSeason> FederationSeasons { get; set; }
    }
}