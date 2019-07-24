using System.Collections.Generic;

namespace SportStatistics.Models
{
    public class PlayerSeason
    {
        public int PlayerSeasonId { get; set; }        
        public string TournamentString
        {
            get { return Tournament.ToString(); }
            private set { Tournament = value.ParseEnum<Tournament>(); }
        }
        public Tournament Tournament { get; set; }
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