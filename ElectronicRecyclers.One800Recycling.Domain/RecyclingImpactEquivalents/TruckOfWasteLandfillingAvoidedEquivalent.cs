using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class TruckOfWasteLandfillingAvoidedEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        //http://www.epa.gov/cleanenergy/energy-resources/refs.html
        //19.51 metric tons CO2/garbage truck of waste recycled converted
        //to pounds 43,012
        public const decimal GarbageTruckOfWasteRecycledInsteadOfLandfilledInPounds = 43012M;

        public TruckOfWasteLandfillingAvoidedEquivalent(Material material, decimal weight)
            : base(new RecyclingClimateChangeImpact(material, weight)) { }

        public override MeasurementUnit ResultMeasurementUnit
        {
            get { return MeasurementUnit.Truck; }
        }

        public override string Description
        {
            get { return "Garbage trucks of waste landfilling avoided"; }
        }

        protected override decimal Calculate()
        {
            return Math.Abs(Math.Round(
                RecyclingImpact.GetNetImpact() / GarbageTruckOfWasteRecycledInsteadOfLandfilledInPounds, 
                2));
        }
    }
}