

using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class AuditEntryMap : ClassMapping<AuditEntry>
    {
        public AuditEntryMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.HighLow));

            Property(x => x.EntityName, m => m.Length(80));

            Property(x => x.EntityId);

            Property(x => x.Changes, m => m.Type(NHibernateUtil.StringClob));

            Property(x => x.Action, m =>
                {
                    m.Length(10);
                    m.Type<EnumStringType<AuditEventType>>();
                });

            Property(x => x.ChangedOn, m => m.Type<DateTimeOffsetType>());

            Property(x => x.ChangedBy, m => m.Length(80));
        }
    }
}