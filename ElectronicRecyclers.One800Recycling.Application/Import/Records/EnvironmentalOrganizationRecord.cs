using FileHelpers;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Records
{
    [DelimitedRecord("\t"), IgnoreFirst, IgnoreEmptyLines]
    public class EnvironmentalOrganizationRecord
    {
        public long? EnvironmentalOrganizationId;
        [FieldTrim(TrimMode.Both, '"')]
        public string Name;

        [FieldTrim(TrimMode.Both, '"')]
        public string Description;

        [FieldTrim(TrimMode.Both, '"')]
        public string AddressLine1;

        [FieldTrim(TrimMode.Both)] 
        public string AddressLine2;

        [FieldTrim(TrimMode.Both, '"')]
        public string City;

        [FieldTrim(TrimMode.Both, '"')]
        public string Region;

        [FieldTrim(TrimMode.Both)]
        public string State;

        [FieldTrim(TrimMode.Both)]
        public string PostalCode;

        [FieldTrim(TrimMode.Both)]
        public string Country;

        [FieldTrim(TrimMode.Both)]
        public string Telephone;

        [FieldTrim(TrimMode.Both)]
        public string Fax;

        [FieldTrim(TrimMode.Both)]
        public string WebsiteUrl;

        [FieldTrim(TrimMode.Both, '"')]
        public string HoursOfOperation;

        [FieldTrim(TrimMode.Both, '"')]
        public string IsEnabled;

        [FieldOptional]
        [FieldTrim(TrimMode.Both, '"')]
        public string PrivateNote;
         [FieldOptional]
        [FieldTrim(TrimMode.Both, '"')]
        public string PublicNote;      
    }   
}