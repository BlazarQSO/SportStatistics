using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportStatistics.Models
{
    public class PlayerSeason
    {
        public int PlayerSeasonId { get; set; }	

        [Column("Season")]
        public string SeasonString
        {
            get { return Season.ToString(); }
            private set { Season = value.ParseEnum<Season>(); }
        }

        [NotMapped]
        public Season Season { get; set; }
        public int GamedMatches { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
                
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public int TeamSeasonId { get; set; }
        public virtual TeamSeason TeamSeason { get; set; }
        public virtual ICollection<Match> Matches { get; set; }        
    }
}