using FileHelpers;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Records
{
    [DelimitedRecord("\t"), IgnoreEmptyLines, IgnoreFirst]
    public class MaterialRecord
    {
        public string MaterialId;
        [FieldTrim(TrimMode.Both, '"')]
        public string Name;

        [FieldOptional, FieldTrim(TrimMode.Both, '"')]
        public string Description;

        [FieldOptional, FieldTrim(TrimMode.Both, '"')]
        public string SearchKeywords;

        [FieldOptional]
        public string Categories;

        [FieldOptional]
        public string IsActive;
    }
}