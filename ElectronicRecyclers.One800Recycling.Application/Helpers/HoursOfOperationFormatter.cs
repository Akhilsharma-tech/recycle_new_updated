using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.Helpers
{
    public static class HoursOfOperationFormatter
    {
        private static string FormatDayOfWeek(DayOfWeek day)
        {
            return day.ToString().Substring(0, 3);
        }

        private static string FormatTime(DateTime? dateTime)
        {
            return (dateTime.HasValue)
                ? dateTime.Value.ToString("h:mm tt")
                : string.Empty;
        }

        private static string FormatHours(HoursOfOperation startHours, HoursOfOperation endHours = null)
        {
            var strBuilderHours = new StringBuilder();
            strBuilderHours.Append(FormatDayOfWeek(startHours.DayOfWeek));

            if (endHours != null)
                strBuilderHours.Append(string.Format("-{0}", FormatDayOfWeek(endHours.DayOfWeek)));

            strBuilderHours.Append(startHours.IsClosed
                ? " Closed"
                : string.Format(
                            " {0}-{1}",
                            FormatTime(startHours.OpenTime),
                            FormatTime(startHours.CloseTime)));

            return strBuilderHours.ToString();
        }

        private static void AddFormattedHours<TKey>(
            ICollection<string> formattedHours,
            IEnumerable<IGrouping<TKey, HoursOfOperation>> hoursQuery)
        {
            hoursQuery.ForEach(groupedHours =>
            {
                var startHours = groupedHours.First();

                if (groupedHours.Count() == 1)
                {
                    formattedHours.Add(FormatHours(startHours));
                    return;
                }

                formattedHours.Add(FormatHours(startHours, groupedHours.Last()));

            });
        }

        public static string Format(IEnumerable<HoursOfOperation> hours)
        {
            var formattedHours = new Collection<string>();

            var openHoursQuery = hours
                .Where(h => h.OpenTime != null && h.CloseTime != null)
                .OrderBy(h => h.Rank)
                .GroupBy(h => new { h.OpenTime, h.CloseTime });

            var closedHoursQuery = hours
                .Where(h => h.IsClosed)
                .OrderBy(h => h.Rank)
                .GroupBy(h => h.IsClosed);

            AddFormattedHours<object>(formattedHours, openHoursQuery);
            AddFormattedHours(formattedHours, closedHoursQuery);

            return string.Join(", ", formattedHours);
        }
    }
}
