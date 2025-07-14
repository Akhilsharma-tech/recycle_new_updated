using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    public class RecyclingResourceDepletionImpactEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        public const decimal OilEnergyInMegaJoulesPerGallon = 116.6966744M;

        public RecyclingResourceDepletionImpactEquivalent(RecyclingResourceDepletionImpact recyclingImpact) 
            : base(recyclingImpact) {}

        public override MeasurementUnit ResultMeasurementUnit
        {
            get { return MeasurementUnit.Gallon; }
        }

        public override string Description
        {
            get { return "Gallons of oil avoided"; }
        }

        protected override decimal Calculate()
        {
           return Math.Abs(Math.Round(RecyclingImpact.GetNetImpact()/OilEnergyInMegaJoulesPerGallon, 2));
        }
    }
}