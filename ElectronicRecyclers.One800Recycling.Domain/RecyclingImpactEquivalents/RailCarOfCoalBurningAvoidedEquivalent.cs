using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class RailCarOfCoalBurningAvoidedEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        //http://www.epa.gov/cleanenergy/energy-resources/refs.html
        //186.50 metric tons CO2/railcar to pounds 411,162.12
        public const decimal RailCarOfCoalBurningCarbonDixiodeEmissionsInPounds = 411162.12M;

        public RailCarOfCoalBurningAvoidedEquivalent(Material material, decimal weight)
            : base(new RecyclingClimateChangeImpact(material, weight)) { }

        public override MeasurementUnit ResultMeasurementUnit
        {
            get { return MeasurementUnit.RailCar; }
        }

        public override string Description
        {
            get { return "Rail cars of coal burning avoided"; }
        }

        protected override decimal Calculate()
        {
            return Math.Abs(Math.Round(
                RecyclingImpact.GetNetImpact() / RailCarOfCoalBurningCarbonDixiodeEmissionsInPounds, 
                2));
        }
    }
}