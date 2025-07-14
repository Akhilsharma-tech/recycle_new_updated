using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class MunicipalAgency : EnvironmentalOrganization
    {
        public MunicipalAgency() { }

        public MunicipalAgency(MunicipalAgency agency) : base(agency) { }

        public override object Copy()
        {
            return new MunicipalAgency(this);
        }
    }
}