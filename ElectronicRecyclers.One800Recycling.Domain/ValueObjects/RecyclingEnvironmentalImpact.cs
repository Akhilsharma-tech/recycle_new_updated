using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.Exceptions;
using System;
using System.Linq;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    /// <summary>
    /// Represents immutable RecyclingEnvironmentalBenefit object.
    /// </summary>
    public abstract class RecyclingEnvironmentalImpact : IEquatable<RecyclingEnvironmentalImpact>
    {
        public const decimal LandfillingPercentage = 80;

        public const decimal IncinerationPercentage = 20;

        public Material Material { get; protected set; }

        public decimal Weight { get; protected set; }

        protected RecyclingEnvironmentalImpact() { }

        protected RecyclingEnvironmentalImpact(Material material, decimal weight)
        {
            Guard.Against<ArgumentNullException>(material == null, "Material is null.");

            Guard.Against<BusinessException>(
                material.GetCompositions().Any() == false,
                "Material does not have material compositions.");

            Guard.Against<BusinessException>(weight <= 0, "Material weight is invalid.");

            Material = material;
            Weight = weight;
        }

        protected RecyclingEnvironmentalImpact(Material material) 
            : this(material, 1) {}

        public abstract decimal GetVirginProductionProcessImpact();

        public abstract decimal GetRecyclingProcessImpact();

        public abstract decimal GetLandfillingProcessImpact();

        public abstract decimal GetIncinerationProcessImpact();

        public abstract decimal GetProductDismantlingProcessImpact();

        public abstract decimal GetNetImpact();

        public override int GetHashCode()
        {
            return Material.GetHashCode()*373;
        }

        public bool Equals(RecyclingEnvironmentalImpact other)
        {
            return other != null && Material == other.Material;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RecyclingEnvironmentalImpact);
        }

        public static bool operator ==(
            RecyclingEnvironmentalImpact objA, 
            RecyclingEnvironmentalImpact objB)
        {
            return Equals(objA, objB);
        }

        public static bool operator !=(
           RecyclingEnvironmentalImpact objA,
           RecyclingEnvironmentalImpact objB)
        {
            return !Equals(objA, objB);
        }
    }
}