using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    public class RecyclingClimateChangeImpactEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        public const decimal PassengerVehicleCO2EmissionsEquivalentPerMileInPounds = 0.663475281M;

        public RecyclingClimateChangeImpactEquivalent(RecyclingClimateChangeImpact recyclingImpact) 
            : base(recyclingImpact) { }

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
                RecyclingImpact.GetNetImpact()/PassengerVehicleCO2EmissionsEquivalentPerMileInPounds, 
                2));
        }
    }
}