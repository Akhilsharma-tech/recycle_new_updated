using System;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    /// <summary>
    /// Represents immutable EnvironmentalImpact object.
    /// </summary>
    [Serializable]
    public class EnvironmentalImpact : IEquatable<EnvironmentalImpact>
    {
        public virtual decimal ClimateChangeImpact { get; protected set; }

        public virtual decimal ResourceDepletionImpact { get; protected set; }

        public virtual decimal WaterWithdrawalImpact { get; protected set; }

        protected EnvironmentalImpact() { }

        public EnvironmentalImpact(
            decimal climateChangeImpact, 
            decimal resourceDepletionImpact, 
            decimal waterWithdrawalImpact)
        {
            ClimateChangeImpact = climateChangeImpact;
            ResourceDepletionImpact = resourceDepletionImpact;
            WaterWithdrawalImpact = waterWithdrawalImpact;
        }

        public override int GetHashCode()
        {
            return ClimateChangeImpact.GetHashCode()*373
                   ^ ResourceDepletionImpact.GetHashCode()*373
                   ^ WaterWithdrawalImpact.GetHashCode()*373;
        }

        public bool Equals(EnvironmentalImpact other)
        {
            return other != null
                   && ClimateChangeImpact.Equals(other.ClimateChangeImpact)
                   && ResourceDepletionImpact.Equals(other.ResourceDepletionImpact)
                   && WaterWithdrawalImpact.Equals(other.WaterWithdrawalImpact);
        }

        public override bool Equals(object other)
        {
            return Equals(other as EnvironmentalImpact);
        }

        public static bool operator ==(EnvironmentalImpact objA, EnvironmentalImpact objB)
        {
            return Equals(objA, objB);
        }

        public static bool operator !=(EnvironmentalImpact objA, EnvironmentalImpact objB)
        {
            return !Equals(objA, objB);
        }
    }
}