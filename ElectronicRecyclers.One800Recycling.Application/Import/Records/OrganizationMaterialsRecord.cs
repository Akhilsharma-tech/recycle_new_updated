using FileHelpers;


namespace ElectronicRecyclers.One800Recycling.Application.Import.Records
{
    [DelimitedRecord("\t"), IgnoreFirst, IgnoreEmptyLines]
    public class OrganizationMaterialsRecord
    {
        public long OrganizationId;

        public string MaterialId;

        [FieldOptional]
        public string MaterialName;
        [FieldOptional]
        public string MaterialBusinessDeliveryOption;
        [FieldOptional]
        public string MaterialResidentialDeliveryOption;
    }
}