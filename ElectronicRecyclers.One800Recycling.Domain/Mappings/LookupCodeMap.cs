using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class LookupCodeMap<T> : DomainObjectMap<T> where T : LookupCode
    {
        protected LookupCodeMap()
        {
            Property(x => x.Name, m => m.Length(60));

            Property(x => x.Code, m =>
            {
                m.Length(20);
                m.Unique(true);
                
            });

            Property(x => x.Description, m => m.Length(160));
        }
    }
}