using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace SportStatistics.Models
{
    public static class StringExtensions
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }

    public enum Result
    {
        Win,
        Draw,
        Lose
    }

    public enum Position
    {
        Forward,
        Midfielder,
        Defender,
        Goalkeeper
    }

    public enum NameSport
    {
        Football,
        Hockey,
        Basketball,
        FigureSkating,
        Volleyball
    }

    public enum Tournament : byte
    {
        League = 1,
        Cup,
        SuperCup,
        ChampionsLeague,
        EuropaLeague,
        EuropaSuperCup,
        WorldTeamCup,
    }

    public enum Season
    {        
        [Display(Name = "2009/2010")]
        _2009_2010 = 2009,
        [Display(Name = "2010/2011")]
        _2010_2011 = 2010,
        [Display(Name = "2011/2012")]
        _2011_2012 = 2011,
        [Display(Name = "2012/2013")]
        _2012_2013 = 2012,
        [Display(Name = "2013/2014")]
        _2013_2014 = 2013,
        [Display(Name = "2014/2015")]
        _2014_2015 = 2014,
        [Display(Name = "2015/2016")]
        _2015_2016 = 2015,
        [Display(Name = "2016/2017")]
        _2016_2017 = 2016,
        [Display(Name = "2017/2018")]
        _2017_2018 = 2017,
        [Display(Name = "2018/2019")]
        _2018_2019 = 2018,
    }

    public static class LeagueName
    {
        public static string Name(string CountryNameSportTournament)
        {
            switch (CountryNameSportTournament)
            {
                case "EnglandFootballLeague":
                    {
                        return "English Premier League";
                    }
                case "SpainFootballLeague":
                    {
                        return "La Liga";
                    }
                case "UEFAFootballLeague":
                    {
                        return "UEFA Champions League";
                    }
                case "GermanyFootballLeague":
                    {
                        return "Bundesliga";
                    }
                case "ItalyFootballLeague":
                    {
                        return "Serie A";
                    }
                case "FranceFootballLeague":
                    {
                        return "Ligue 1";
                    }
            }
            return "";
        }
    }

    public static class myException
    {
        public static string Message { get; set; }
    }
}