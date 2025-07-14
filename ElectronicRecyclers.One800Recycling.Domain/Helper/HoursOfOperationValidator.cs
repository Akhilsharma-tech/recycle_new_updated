using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ElectronicRecyclers.One800Recycling.Domain.Common.Helper
{
    public static class HoursOfOperationValidator
    {
        private static readonly Regex hoursRegex = new Regex(
            @"^(?:(?<day>(Mon(day)?|Tue(sday)?|Wed(nesday)?|Thu(rsday)?|Fri(day)?|Sat(urday)?|Sun(day)?|M|Tu|W|Th|F|Sa|Su|Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday))" +
            @"(?:\s*-\s*(?<day2>(Mon(day)?|Tue(sday)?|Wed(nesday)?|Thu(rsday)?|Fri(day)?|Sat(urday)?|Sun(day)?|M|Tu|W|Th|F|Sa|Su|Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday)))?)\s+" +
            @"(Closed|(([0-9]{1,2})(\s*:\s*[0-5][0-9])?\s*(AM|PM)?\s*-\s*([0-9]{1,2})(\s*:\s*[0-5][0-9])?\s*(AM|PM)?))" +
            @"(\s*,\s*(?<day>(Mon(day)?|Tue(sday)?|Wed(nesday)?|Thu(rsday)?|Fri(day)?|Sat(urday)?|Sun(day)?|M|Tu|W|Th|F|Sa|Su|Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday))" +
            @"(?:\s*-\s*(?<day2>(Mon(day)?|Tue(sday)?|Wed(nesday)?|Thu(rsday)?|Fri(day)?|Sat(urday)?|Sun(day)?|M|Tu|W|Th|F|Sa|Su|Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday)))?\s+" +
            @"(Closed|(([0-9]{1,2})(\s*:\s*[0-5][0-9])?\s*(AM|PM)?\s*-\s*([0-9]{1,2})(\s*:\s*[0-5][0-9])?\s*(AM|PM)?)))*\s*$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture
        );
        public static string HoursRegex
        {
            get { return hoursRegex.ToString(); }    
        }


        private static bool IsHoursRangeValid(string hours)
        {
            var matches = Regex.Matches(hours, @"(\d{1,2}\s*:\s*\d{2}\s*(AM|PM)?)[\s\-]+(\d{1,2}\s*:\s*\d{2}\s*(AM|PM)?)", RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                var parts = match.Value.Split(new[] { '-', '–' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                    return false;

                if (!DateTime.TryParse(parts[0].Trim(), out DateTime start))
                    return false;

                if (!DateTime.TryParse(parts[1].Trim(), out DateTime end))
                    return false;

                if (start >= end)
                    return false;
            }

            return true;
        }


        public static bool Validate(string hours)
        {
            
            return hoursRegex.Match(hours).Length != 0 && IsHoursRangeValid(hours);
        }
    }
}