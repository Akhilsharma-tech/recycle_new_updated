using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Type;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class DismantlingProcessMap : DomainObjectMap<DismantlingProcess>
    {
        public DismantlingProcessMap()
        {
            Property(x => x.Name, m => m.Length(200));

            Property(x => x.LossPercentageDuringRecycling, map =>
            {
                map.Precision(5);
                map.Scale(2);
            });

            Property(x => x.Type, m =>
            {
                m.Length(20);
                m.Type<EnumStringType<DismantlingProcessType>>();
            });

            Component(x => x.EnvironmentalImpact, m =>
            {
                m.Property(i => i.ClimateChangeImpact, map =>
                {
                    map.Precision(28);
                    map.Scale(20);
                });

                m.Property(i => i.ResourceDepletionImpact, map =>
                {
                    map.Precision(28);
                    map.Scale(20);
                });

                m.Property(i => i.WaterWithdrawalImpact, map =>
                {
                    map.Precision(28);
                    map.Scale(20);
                });
            });
        }
    }
}