using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportStatistics.Models
{
    public class Sport
    {        
        public int SportId { get; set; }

        [Column("NameSport")]
        public string NameSportString
        {
            get { return NameSport.ToString(); }
            private set { NameSport = value.ParseEnum<NameSport>(); }
        }
                
        [NotMapped]
        [Display(Name = "Select the type of sport")]
        public NameSport NameSport { get; set; }
        public virtual ICollection<SportFederation> SportFederations { get; set; }
    }
}