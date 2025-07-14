using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class FormatHoursOfOperations 
    {
        private readonly Dictionary<string, string> militaryTimes = new Dictionary<string, string>
        {
            {"1", "13"},
            {"2", "14"},
            {"3", "15"},
            {"4", "16"},
            {"5", "17"},
            {"6", "18"},
            {"7", "19"},
            {"8", "20"},
            {"9", "21"},
            {"10", "22"},
            {"11", "23"},
            {"12", "24"}
        }; 

        public  IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            foreach (var row in rows)
            {
                var hours = row["HoursOfOperation"] as string;

                if(string.IsNullOrWhiteSpace(hours))
                    continue;

                hours = Regex.Replace(hours, @"\s+", " ");
                hours = Regex.Replace(hours, @"Sunday|\bSu\b|\bSun\b", "Sun", RegexOptions.IgnoreCase);
                hours = Regex.Replace(hours, @"Monday|Monady|\bM\b|\bMo\b|\bMon\b", "Mon", RegexOptions.IgnoreCase);
                hours = Regex.Replace(hours, @"Tuesday|\bT\b|\bTu\b|\bTue\b|\bTues\b", "Tue", RegexOptions.IgnoreCase);
                hours = Regex.Replace(hours, @"Wednesday|\bW\b|\bWe\b|\bWed\b", "Wed", RegexOptions.IgnoreCase);
                hours = Regex.Replace(hours, @"Thursday|\bThur\b|\bThurs\b", "Thu", RegexOptions.IgnoreCase);
                hours = Regex.Replace(hours, @"Friday|\bF\b|\bFr\b|\bFri\b|\bFro\b", "Fri", RegexOptions.IgnoreCase);
                hours = Regex.Replace(hours, @"Saturday|\bSa\b|\bSats\b", "Sat", RegexOptions.IgnoreCase);
                hours = Regex.Replace(hours, @";", ",");
                hours = Regex.Replace(hours, @"(\w{3})(:)", "$1");
                hours = Regex.Replace(hours, @"(\s\d+)(-)", "$1:00-");
                hours = Regex.Replace(hours, @"(-\d+)(,)", "$1:00,");
                hours = Regex.Replace(hours, @"(-\d+$)", "$1:00");

                foreach (var matchStr in from object match in Regex.Matches(hours, @"(-\d+:\d+)") select match.ToString())
                {
                    var numbers = matchStr
                        .Replace("-", "")
                        .Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                    if (numbers.Length != 2 || militaryTimes.ContainsKey(numbers[0]) == false)
                        continue;

                    hours = Regex.Replace(
                        hours, 
                        matchStr, 
                        string.Format("-{0}:{1}",  militaryTimes[numbers[0]], numbers[1]), 
                        RegexOptions.IgnoreCase);
                }

                row["HoursOfOperation"] = hours;

                yield return row;
            }
        }
    }
}