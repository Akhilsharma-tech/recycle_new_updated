using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class PropaneCylinderUsageAvoidedEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        //http://www.epa.gov/cleanenergy/energy-resources/refs.html
        //0.024 metric tons CO2/cylinder in pounds 52.91
        public const decimal RailCarOfCoalBurningCarbonDixiodeEmissionsInPounds = 52.91M;

        public PropaneCylinderUsageAvoidedEquivalent(Material material, decimal weight)
            : base(new RecyclingClimateChangeImpact(material, weight)) { }

        public override MeasurementUnit ResultMeasurementUnit
        {
            get { return MeasurementUnit.PropaneCylinder; }
        }

        public override string Description
        {
            get { return "Home barbecue propane cylinders usage avoided"; }
        }

        protected override decimal Calculate()
        {
            return Math.Abs(Math.Round(
                RecyclingImpact.GetNetImpact() / RailCarOfCoalBurningCarbonDixiodeEmissionsInPounds, 
                2));
        }
    }
}