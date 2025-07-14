using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Mapping.ByCode.Conformist;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class RecyclerMap : JoinedSubclassMapping<Recycler>
    {
        public RecyclerMap()
        {
            Key(k => k.Column("Id"));

            Property(x => x.IsDedicatedRecycler);
        }
    }
}