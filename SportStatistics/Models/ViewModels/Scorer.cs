using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportStatistics.Models.ViewModels
{
    public class Scorer
    {
        public int PlayerId { get; set; }
        public Season Season { get; set; }
        public int FederationSeasonId { get; set; }
        public string Name { get; set; }
        public string NameTeam { get; set; }
        public int TeamId { get; set; }
        public int Apps { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
    }
}