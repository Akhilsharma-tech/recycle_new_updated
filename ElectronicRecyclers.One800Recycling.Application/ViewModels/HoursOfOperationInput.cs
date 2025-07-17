using ElectronicRecyclers.One800Recycling.Application.Helpers.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public class HoursOfOperationInput
    {
        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public int OrganizationId { get; set; }

        [Display(Name = "Week Days")]
        public IEnumerable<string> WeekDays
        {
            get { return Enum.GetNames(typeof(DayOfWeek)); }
        }

        public IEnumerable<string> Hours
        {
            get
            {
                var startDate = DateTime.MinValue.AddHours(6);
                var endDate = DateTime.MinValue.AddDays(1).AddHours(6);
                while (startDate < endDate)
                {
                    yield return startDate.ToShortTimeString();
                    startDate = startDate.AddMinutes(15);
                }
            }
        }

        public bool IsClosed { get; set; }

        public IDictionary<string, bool> SelectedWeekDays { get; set; }

        private string selectedOpenTime;

        [RequiredIf("IsClosed", "false", ErrorMessage = "Open time is required.")]
        [Display(Name = "Open Time")]
        public string SelectedOpenTime
        {
            get
            {
                return string.IsNullOrEmpty(selectedOpenTime)
                    ? "9:00 AM"
                    : selectedOpenTime;
            }
            set { selectedOpenTime = value; }
        }

        private string selectedCloseTime;

        [RequiredIf("IsClosed", "false", ErrorMessage = "Close time is required.")]
        [Display(Name = "Close Time")]
        public string SelectedCloseTime
        {
            get
            {
                return string.IsNullOrEmpty(selectedCloseTime)
                    ? "5:00 PM"
                    : selectedCloseTime;
            }
            set { selectedCloseTime = value; }
        }

        [RequiredIfNot("SelectedAfterBreakCloseTime", "", ErrorMessage = "Open time is required.")]
        public string SelectedAfterBreakOpenTime { get; set; }

        [RequiredIfNot("SelectedAfterBreakOpenTime", "", ErrorMessage = "Close time is required.")]
        public string SelectedAfterBreakCloseTime { get; set; }

        public bool IsNewHoursOfOperation()
        {
            return Id == 0;
        }
    }
}
