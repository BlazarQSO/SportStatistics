using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportStatistics.Models.ViewModels
{
    public class Winner
    {
        public int FederationSeasonId { get; set; }
        public int TeamId { get; set; }
        public Season Season { get; set; }
        public string TeamName { get; set; }
    }
}