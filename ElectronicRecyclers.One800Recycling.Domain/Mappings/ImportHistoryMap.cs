using ElectronicRecyclers.One800Recycling.Domain.Entities;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class ImportHistoryMap : DomainObjectMap<ImportHistory>
    {
        public ImportHistoryMap()
        {
            Property(x => x.FileName, m => m.Length(100));
        }
    }
}