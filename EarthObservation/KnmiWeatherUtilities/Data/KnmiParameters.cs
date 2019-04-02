using System;
using System.Collections.Generic;

namespace KnmiWeatherUtilities.Data
{
    public class KnmiParameters
    {
        private const string ValueSeparator = ":";

        public string[] Stations { get; set; }

        public string[] Variables { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool InSeason { get; set; }

        public Interval Interval { get; set; }

        public Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>()
            {
                { "stns", String.Join(ValueSeparator, Stations)},
                { "vars", String.Join(ValueSeparator, Variables) },
                { "start", GetDateString(StartDate) },
                { "end", GetDateString(EndDate) },
                { "inseason", InSeason ? "Y" : "N" }
            };
        }

        private static string GetDateString(DateTime dt)
        {
            return dt.ToString("yyyyMMdd");
        }
    }
}
