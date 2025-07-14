using System;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    public class RecyclingEnvironmentalImpactEquivalentCalculationResult
        : IEquatable<RecyclingEnvironmentalImpactEquivalentCalculationResult>
    {
        public Material Material { get; protected set; }

        public RecyclingClimateChangeImpactEquivalent ClimateChangeImpactEquivalent { get; protected set; }

        public RecyclingResourceDepletionImpactEquivalent ResourceDepletionImpactEquivalent { get; protected set; }

        public RecyclingWaterWithdrawalImpactEquivalent WaterWithdrawalImpactEquivalent { get; protected set; }

        public RecyclingEnvironmentalImpactEquivalentCalculationResult(
            Material material,
            RecyclingClimateChangeImpactEquivalent climateChangeImpactEquivalent,
            RecyclingResourceDepletionImpactEquivalent resourceDepletionImpactEquivalent,
            RecyclingWaterWithdrawalImpactEquivalent waterWithdrawalImpactEquivalent)
        {
            Guard.Against<ArgumentNullException>(material == null, "Material is null.");

            Guard.Against<ArgumentNullException>(
                climateChangeImpactEquivalent == null, 
                "Climate change impact equivalent is null.");

            Guard.Against<ArgumentNullException>(
                resourceDepletionImpactEquivalent == null, 
                "Resource depletion impact equivalent is null.");

            Guard.Against<ArgumentNullException>(
                waterWithdrawalImpactEquivalent == null, 
                "Water withdrawal impact equivalent is null.");

            Material = material;
            ClimateChangeImpactEquivalent = climateChangeImpactEquivalent;
            ResourceDepletionImpactEquivalent = resourceDepletionImpactEquivalent;
            WaterWithdrawalImpactEquivalent = waterWithdrawalImpactEquivalent;
        }

        public override int GetHashCode()
        {
            return Material.GetHashCode() * 373
                   ^ ClimateChangeImpactEquivalent.GetHashCode() * 373
                   ^ ResourceDepletionImpactEquivalent.GetHashCode() * 373
                   ^ WaterWithdrawalImpactEquivalent.GetHashCode() * 373;
        }

        public bool Equals(RecyclingEnvironmentalImpactEquivalentCalculationResult other)
        {
            return Material == other.Material
                   && ClimateChangeImpactEquivalent == other.ClimateChangeImpactEquivalent
                   && ResourceDepletionImpactEquivalent == other.ResourceDepletionImpactEquivalent
                   && WaterWithdrawalImpactEquivalent == other.WaterWithdrawalImpactEquivalent;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RecyclingEnvironmentalImpactEquivalentCalculationResult);
        }

        public static bool operator ==(
            RecyclingEnvironmentalImpactEquivalentCalculationResult objA,
            RecyclingEnvironmentalImpactEquivalentCalculationResult objB)
        {
            return Equals(objA, objB);
        }

        public static bool operator !=(
          RecyclingEnvironmentalImpactEquivalentCalculationResult objA,
          RecyclingEnvironmentalImpactEquivalentCalculationResult objB)
        {
            return !Equals(objA, objB);
        }
    }
}