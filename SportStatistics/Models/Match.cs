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
        [Column("Name Sport")]
        public string NameSportString
        {
            get { return NameSport.ToString(); }
            private set { NameSport = value.ParseEnum<NameSport>(); }
        }
        [NotMapped]
        public NameSport NameSport { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        [Column("Season")]
        public string SeasonString
        {
            get { return Season.ToString(); }
            private set { Season = value.ParseEnum<Season>(); }
        }

        [NotMapped]
        public Season Season { get; set; }

        [Column("Tournament")]
        public string TournamentString
        {
            get { return Tournament.ToString(); }
            private set { Tournament = value.ParseEnum<Tournament>(); }
        }

        [NotMapped]
        public Tournament Tournament { get; set; }
        public string NameTournament { get; set; }
        public string Tour { get; set; }
        public string Date { get; set; }
        public string NameStadium { get; set; }        
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeTeamGoal { get; set; }
        public int AwayTeamGoal { get; set; }

        [Column("Home Team Result")]
        public string HomeTeamResultString
        {
            get { return HomeTeamResult.ToString(); }
            private set { HomeTeamResult = value.ParseEnum<Result>(); }
        }

        [NotMapped]
        public Result HomeTeamResult { get; set; }        

        [Column("Away Team Result")]
        public string AwayTeamResultString
        {
            get { return AwayTeamResult.ToString(); }
            private set { AwayTeamResult = value.ParseEnum<Result>(); }
        }

        [NotMapped]
        public Result AwayTeamResult { get; set; }
        public int HomeTeamPoint { get; set; }
        public int AwayTeamPoint { get; set; }        
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
        public string TimeLineHome
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
        public virtual ICollection<PlayerSeason> PlayerSeasons { get; set; }
    }
}