using ElectronicRecyclers.One800Recycling.Domain.Entities;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class EnvironmentalOrganizationImportMap 
        : DomainObjectMap<EnvironmentalOrganizationImport>
    {
        public EnvironmentalOrganizationImportMap()
        {
            Property(x => x.Name, m =>
                {
                    m.Length(160);
                    m.NotNullable(false);
                });

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

            Property(x => x.Type, m => m.Length(60));

            Property(x => x.ImportBatchName, m => m.Length(4002));

            Property(x => x.PrivateNote, m =>
            {
                m.Length(4002);
                m.NotNullable(false);
            });

            Property(x => x.PublicNote, m =>
            {
                m.Length(4002);
                m.NotNullable(false);
            });

            Property(x => x.HoursOfOperation, m =>
            {
                m.Length(4002);
                m.NotNullable(false);
            });

            Property(x => x.MaterialResidentialDeliveryOption, m =>
            {
                m.Length(4002);
                m.NotNullable(false);
            });

            Property(x => x.MaterialBusinessDeliveryOption, m =>
            {
                m.Length(4002);
                m.NotNullable(false);
            });

            Property(x => x.IsDedicatedRecycler);

            Property(x => x.IsNameValid);

            Property(x => x.IsTelephoneValid);

            Property(x => x.IsFaxValid);

            Property(x => x.IsWebsiteUrlValid);

            Property(x => x.IsHoursOfOperationValid);

            Property(x => x.IsAddressValid);

            Property(x => x.IsMailingAddress);

            Property(x => x.IsDuplicate);

            Property(x => x.IsDuplicateOrganizationFoundDuringMoveOperation);

            Component(x => x.Address, m =>
                {
                    m.Property(a => a.AddressLine1, map =>
                    {
                        map.Length(200);
                        map.NotNullable(false);
                    });
                    m.Property(a => a.AddressLine2, map =>
                    {
                        map.Length(200);
                        map.NotNullable(false);
                    });
                    m.Property(a => a.City, map =>
                    {
                        map.Length(100);
                        map.NotNullable(false);
                    });
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
                    m.Property(a => a.PostalCode, map =>
                    {
                        map.Length(20);
                        map.NotNullable(false);
                    });
                    m.Property(a => a.Country, map =>
                    {
                        map.Length(20);
                        map.NotNullable(false);
                    });
                    m.Property(a => a.Latitude);
                    m.Property(a => a.Longitude);

                    Property(x => x.EnvironmentalOrganizationId, map =>
                    {
                        map.NotNullable(false);
                    });

                    Property(x => x.IsEnabled);


                });
        }
    }
}