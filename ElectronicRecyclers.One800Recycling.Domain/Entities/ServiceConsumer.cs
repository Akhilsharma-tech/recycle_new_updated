using ElectronicRecyclers.One800Recycling.Domain.Common;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class ServiceConsumer : DomainObject
    {
        protected ServiceConsumer()
        {
        }

        public ServiceConsumer(string email, string websiteUrl)
        {
            Email = email;
            WebsiteUrl = websiteUrl;
            AuthorizationCode = Guid.NewGuid().ToString("N");
        }

        public virtual string AuthorizationCode { get; protected set; }

        public virtual string WebsiteUrl { get; set; }

        public virtual string Email { get; set; }
    }
}