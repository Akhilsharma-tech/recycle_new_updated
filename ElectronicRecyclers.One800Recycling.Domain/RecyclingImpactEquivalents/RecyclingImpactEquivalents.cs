using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ElectronicRecyclers.One800Recycling.Domain.RecyclingImpactEquivalents
{
    public class RecyclingImpactEquivalents
    {
        public static RecyclingImpactEquivalents For(Material material, decimal weight)
        {
            return new RecyclingImpactEquivalents(material, weight);
        }

        private readonly ICollection<RecyclingEnvironmentalImpactEquivalent> equivalents = 
            new Collection<RecyclingEnvironmentalImpactEquivalent>();

        protected RecyclingImpactEquivalents(Material material, decimal weight)
        {
            equivalents.Add(new CarDistanceAvoidedEquivalent(material, weight));
            equivalents.Add(new CarRemovalEquivalent(material, weight));
            equivalents.Add(new CoalBurningAvoidedEquivalent(material, weight));
            equivalents.Add(new EnergySavingsEquivalent(material, weight));
            equivalents.Add(new OilUsageAvoidedEquivalent(material, weight));
            equivalents.Add(new PropaneCylinderUsageAvoidedEquivalent(material, weight));
            equivalents.Add(new RailCarOfCoalBurningAvoidedEquivalent(material, weight));
            equivalents.Add(new TruckOfWasteLandfillingAvoidedEquivalent(material, weight));
            equivalents.Add(new WaterUsageAvoidedEquivalent(material, weight));
            equivalents.Add(new NaturalGasBurningAvoidedEquivalent(material, weight));
            equivalents.Add(new ElectricityUsageAvoidedEquivalent(material, weight));
            equivalents.Add(new TreeSeedlingsGrownEquivalent(material, weight));
            equivalents.Add(new DeforestationEquivalent(material, weight));
        }

        public IEnumerable<RecyclingEnvironmentalImpactEquivalent> GetEquivalents()
        {
            return equivalents;
        } 
    }
}