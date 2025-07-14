using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Mapping.ByCode.Conformist;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class GovernmentAgencyOfAirQualityMap : JoinedSubclassMapping<GovernmentAgencyOfAirQuality>
    {
        public GovernmentAgencyOfAirQualityMap() 
        {
            Key(x => x.Column("Id"));
        }
    }
}