using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ElectronicRecyclers.One800Recycling.Domain.Common.Helper
{
    public class HoursOfOperationParser
    {
        private readonly List<Tuple<Regex, Action<HoursOfOperation, MatchCollection>>> actions = 
            new List<Tuple<Regex, Action<HoursOfOperation, MatchCollection>>>();

        private readonly Dictionary<string, DayOfWeek> daysOfWeek = new Dictionary<string, DayOfWeek>
        {
            { "Sun", DayOfWeek.Sunday },
            { "Mon", DayOfWeek.Monday },
            { "Tue", DayOfWeek.Tuesday },
            { "Wed", DayOfWeek.Wednesday },
            { "Thu", DayOfWeek.Thursday },
            { "Fri", DayOfWeek.Friday },
            { "Sat", DayOfWeek.Saturday }
        };

        private static readonly Dictionary<string, DateTime?> businessHours = 
            new Dictionary<string, DateTime?>();  

        private static readonly Dictionary<string, HoursOfOperation> hoursOfOperations = 
            new Dictionary<string, HoursOfOperation>(); 

        private static DateTime? ParseTime(string time)
        {
            if (businessHours.ContainsKey(time))
                return businessHours[time];

            DateTime result;
            if (DateTime.TryParse(time, out result) == false)
                return null;

            businessHours.Add(time, result);
            return result;
        }

        private void Add(string regex, Action<HoursOfOperation, MatchCollection> action)
        {
            var key = new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            actions.Add(Tuple.Create(key, action));
        }

        public HoursOfOperationParser()
        {
            Add(@"^Mon|Tue|Wed|Thu|Fri|Sat|Sun", (hours, collection) =>
            {
                if (collection.Count == 0) return;

                hours.DayOfWeek = daysOfWeek[collection[0].Value];
            });

            Add(@"((2[0-3]|[01]?[0-9]):([0-5]?[0-9]))+", (hours, collection) =>
            {
                if (collection.Count < 2) return;

                hours.OpenTime = ParseTime(collection[0].Value);
                hours.CloseTime = ParseTime(collection[1].Value);

                if (collection.Count < 4) return;

                hours.AfterBreakOpenTime = ParseTime(collection[2].Value);
                hours.AfterBreakCloseTime = ParseTime(collection[3].Value);
            });

            Add(@"\sClosed", (hours, collection) =>
            {
                if (collection.Count > 0)
                    hours.IsClosed = true;
            });
        }

        private static string[] Split(string text, string delimiter)
        {
            return text
                .Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries)
                .Where(parts => parts.Trim().Length > 0)
                .ToArray();
        }

        private IEnumerable<string> SplitIntoDaysOfWeek(string text)
        {
            var strings = Split(text, " ");

            var days = Split(strings[0], "-");
            if (days.Count() > 1)
            {
                var hours = (strings.Count() == 3)
                    ? string.Format("{0} {1}", strings[1], strings[2])
                    : strings[1];

                var startDayOfWeek = days[0].ToTitleCase();
                var endDayOfWeek = days[1].ToTitleCase();

                if (daysOfWeek.ContainsKey(startDayOfWeek) == false || 
                    daysOfWeek.ContainsKey(endDayOfWeek) == false) 
                    yield break;

                var startIndex = (int)daysOfWeek[startDayOfWeek];
                var endIndex = (int)daysOfWeek[endDayOfWeek];

                for (var i = startIndex; i <= endIndex; i++)
                    yield return string.Format(
                        "{0} {1}",
                        Enum.GetValues(typeof(DayOfWeek)).GetValue(i),
                        hours);
            }
            else
            {
                yield return text;
            }
        }

        public IEnumerable<HoursOfOperation> Parse(string text)
        {
            if (HoursOfOperationValidator.Validate(text) == false)
               yield break;

            foreach (var hoursStr in Split(text, ",").SelectMany(SplitIntoDaysOfWeek))
            {
                if (hoursOfOperations.ContainsKey(hoursStr) == false)
                {
                    var hoursObj = new HoursOfOperation();
                    hoursOfOperations.Add(hoursStr, hoursObj);

                    foreach (var action in actions)
                    {
                        var collection = action.Item1.Matches(hoursStr);
                        try
                        {
                            action.Item2(hoursObj, collection);
                        }
                        catch (Exception ex)
                        {
                            //LogManager.GetLogger().Error("Error parsing hours of operation.", ex);
                            yield break;
                        }
                    }
                    yield return hoursObj;
                }
                else
                {
                    var item = hoursOfOperations[hoursStr];
                    yield return new HoursOfOperation
                    {
                        OpenTime = item.OpenTime,
                        CloseTime = item.CloseTime,
                        AfterBreakOpenTime = item.AfterBreakOpenTime,
                        AfterBreakCloseTime = item.AfterBreakCloseTime,
                        DayOfWeek = item.DayOfWeek,
                        IsClosed = item.IsClosed
                    };    
                }
            }
        }
    }
}