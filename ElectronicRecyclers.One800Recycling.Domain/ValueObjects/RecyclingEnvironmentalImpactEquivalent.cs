using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    public abstract class RecyclingEnvironmentalImpactEquivalent
    {
        public RecyclingEnvironmentalImpact RecyclingImpact { get; protected set; }

        public Material Material
        {
            get { return RecyclingImpact.Material; }
        }

        public decimal Weight
        {
            get { return RecyclingImpact.Weight; }
        }

        public MeasurementUnit WeightMeasurementUnit
        {
            get { return MeasurementUnit.Pound; }
        }

        public abstract MeasurementUnit ResultMeasurementUnit { get; }

        public abstract string Description { get; }

        public decimal Result { get; protected set; }

        protected abstract decimal Calculate();

        protected RecyclingEnvironmentalImpactEquivalent(RecyclingEnvironmentalImpact impact)
        {
            Guard.Against<ArgumentNullException>(impact == null, "Recycling impact is null.");
            RecyclingImpact = impact;
            Result = Calculate();
        }
    }
}