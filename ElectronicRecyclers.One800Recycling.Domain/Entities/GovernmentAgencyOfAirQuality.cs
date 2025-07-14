using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class GovernmentAgencyOfAirQuality : EnvironmentalOrganization
    {
        public GovernmentAgencyOfAirQuality() { }

        public GovernmentAgencyOfAirQuality(GovernmentAgencyOfAirQuality agency) 
            : base(agency) { }

        public override object Copy()
        {
            return new GovernmentAgencyOfAirQuality(this);
        }
    }
}