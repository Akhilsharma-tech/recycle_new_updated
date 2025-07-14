using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System.Linq;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    public static class RecyclingEnvironmentalImpactEquivalentCalculator
    {
        public static RecyclingEnvironmentalImpactEquivalentCalculationResult Calculate(
            Material material, 
            decimal weight)
        {
            if (material == null)
                return null;

            if (material.GetCompositions().Any() == false)
                return null;

            //TODO: Remove GetEquivalent methods from material. Set all logic in calculator.
            var climateChangeImpactEquivalent = material
                .GetRecyclingClimateChangeImpactEquivalent(weight);

            var resourceDepletionImpactEquivalent = material
                .GetRecyclingResourceDepletionImpactEquivalent(weight);

            var waterWithdrawalImpactEquivalent = material
                .GetRecyclingWaterWithdrawalImpactEquivalent(weight);

            return new RecyclingEnvironmentalImpactEquivalentCalculationResult(
                material,
                climateChangeImpactEquivalent,
                resourceDepletionImpactEquivalent,
                waterWithdrawalImpactEquivalent);
        }
    }
}