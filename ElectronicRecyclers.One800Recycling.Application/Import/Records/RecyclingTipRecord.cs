using FileHelpers;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Records
{
    [DelimitedRecord("\t"), IgnoreEmptyLines, IgnoreFirst]
    public class RecyclingTipRecord
    {
        [FieldTrim(TrimMode.Both, '"')]
        public string Title;

        [FieldOptional, FieldTrim(TrimMode.Both, '"')]
        public string Description;

        [FieldOptional, FieldTrim(TrimMode.Both, '"')]
        public string ImageName;
    }
}