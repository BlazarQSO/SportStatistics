using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class ShowMatch
    {        
        public string NameSportString { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public Season Season { get; set; }        
        public Tournament Tournament { get; set; }
        public string NameTournament { get; set; }
        public string Tour { get; set; }
        public DateTime Date { get; set; }
        public string NameStadium { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeTeamGoal { get; set; }
        public int AwayTeamGoal { get; set; }                        
        public List<string> ListHomePlayers { get; set; }        
        public List<string> ListAwayPlayers { get; set; }        
        public List<string> ListTimeLineHome { get; set; }        
        public List<string> ListTimeLineAway { get; set; }        
        public IdMatch IdMatch { get; set; }        
    }

    public class IdMatch
    {
        public int TeamHomeId { get; set; }
        public int TeamAwayId { get; set; }
        public int FederationSeasonId { get; set; }
        public List<int> HomeTeamPlayersId { get; set; }
        public List<int> AwayTeamPlayersId { get; set; }
    }
}
