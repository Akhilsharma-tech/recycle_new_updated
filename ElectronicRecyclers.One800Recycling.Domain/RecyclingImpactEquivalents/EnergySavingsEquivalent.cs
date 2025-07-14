using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class EnergySavingsEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        public const decimal HouseholdEnergyUsagePerYearInKilowatts = 12069M;

        public EnergySavingsEquivalent(Material material, decimal weight)
            : base(new RecyclingResourceDepletionImpact(material, weight)) { }

        public override MeasurementUnit ResultMeasurementUnit
        {
            get { return MeasurementUnit.Household; }
        }

        public override string Description
        {
            get { return "US households' energy saved in one year"; }
        }

        protected override decimal Calculate()
        {
            return Math.Abs(Math.Round(
                RecyclingImpact.GetNetImpact()/HouseholdEnergyUsagePerYearInKilowatts, 
                2));
        }
    }
}