using ElectronicRecyclers.One800Recycling.Domain.Entities;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class ProductDismantlingProcessMap : DomainObjectMap<ProductDismantlingProcess>
    {
        public ProductDismantlingProcessMap()
        {
            Property(x => x.ProductName, m => m.Length(100));

            ManyToOne(x => x.DismantlingProcess);
        }
    }
}