using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Mapping.ByCode;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class EnvironmentalOrganizationMaterialMap : DomainObjectMap<EnvironmentalOrganizationMaterial>
    {
        public EnvironmentalOrganizationMaterialMap()
        {
            ManyToOne(x => x.Material);

            ManyToOne(x => x.Organization);

            Bag<MaterialDelivery>("materialDeliveries", m =>
                {
                    m.Key(k =>
                        {
                            k.Column("EnvironmentalOrganizationMaterialId");
                            k.NotNullable(true);
                        });
                    m.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    m.Lazy(CollectionLazy.NoLazy);
                },
                r => r.OneToMany());
        }
    }
}