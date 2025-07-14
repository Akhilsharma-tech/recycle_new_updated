using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Type;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class MaterialDeliveryMap : DomainObjectMap<MaterialDelivery>
    {
        public MaterialDeliveryMap()
        {
            Property(x => x.DeliveryType, m =>
                {
                    m.Length(20);
                    m.Type<EnumStringType<MaterialDeliveryType>>();
                });

            Property(x => x.IsBusinessDelivery);
        }
    }
}