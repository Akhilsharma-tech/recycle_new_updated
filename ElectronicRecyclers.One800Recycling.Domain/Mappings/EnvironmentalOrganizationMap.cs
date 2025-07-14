using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Mapping.ByCode;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class EnvironmentalOrganizationMap : DomainObjectMap<EnvironmentalOrganization>
    {
        public EnvironmentalOrganizationMap()
        {
            Property(x => x.Name, m => m.Length(160));

            Property(x => x.Description, m =>
                {
                    m.Length(300); 
                    m.NotNullable(false);
                });

            Component(x => x.Telephone, m => PhoneMapper.Map(
                m,
                "TelephoneCountryCodeSource",
                "TelephoneCountryCode",
                "TelephoneNumber",
                "TelephoneExtension"));

            Component(x => x.Fax, m => PhoneMapper.Map(
                m,
                "FaxCountryCodeSource",
                "FaxCountryCode",
                "FaxNumber",
                "FaxExtension"));

            Component(x => x.Address, m => 
                {
                    m.Property(a => a.AddressLine1, map => map.Length(200));
                    m.Property(a => a.AddressLine2, map =>
                    {
                        map.Length(200);
                        map.NotNullable(false);
                    });
                    m.Property(a => a.City, map => map.Length(100));
                    m.Property(a => a.Region, map =>
                    {
                        map.Length(100);
                        map.NotNullable(false);
                    });
                    m.Property(a => a.State, map =>
                    {
                        map.Length(20);
                        map.NotNullable(false);
                    });
                    m.Property(a => a.PostalCode, map => map.Length(20));
                    m.Property(a => a.Country, map => map.Length(20));
                    m.Property(a => a.Latitude);
                    m.Property(a => a.Longitude);
                });

            Property(x => x.IsMailingAddress);

            Property(x => x.LogoImageUrl, m =>
                {
                    m.Length(100);
                    m.NotNullable(false);
                });

            Property(x => x.WebsiteUrl, m =>
                {
                    m.Length(200); 
                    m.NotNullable(false);
                });

            Property(x => x.ImportBatchName, m =>
            {
                m.Length(100);
                m.NotNullable(false);
            });

            Property(x => x.IsEnabled);

            Bag<Note>("notes", m =>
                {
                    m.Table("EnvironmentalOrganizationNote");
                    m.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    m.Key(k => k.Column("EnvironmentalOrganizationId"));
                    m.Lazy(CollectionLazy.Lazy);
                },
                r => r.ManyToMany(c => c.Column("NoteId")));

            Bag<EnvironmentalOrganizationMaterial>("materials", m =>
                {
                    m.Table("EnvironmentalOrganizationMaterial");
                    m.Key(k => k.Column("EnvironmentalOrganizationId"));
                    m.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    m.Lazy(CollectionLazy.Lazy);
                    m.Inverse(true);
                },
                r => r.OneToMany());

            Bag<HoursOfOperation>("hoursOfOperations", m => 
                {
                    m.Key(k => k.Column("EnvironmentalOrganizationId"));
                    m.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    m.Lazy(CollectionLazy.Lazy);
                    m.Inverse(true);
                },
                r => r.OneToMany());

            Bag<InformationVerificationGroup>("verificationGroups", m =>
            {
                m.Table("EnvironmentalOrganizationInformationVerificationGroup");
                m.Cascade(Cascade.Persist | Cascade.Detach);
                m.Key(k => k.Column("EnvironmentalOrganizationId"));
                m.Lazy(CollectionLazy.Lazy);
            },
           r => r.ManyToMany(c => c.Column("InformationVerificationGroupId")));
        }
    }
}