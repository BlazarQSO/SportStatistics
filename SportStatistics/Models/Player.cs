using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportStatistics.Models
{
    public class Player
    {        
        public int PlayerId { get; set; }

        [Column("NameSport")]
        public string NameSportString
        {
            get { return NameSport.ToString(); }
            private set { NameSport = value.ParseEnum<NameSport>(); }
        }

        [NotMapped]        
        public NameSport NameSport { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }

        [Column("Position")]
        public string PositionString
        {
            get { return Position.ToString(); }
            private set { Position = value.ParseEnum<Position>(); }
        }
        [NotMapped]
        public Position Position { get; set; }        
        public virtual ICollection<PlayerSeason> PlayerSeasons { get; set; }
    }
}