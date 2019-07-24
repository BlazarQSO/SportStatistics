using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportStatistics.Models
{
    public class Player
    {
        // @String.Format("{0:d}", item.Birthday)
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string PositionString
        {
            get { return Position.ToString(); }
            private set { Position = value.ParseEnum<Position>(); }
        }
        public Position Position { get; set; }        
        public virtual ICollection<PlayerSeason> PlayerSeasons { get; set; }
    }
}