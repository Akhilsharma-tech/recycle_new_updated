using ElectronicRecyclers.One800Recycling.Domain.Entities;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class RecyclingTipMap : DomainObjectMap<RecyclingTip>
    {
         public RecyclingTipMap()
        {
            Property(x => x.Title, m => m.Length(100));

            Property(x => x.Description, m => m.Length(400));

            Property(x => x.ImageName, m => m.Length(100));

            Property(x => x.Number);
        }
    }
}