using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class OilUsageAvoidedEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        public const decimal OilEnergyInMegaJoulesPerGallon = 116.6966744M;

        public OilUsageAvoidedEquivalent(Material material, decimal weight)
            : base(new RecyclingResourceDepletionImpact(material, weight)) { }

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
           return Math.Abs(Math.Round(
               RecyclingImpact.GetNetImpact()/OilEnergyInMegaJoulesPerGallon, 
               2));
        }
    }
}