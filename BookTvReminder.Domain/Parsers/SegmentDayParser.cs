using System;
using System.Collections.Generic;
using System.Globalization;

namespace BookTvReminder.Domain.Parsers
{
    public class SegmentDayParser
    {
        /// <summary>
        /// Intended to match input like this "Saturday, September 25th"
        /// </summary>
        private readonly List<string> parseFormatPatterns = new List<string>
                                                                {
                                                                    "dddd, MMMM d'th',yyyy",
                                                                    "dddd, MMMM d'st',yyyy",
                                                                    "dddd, MMMM d'nd',yyyy",
                                                                    "dddd, MMMM d'rd',yyyy"
                                                                };

        private readonly Dictionary<string, DateTime> parserCache;
        //currentYear is isolated for easier testing
        private readonly int currentYear;

        public SegmentDayParser()
            : this(DateTime.Today.Year)
        {
        }

        public SegmentDayParser(int currentYear)
        {
            parserCache = new Dictionary<string, DateTime>();
            this.currentYear = currentYear;
        }

        public DateTime Parse(string dayString)
        {
            DateTime parsedDate;
            string dayStringWithYear = dayString + "," + currentYear;

            if (parserCache.TryGetValue(dayString, out parsedDate))
            {
                return parsedDate;
            }

            foreach (var formatPattern in parseFormatPatterns)
            {
                bool parsed = DateTime.TryParseExact(dayStringWithYear,
                                                     formatPattern,
                                                     DateTimeFormatInfo.CurrentInfo,
                                                     DateTimeStyles.None,
                                                     out parsedDate);

                //If parsed successfully, put in cache and short-circuit out of method
                if (parsed)
                {
                    parserCache[dayString] = parsedDate;
                    return parsedDate;
                }
            }

            //If nothing matched, fail loudly.
            throw new ArgumentException("Day string does not match any supported pattern. Day string: [" + dayString + "]");
        }
    }
}
