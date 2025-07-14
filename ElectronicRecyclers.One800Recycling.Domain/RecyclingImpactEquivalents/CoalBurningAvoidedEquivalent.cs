using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class CoalBurningAvoidedEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
         //http://www.epa.gov/cleanenergy/energy-resources/refs.html
         //0.000931 metric tons CO2/pound of coal converted to pounds 2.0525
         public const decimal CoalCarbonDioxideEmissionPerPound = 2.0525M;

         public CoalBurningAvoidedEquivalent(Material material, decimal weight)
            : base(new RecyclingClimateChangeImpact(material, weight)) { }

        public override MeasurementUnit ResultMeasurementUnit
        {
            get { return MeasurementUnit.Pound; }
        }

        public override string Description
        {
            get { return "Pounds of coal burning avoided"; }
        }

        protected override decimal Calculate()
        {
            return Math.Abs(Math.Round(
                RecyclingImpact.GetNetImpact() / CoalCarbonDioxideEmissionPerPound, 
                2));
        }
    }
}