using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class NaturalGasBurningAvoidedEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        //http://www.epa.gov/cleanenergy/energy-resources/refs.html
        //0.005302 metric tons CO2/therm burned as a fuel converted
        //to pounds 11.69
        public const decimal NaturalGasCarbonDioxideEmissionInPoundsPerTherm = 11.69M;

        public NaturalGasBurningAvoidedEquivalent(Material material, decimal weight)
            : base(new RecyclingClimateChangeImpact(material, weight)) { }

        public override MeasurementUnit ResultMeasurementUnit
        {
            get { return MeasurementUnit.Therm; }
        }

        public override string Description
        {
            get { return "Natural gas bruning as a fuel avoided"; }
        }

        protected override decimal Calculate()
        {
            return Math.Abs(Math.Round(
                RecyclingImpact.GetNetImpact()/NaturalGasCarbonDioxideEmissionInPoundsPerTherm, 
                2));
        }
    }
}