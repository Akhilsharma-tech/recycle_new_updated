using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Type;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class NoteMap : DomainObjectMap<Note>
    {
        public NoteMap()
        {
            Property(x => x.Text, m => m.Length(4002));

            Property(x => x.AccessLevel, m =>
                {
                    m.Length(10);
                    m.Type<EnumStringType<AccessLevel>>();
                });
        }
    }
}