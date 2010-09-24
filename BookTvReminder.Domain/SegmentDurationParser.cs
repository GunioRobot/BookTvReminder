using System;
using System.Text.RegularExpressions;

namespace BookTvReminder.Domain
{
    public class SegmentDurationParser
    {
        private const string hoursGroupName = "Hours";
        private const string minutesGroupName = "Minutes";

        private readonly Regex durationRegex =
            new Regex(
                @"\s*Approx\.\s*((?<" + hoursGroupName + @">\d+)\s*hr\.\s*)*\s*((?<" + minutesGroupName + @">\d+)\s*min\.)*\s*",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);


        public int GetDurationInMinutes(string duration)
        {
            Match match = durationRegex.Match(duration);

            if (!match.Success)
            {
                throw new ArgumentException("Segment duration does not match expected pattern. Original string:[" +
                                            duration + "]");
            }

            int hours = 0;
            int minutes = 0;

            int.TryParse(match.Groups[hoursGroupName].Value, out hours);
            int.TryParse(match.Groups[minutesGroupName].Value, out minutes);

            if (hours == 0 && minutes == 0)
            {
                var hoursString = match.Groups[hoursGroupName].Value;
                var minutesString = match.Groups[minutesGroupName].Value;

                if (string.IsNullOrEmpty(hoursString) && string.IsNullOrEmpty(minutesString))
                {
                    throw new ArgumentException(
                        "Invalid segment duration string passed to parser. No hours or minutes exist. Original string:[" +
                        duration + "]");
                }
            }

            return (hours * 60) + minutes;
        }
    }
}
