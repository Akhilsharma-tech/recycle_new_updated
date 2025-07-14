using ElectronicRecyclers.One800Recycling.Domain.Entities;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class MaterialCategoryMap : DomainObjectMap<MaterialCategory>
    {
        public MaterialCategoryMap()
        {
            Property(x => x.Name, m => 
                {
                    m.Length(50);
                    m.Unique(true);
                });

            Property(x => x.Description, m =>m.Length(100));

            Property(x => x.IsEnabled);
        }
    }
}