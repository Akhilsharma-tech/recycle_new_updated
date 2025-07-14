using ElectronicRecyclers.One800Recycling.Domain.Entities;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class MaterailProductDismantlingProcessMap 
        : DomainObjectMap<MaterialProductDismantlingProcess>
    {
        public MaterailProductDismantlingProcessMap()
        {
            ManyToOne(x => x.Material);

            ManyToOne(x => x.ProductDismantlingProcess);

            Property(x => x.CompositionPercentage, m =>
            {
                m.Precision(5);
                m.Scale(2);
            });
        }
    }
}