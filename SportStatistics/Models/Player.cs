using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Player
    {
        // @String.Format("{0:d}", item.Birthday)
        public int PlayerId { get; set; }        
        public string Name { get; set; }
        public string Surname { get; set; }
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
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<PlayerSeason> PlayerSeasons { get; set; }
    }
}