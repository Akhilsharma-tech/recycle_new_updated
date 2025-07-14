using ElectronicRecyclers.One800Recycling.Domain.Entities;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class InformationVerificationGroupMap : DomainObjectMap<InformationVerificationGroup>
    {
        public InformationVerificationGroupMap()
        {
            Property(x => x.Name, m => m.Length(100));
        }
    }
}