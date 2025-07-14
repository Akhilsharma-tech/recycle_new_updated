using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class GovernmentAgencyOfWaterQuality : EnvironmentalOrganization
    {
        public GovernmentAgencyOfWaterQuality() { }

        public GovernmentAgencyOfWaterQuality(GovernmentAgencyOfWaterQuality agency) 
            : base(agency) { }

        public override object Copy()
        {
            return new GovernmentAgencyOfWaterQuality(this);
        }
    }
}