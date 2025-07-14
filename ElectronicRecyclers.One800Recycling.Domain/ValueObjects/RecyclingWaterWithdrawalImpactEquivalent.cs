using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    public class RecyclingWaterWithdrawalImpactEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        private const decimal NumberOfLiters = 1000M;
        private const decimal OneGallonPerLiter = 0.264172052M;

        public RecyclingWaterWithdrawalImpactEquivalent(RecyclingWaterWithdrawalImpact recyclingImpact) 
            : base(recyclingImpact) { }

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
            return Math.Abs(Math.Round((RecyclingImpact.GetNetImpact()*NumberOfLiters)*OneGallonPerLiter, 2));
        }
    }
}