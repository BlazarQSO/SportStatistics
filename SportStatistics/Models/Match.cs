using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SportStatistics.Models
{    
    public class Match
    {
        public int MatchId { get; set; }        
        public string NameSportString
        {
            get { return NameSport.ToString(); }
            private set { NameSport = value.ParseEnum<NameSport>(); }
        }
        public NameSport NameSport { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public Season Season { get; set; }
        public string TournamentString
        {
            get { return Tournament.ToString(); }
            private set { Tournament = value.ParseEnum<Tournament>(); }
        }
        public Tournament Tournament { get; set; }
        public string NameTournament { get; set; }
        public string Tour { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime Date { get; set; }
        public string NameStadium { get; set; }        
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeTeamGoal { get; set; }
        public int AwayTeamGoal { get; set; }
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
        public string HomePlayers
        {
            get
            {
                return string.Join(",", ListHomePlayers);
            }
            set
            {
                ListHomePlayers = value.Split(',').ToList();
            }
        }
        public List<string> ListAwayPlayers { get; set; }
        public string AwayPlayers
        {
            get
            {
                return string.Join(",", ListAwayPlayers);
            }
            set
            {
                ListAwayPlayers = value.Split(',').ToList();
            }
        }
        public List<string> ListTimeLineHome { get; set; }        
        public string TimeLine
        {
            get
            {
                if (ListTimeLineHome != null)
                {
                    return string.Join(",", ListTimeLineHome);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (value != "")
                {
                    ListTimeLineHome = value.Split(',').ToList();
                }
                else
                {
                    ListTimeLineHome = null;
                }
            }
        }
        public List<string> ListTimeLineAway { get; set; }
        public string TimeLineAway
        {
            get
            {
                if (ListTimeLineAway != null)
                {
                    return string.Join(",", ListTimeLineAway);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (value != "")
                {
                    ListTimeLineAway = value.Split(',').ToList();
                }
                else
                {
                    ListTimeLineAway = null;
                }
            }
        }
        public virtual ICollection<TeamSeason> TeamSeasons { get; set; }
        //public virtual ICollection<Player> Players { get; set; }
        //public virtual TeamSeason TeamSeasonHome { get; set; }
        //public virtual TeamSeason TeamSeasonAway { get; set; }
        //public int FederationSeasonId { get; set; }
        //public virtual FederationSeason FederationSeason { get; set; }                
        public virtual ICollection<PlayerSeason> PlayerSeasons { get; set; }
    }
}