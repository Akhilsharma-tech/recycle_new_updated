using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class WaterUsageAvoidedEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        private const decimal NumberOfLiters = 1000M;
        private const decimal OneGallonPerLiter = 0.264172052M;

        public WaterUsageAvoidedEquivalent(Material material, decimal weight)
            : base(new RecyclingWaterWithdrawalImpact(material, weight)) { }

        public override MeasurementUnit ResultMeasurementUnit
        {
            get { return MeasurementUnit.Gallon; }
        }

        public override string Description
        {
            get { return "Gallons of water avoided"; }
        }

        protected override decimal Calculate()
        {
            return Math.Abs(Math.Round(
                (RecyclingImpact.GetNetImpact()*NumberOfLiters)*OneGallonPerLiter, 
                2));
        }
    }
}