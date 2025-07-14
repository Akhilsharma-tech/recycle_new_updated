using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class AuditEntry
    {
        public virtual int Id { get; set; }

        public virtual string EntityName { get; set; }

        public virtual int EntityId { get; set; }

        public virtual string Changes { get; set; }

        public virtual AuditEventType Action { get; set; }

        public virtual DateTimeOffset ChangedOn { get; set; }

        public virtual string ChangedBy { get; set; }
    }
}