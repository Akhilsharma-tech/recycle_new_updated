using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Mapping.ByCode.Conformist;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class GovernmentAgencyOfWaterQualityMap : JoinedSubclassMapping<GovernmentAgencyOfWaterQuality>
    {
        public GovernmentAgencyOfWaterQualityMap() 
        {
            Key(x => x.Column("Id"));
        }
    }
}