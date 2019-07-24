using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        Frist = 2000,
        [Display(Name = "2001/2002")]
        Second = 2001,
        [Display(Name = "2002/2003")]
        Thred = 2002,
        [Display(Name = "2003/2004")]
        Fourth = 2003,
        [Display(Name = "2004/2005")]
        Fifth = 2004,
        [Display(Name = "2005/2006")]
        Sixth = 2005,
        [Display(Name = "2006/2007")]
        Seventh = 2006,
        [Display(Name = "2007/2008")]
        Eighth = 2007,
        [Display(Name = "2008/2009")]
        Nineth = 2008,
        [Display(Name = "2009/2010")]
        Tenth = 2009,
        [Display(Name = "2010/2011")]
        Eleventh = 2010,
        [Display(Name = "2011/2012")]
        Twelfth = 2011,
        [Display(Name = "2012/2013")]
        Thirteenth = 2012,
        [Display(Name = "2013/2014")]
        Fourteenth = 2013,
        [Display(Name = "2014/2015")]
        Fifteenth = 2014,
        [Display(Name = "2015/2016")]
        Sixteenth = 2015,
        [Display(Name = "2016/2017")]
        Seventeenth = 2016,
        [Display(Name = "2017/2018")]
        Eighteenth = 2017,
        [Display(Name = "2018/2019")]
        Nineteenth = 2018,
        [Display(Name = "2019/2020")]
        Twentieth = 2019,
        [Display(Name = "2020/2021")]
        TwentyFirst = 2020,
    }
}