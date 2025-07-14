using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class PostalCodeMap : DomainObjectMap<PostalCode>
    {
        public PostalCodeMap()
        {
            Property(x => x.Code, m =>
            {
                m.Length(20);
                m.Unique(true);
            });

            Property(x => x.Region, m => m.Length(40));
            Property(x => x.City, m => m.Length(40));
            Property(x => x.State, m =>
            {
                m.Length(20);
                m.NotNullable(false);
            });

            Property(x => x.Country, m => m.Length(20));
            Property(x => x.Latitude);
            Property(x => x.Longitude);
        }
    }
}