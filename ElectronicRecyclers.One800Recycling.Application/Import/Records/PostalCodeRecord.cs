using FileHelpers;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Records
{
    [DelimitedRecord("\t"), IgnoreFirst]
    public class PostalCodeRecord
    {
        [FieldTrim(TrimMode.Both)]
        public string PostalCode;

        [FieldTrim(TrimMode.Both)]
        public string City;

        [FieldTrim(TrimMode.Both)]
        public string Region;

        [FieldTrim(TrimMode.Both)]
        public string State;

        [FieldTrim(TrimMode.Both)]
        public string Country;

        [FieldTrim(TrimMode.Both)]
        public string Latitude;

        [FieldTrim(TrimMode.Both)]
        public string Longitude;

    }
}