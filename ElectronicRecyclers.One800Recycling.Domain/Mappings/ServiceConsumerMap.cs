using ElectronicRecyclers.One800Recycling.Domain.Entities;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class ServiceConsumerMap : DomainObjectMap<ServiceConsumer>
    {
        public ServiceConsumerMap()
        {
            Property(x => x.AuthorizationCode, m =>
                {
                    m.Length(50);
                    m.Unique(true);
                });

            Property(x => x.WebsiteUrl, m => m.Length(255));

            Property(x => x.Email, m =>
                {
                    m.Length(80);
                    m.Unique(true);
                });
        }
    }
}