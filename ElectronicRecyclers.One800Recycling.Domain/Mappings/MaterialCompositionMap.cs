using ElectronicRecyclers.One800Recycling.Domain.Entities;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class MaterialCompositionMap : DomainObjectMap<MaterialComposition>
    {
        public MaterialCompositionMap()
        {
            ManyToOne(x => x.Material);

            ManyToOne(x => x.MaterialComponent);

            Property(x => x.CompositionPercentage, m =>
            {
                m.Precision(5);
                m.Scale(2);
            });
        }
    }
}