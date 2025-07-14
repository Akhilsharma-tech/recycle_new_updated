using FileHelpers;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Records
{
    [DelimitedRecord("\t"), IgnoreEmptyLines, IgnoreFirst]
    public class LookupCodeRecord
    {
        [FieldTrim(TrimMode.Both, '"')] 
        public string Name;

        [FieldTrim(TrimMode.Both, '"')] 
        public string Code;

        [FieldTrim(TrimMode.Both, '"')] 
        public string Description;

    }
}