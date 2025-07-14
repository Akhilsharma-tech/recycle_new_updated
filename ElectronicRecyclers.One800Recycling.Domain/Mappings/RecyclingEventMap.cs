using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Type;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class RecyclingEventMap : DomainObjectMap<RecyclingEvent>
    {
        public RecyclingEventMap()
        {
            Property(x => x.Subject, m => m.Length(100));

            Property(x => x.Description, m => m.Length(300));

            Property(x => x.StartOn, m => m.Type<DateTimeOffsetType>());

            Property(x => x.EndOn, m => m.Type<DateTimeOffsetType>());

            Component(x => x.Address, m =>
                {
                    m.Property(a => a.AddressLine1, map => map.Length(200));
                    m.Property(a => a.AddressLine2, map =>
                    {
                        map.Length(200);
                        map.NotNullable(false);
                    });
                    m.Property(a => a.City, map => map.Length(100));
                    m.Property(a => a.Region, map =>
                    {
                        map.Length(100);
                        map.NotNullable(false);
                    });
                    m.Property(a => a.State, map =>
                    {
                        map.Length(40);
                        map.NotNullable(false);
                    });
                    m.Property(a => a.PostalCode, map => map.Length(20));
                    m.Property(a => a.Country, map => map.Length(60));
                    m.Property(a => a.Latitude);
                    m.Property(a => a.Longitude);
                });

            Property(x => x.WebsiteUrl, m =>
                {
                    m.Length(200);
                    m.NotNullable(false);
                });

            Property(x => x.IsMailingAddress);

            Bag<Material>("materials", m =>
                {
                    m.Table("RecyclingEventMaterial");
                    m.Cascade(Cascade.None);
                    m.Lazy(CollectionLazy.Lazy);
                    m.Key(k => k.Column("RecyclingEventId"));
                },
                r => r.ManyToMany(c => c.Column("MaterialId")));
        }
    }
}