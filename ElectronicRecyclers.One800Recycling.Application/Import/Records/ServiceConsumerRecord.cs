using FileHelpers;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Records
{
    [DelimitedRecord("\t"), IgnoreEmptyLines, IgnoreFirst]
    public class ServiceConsumerRecord
    {
        [FieldTrim(TrimMode.Both, '"')]
        public string AuthorizationCode;

        [FieldOptional, FieldTrim(TrimMode.Both, '"')]
        public string WebsiteUrl;

        [FieldOptional, FieldTrim(TrimMode.Both, '"')]
        public string Email;
    }
}