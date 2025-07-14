using FileHelpers;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Records
{
    [DelimitedRecord("\t"), IgnoreEmptyLines, IgnoreFirst]
    public class MaterialCategoryRecord
    {
        [FieldTrim(TrimMode.Both, '"')]
        public string Name;

        [FieldOptional, FieldTrim(TrimMode.Both, '"')]
        public string Description;
    }
}