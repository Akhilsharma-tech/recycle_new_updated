using ElectronicRecyclers.One800Recycling.Domain.Common;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class DomainObjectMap<TDomainObject> : ClassMapping<TDomainObject> 
        where TDomainObject : DomainObject
    {
        protected DomainObjectMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.HighLow));

            Property(x => x.CreatedBy, m => m.Length(80));

            Property(x => x.ModifiedBy, m => m.Length(80));

            Property(x => x.CreatedOn, m => m.Type<DateTimeOffsetType>());

            Property(x => x.ModifiedOn, m => m.Type<DateTimeOffsetType>());

            Version(x => x.Version, m =>
                {
                    m.Generated(VersionGeneration.Never);
                    m.UnsavedValue(0);
                    m.Type(new Int32Type());
                });
        }
    }
}