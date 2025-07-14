using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Type;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class HoursOfOperationMap : DomainObjectMap<HoursOfOperation>
    {
        public HoursOfOperationMap()
        {
            Property(x => x.DayOfWeek, m =>
                {
                    m.Length(10);
                    m.Type<EnumStringType<System.DayOfWeek>>();
                });

            Property(x => x.OpenTime, m =>
            {
                m.Type<TimeType>();
                m.NotNullable(false);
            });

            Property(x => x.CloseTime, m =>
            {
                m.Type<TimeType>();
                m.NotNullable(false);
            });

            Property(x => x.AfterBreakOpenTime, m =>
            {
                m.Type<TimeType>();
                m.NotNullable(false);
            });

            Property(x => x.AfterBreakCloseTime, m =>
            {
                m.Type<TimeType>();
                m.NotNullable(false);
            });

            Property(x => x.IsClosed);

            Property(x => x.Rank, m => m.Type<Int16Type>());

            ManyToOne(x => x.Organization, m => m.Column("EnvironmentalOrganizationId"));
        }
    }
}