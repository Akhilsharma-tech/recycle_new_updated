using ElectronicRecyclers.One800Recycling.Domain.Entities;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class RoleMap : DomainObjectMap<Role>
    {
        public RoleMap()
        {
            Property(x => x.Name, m =>
                {
                    m.Length(40);
                    m.Unique(true);
                });

            Property(x => x.Description, m => m.Length(300));
        }
    }
}