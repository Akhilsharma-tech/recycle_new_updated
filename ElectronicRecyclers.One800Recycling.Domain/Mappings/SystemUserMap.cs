using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Type;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class SystemUserMap : DomainObjectMap<SystemUser>
    {
        public SystemUserMap()
        {
            Component(x => x.Name, m =>
                {
                    m.Property(n => n.FirstName, map => map.Length(40));
                    m.Property(n => n.MiddleName, map => 
                        {
                            map.Length(40);
                            map.NotNullable(false);
                        });
                    m.Property(n => n.LastName, map => map.Length(60));
                });

            Property(x => x.Email, m => 
                {
                    m.Length(80);
                    m.Unique(true);
                });

            Property(x => x.HashedPassword, m => m.Length(100));

            Property(x => x.PasswordSalt, m => m.Length(80));

            Property(x => x.LastLoginOn, m => m.Type<DateTimeOffsetType>());

            Property(x => x.IsEnabled);

            Bag<Role>("roles", m => 
                {
                    m.Table("SystemUserRole");
                    m.Cascade(Cascade.None);
                    m.Key(k => k.Column("SystemUserId"));
                },
                r => r.ManyToMany(c => c.Column("RoleId")));
        }
    }
}