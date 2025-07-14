using ElectronicRecyclers.One800Recycling.Domain.Mappings.Types;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using NHibernate.Mapping.ByCode;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class PhoneMapper
    {
        public static void Map(
            IComponentMapper<Phone> mapper, 
            string columnCountrySource,
            string columnCountryCode,
            string columnNumber,
            string columnExtension)
        {
            mapper.Property(p => p.CountryCodeSource, map =>
            {
                map.Column(columnCountrySource);
                map.Length(20);
                map.NotNullable(false);
            });

            mapper.Property(p => p.CountryCode, map =>
            {
                map.Column(columnCountryCode);
                map.NotNullable(false);
            });

            mapper.Property(p => p.Number, map =>
            {
                map.Column(columnNumber);
                map.Type<ULongAsLongType>();
                map.NotNullable(false);
            });

            mapper.Property(p => p.Extension, map =>
            {
                map.Column(columnExtension);
                map.Length(20);
                map.NotNullable(false);
            });
        }
    }
}