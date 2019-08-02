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
        [Display(Name = "2000/2001")]
        _2000_2001 = 2000,
        [Display(Name = "2001/2002")]
        _2001_2002 = 2001,
        [Display(Name = "2002/2003")]
        _2002_2003 = 2002,
        [Display(Name = "2003/2004")]
        _2003_2004 = 2003,
        [Display(Name = "2004/2005")]
        _2004_2005 = 2004,
        [Display(Name = "2005/2006")]
        _2005_2006 = 2005,
        [Display(Name = "2006/2007")]
        _2006_2007 = 2006,
        [Display(Name = "2007/2008")]
        _2007_2008 = 2007,
        [Display(Name = "2008/2009")]
        _2008_2009 = 2008,
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
        [Display(Name = "2019/2020")]
        _2019_2020 = 2019,
        [Display(Name = "2020/2021")]
        _2020_2021 = 2020,
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