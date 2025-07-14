using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class ElectricityUsageAvoidedEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        //https://www.carbontrust.com/media/18223/ctl153_conversion_factors.pdf
        public const decimal GridElectricityCarbonDioxideEmissionPerKwhInPounds = 1.16M;

        public ElectricityUsageAvoidedEquivalent(Material material, decimal weight)
            : base(new RecyclingClimateChangeImpact(material, weight)) { }

        public override MeasurementUnit ResultMeasurementUnit
        {
            get { return MeasurementUnit.Kilowatt; }
        }

        public override string Description
        {
            get { return "Electricity usage avoided"; }
        }

        protected override decimal Calculate()
        {
            return Math.Abs(Math.Round(
                RecyclingImpact.GetNetImpact() / GridElectricityCarbonDioxideEmissionPerKwhInPounds, 
                2));
        }
    }
}