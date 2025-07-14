using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class CarRemovalEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        //http://www.epa.gov/cleanenergy/energy-resources/refs.html
        //Average vehicle miles traveled in 2011 was 11,318 miles per year.
        //11,318 mile x 0.663475281 CO2 per mile in lbs. 
        public const decimal CarCarbonDioxideEmissionPerYearInPounds = 7509.21M;

        public CarRemovalEquivalent(Material material, decimal weight)
            : base(new RecyclingClimateChangeImpact(material, weight)) { }

        public override MeasurementUnit ResultMeasurementUnit
        {
            get { return MeasurementUnit.Car; }
        }

        public override string Description
        {
            get { return "Passenger cars removed from the road in one year"; }
        }

        protected override decimal Calculate()
        {
            return Math.Abs(Math.Round(
                RecyclingImpact.GetNetImpact()/CarCarbonDioxideEmissionPerYearInPounds, 
                2));
        }
    }
}