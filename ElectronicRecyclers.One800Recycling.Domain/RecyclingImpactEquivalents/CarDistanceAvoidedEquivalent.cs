using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class CarDistanceAvoidedEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        public const decimal CarCarbonDioxideEmissionPerMileInPounds = 0.663475281M;

        public CarDistanceAvoidedEquivalent(Material material, decimal weight)
            : base(new RecyclingClimateChangeImpact(material, weight)) { }

        public override MeasurementUnit ResultMeasurementUnit
        {
            get { return MeasurementUnit.Mile; }
        }

        public override string Description
        {
            get { return "Car distance (mi) avoided"; }
        }

        protected override decimal Calculate()
        {
            return Math.Abs(Math.Round(
                RecyclingImpact.GetNetImpact()/CarCarbonDioxideEmissionPerMileInPounds, 
                2));
        }
    }
}