using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class DeforestationEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        //http://www.epa.gov/cleanenergy/energy-resources/refs.html#deforestation
        //-129.51 metric tons CO2/acre/year not emitted when acre of forest preserved
        //converted to pounds 85.98
        public const decimal CarbonDixiodeNotEmittedWhenAnAcreOfForestPreservedInPounds = -285520.68M;

        public DeforestationEquivalent(Material material, decimal weight)
            : base(new RecyclingClimateChangeImpact(material, weight)) { }

        public override MeasurementUnit ResultMeasurementUnit
        {
            get { return MeasurementUnit.Acre; }
        }

        public override string Description
        {
            get { return "Acres of U.S. forest preserved in one year"; }
        }

        protected override decimal Calculate()
        {
            return Math.Abs(Math.Round(
                RecyclingImpact.GetNetImpact() / CarbonDixiodeNotEmittedWhenAnAcreOfForestPreservedInPounds, 
                2));
        }
    }
}