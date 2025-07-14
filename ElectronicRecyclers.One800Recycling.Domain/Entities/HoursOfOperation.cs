using ElectronicRecyclers.One800Recycling.Domain.Common;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class HoursOfOperation : DomainObject
    {
        public virtual EnvironmentalOrganization Organization { get; set; }

        private DayOfWeek dayOfWeek;
        public virtual DayOfWeek DayOfWeek
        {
            get { return dayOfWeek; }
            set
            {
                dayOfWeek = value;
                Rank = (Int16) dayOfWeek;
            }
        }

        public virtual DateTime? OpenTime { get; set; }

        public virtual DateTime? CloseTime { get; set; }

        public virtual DateTime? AfterBreakOpenTime { get; set; }

        public virtual DateTime? AfterBreakCloseTime { get; set; }

        public virtual bool IsClosed { get; set; }

        public virtual Int16 Rank { get; protected set; }

        public HoursOfOperation() { }

        public HoursOfOperation(
            EnvironmentalOrganization organization,
            DayOfWeek dayOfWeek,
            DateTime? openTime,
            DateTime? closeTime,
            DateTime? afterBreakOpenTime,
            DateTime? afterBreakCloseTime,
            bool isClosed)
        {
            Organization = organization;
            DayOfWeek = dayOfWeek;
            OpenTime = openTime;
            CloseTime = closeTime;
            AfterBreakOpenTime = afterBreakOpenTime;
            AfterBreakCloseTime = afterBreakCloseTime;
            IsClosed = isClosed;
            Rank = (Int16) dayOfWeek;
        }
    }
}