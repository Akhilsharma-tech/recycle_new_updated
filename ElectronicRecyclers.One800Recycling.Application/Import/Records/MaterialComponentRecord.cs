using FileHelpers;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Records
{
    [DelimitedRecord("\t"), IgnoreFirst]
    public class MaterialComponentRecord
    {
        [FieldTrim(TrimMode.Both, '"')]
        public string Name;

        [FieldTrim(TrimMode.Both, '"')]
        public string Description;

        [FieldTrim(TrimMode.Both, '"')]
        public string VirginProductionProcess;

        [FieldTrim(TrimMode.Both, '"')]
        public string RecyclingProcess;

        [FieldTrim(TrimMode.Both, '"')]
        public string LandfillingProcess;

        [FieldTrim(TrimMode.Both, '"')]
        public string IncinerationProcess;
    }
}