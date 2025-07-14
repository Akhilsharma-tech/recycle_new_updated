using System;
using System.Threading;

namespace ElectronicRecyclers.One800Recycling.Domain.Common
{
    [Serializable]
    public abstract class DomainObject : IDomainObject, IAuditable
    {
        public virtual int Id { get; protected set; }

        public virtual DateTimeOffset CreatedOn { get; protected set; }

        public virtual DateTimeOffset ModifiedOn { get; protected set; }

        public virtual string CreatedBy { get; protected set; }

        public virtual string ModifiedBy { get; protected set; }

        public virtual int Version { get; protected set; } //Used by NHibernate for versioning control

        protected DomainObject()
        {
            var userName = Thread.CurrentPrincipal.Identity.IsAuthenticated
                ? Thread.CurrentPrincipal.Identity.Name
                : "unknown";

            CreatedBy = userName;
            ModifiedBy = userName;
            CreatedOn = DateTimeOffset.Now;
            ModifiedOn = DateTimeOffset.Now;
        }
    }
}