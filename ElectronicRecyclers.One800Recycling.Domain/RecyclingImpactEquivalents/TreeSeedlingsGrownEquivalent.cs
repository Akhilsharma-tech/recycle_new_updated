using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class TreeSeedlingsGrownEquivalent : RecyclingEnvironmentalImpactEquivalent
    {
        //http://www.epa.gov/cleanenergy/energy-resources/refs.html#seedlings
        //0.039 metric ton CO2 per urban tree planted converted
        //to pounds 85.98
        public const decimal CarbonDixiodeEmissionPerTreePlantedInPounds = 85.98M;

        public TreeSeedlingsGrownEquivalent(Material material, decimal weight)
            : base(new RecyclingClimateChangeImpact(material, weight)) { }

        public override MeasurementUnit ResultMeasurementUnit
        {
            get { return MeasurementUnit.Tree; }
        }

        public override string Description
        {
            get { return "Number of  tree seedlings grown for 10 years"; }
        }

        protected override decimal Calculate()
        {
            return Math.Abs(Math.Round(
                RecyclingImpact.GetNetImpact() / CarbonDixiodeEmissionPerTreePlantedInPounds, 
                2));
        }
    }
}