using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportStatistics.Models
{    
    public class Match
    {
        public int MatchId { get; set; }
        public int FederationSeasonId { get; set; }
        public virtual FederationSeason FederationSeason { get; set; }
        public int TeamSeasonId { get; set; }
        public virtual TeamSeason TeamSeason { get; set; }
        public int PlayerSeasonId { get; set; }
        public virtual PlayerSeason PlayerSeason { get; set; }
        public Season Season { get; set; }        
        public string TournamentString
        {
            get { return Tournament.ToString(); }
            private set { Tournament = value.ParseEnum<Tournament>(); }
        }
        public Tournament Tournament { get; set; }
        public string Tour { get; set; }
        public DateTime Date { get; set; }
        public string NameStadium { get; set; }        
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public byte HomeTeamGoal { get; set; }
        public byte AwayTeamGoal { get; set; }
        public string HomeTeamResultString
        {
            get { return HomeTeamResult.ToString(); }
            private set { HomeTeamResult = value.ParseEnum<Result>(); }
        }
        public Result HomeTeamResult { get; set; }        
        public string AwayTeamResultString
        {
            get { return AwayTeamResult.ToString(); }
            private set { AwayTeamResult = value.ParseEnum<Result>(); }
        }
        public Result AwayTeamResult { get; set; }
        public Point HomeTeamPoint { get; set; }
        public Point AwayTeamPoint { get; set; }        
        public List<string> ListHomePlayers { get; set; }
        public List<string> ListAwayPlayers { get; set; }
        public List<string> ListTimeLine { get; set; }        
        public string TimeLine
        {
            get { return string.Join(",", ListTimeLine); }
            set { ListTimeLine = value.Split(',').ToList(); }
        }
    }
}