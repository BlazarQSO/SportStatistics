using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportStatistics.Models.ViewModels
{
    public class Progress
    {
        public int TeamId { get; set; }
        public int FederationSeasonId { get; set; }
        public Season Season { get; set; }
        public string NameTeam { get; set; }        
        public int Count { get; set; }
        public List<Form> Form { get; set; }
    }
}