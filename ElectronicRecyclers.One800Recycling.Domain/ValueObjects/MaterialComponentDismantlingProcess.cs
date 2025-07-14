using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.Exceptions;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    /// <summary>
    /// Represents immutable MaterialComponentDismantlingProcess object.
    /// </summary>
    [Serializable]
    public class MaterialComponentDismantlingProcess 
        : IEquatable<MaterialComponentDismantlingProcess>
    {
        public DismantlingProcess VirginProductionProcess { get; protected set; }

        public DismantlingProcess RecyclingProcess { get; protected set; }

        public DismantlingProcess LandfillingProcess { get; protected set; }

        public DismantlingProcess IncinerationProcess { get; protected set; }

        protected MaterialComponentDismantlingProcess() { }

        public MaterialComponentDismantlingProcess(
            DismantlingProcess virginProductionProcess,
            DismantlingProcess recyclingProcess,
            DismantlingProcess landfillingProcess,
            DismantlingProcess incinerationProcess)
        {
            Guard.Against<ArgumentNullException>(
                virginProductionProcess == null, 
                "Virgin production process is null.");

            Guard.Against<ArgumentNullException>(
                recyclingProcess == null, 
                "Recycling process is null.");

            Guard.Against<ArgumentNullException>(
                landfillingProcess == null, 
                "Landfilling process is null.");

            Guard.Against<ArgumentNullException>(
                incinerationProcess == null, 
                "Incineration process is null.");

            Guard.Against<BusinessException>(
                virginProductionProcess.Type != DismantlingProcessType.VirginProduction 
                && virginProductionProcess.Type != DismantlingProcessType.None,
                "Virgin production process is wrong type.");

            Guard.Against<BusinessException>(
                recyclingProcess.Type != DismantlingProcessType.Recycling 
                && recyclingProcess.Type != DismantlingProcessType.None,
                "Recycling process is wrong type.");

            Guard.Against<BusinessException>(
                landfillingProcess.Type != DismantlingProcessType.Landfilling
                && landfillingProcess.Type != DismantlingProcessType.None,
                "Landfilling process is wrong type.");

            Guard.Against<BusinessException>(
                incinerationProcess.Type != DismantlingProcessType.Incineration
                && incinerationProcess.Type != DismantlingProcessType.None,
                "Incineration process is wrong type.");

            VirginProductionProcess = virginProductionProcess;
            RecyclingProcess = recyclingProcess;
            LandfillingProcess = landfillingProcess;
            IncinerationProcess = incinerationProcess;
        }

        public EnvironmentalImpact GetVirginProductionProcessImpact()
        {
            var recyclingProcessLoss = RecyclingProcess.LossPercentageDuringRecycling;
            var lossPercentage = (decimal)(recyclingProcessLoss == 0
                                                ? 0
                                                : 1 - (recyclingProcessLoss/100));

            var virginImpact = VirginProductionProcess.EnvironmentalImpact;

            return new EnvironmentalImpact(
                virginImpact.ClimateChangeImpact * lossPercentage,
                virginImpact.ResourceDepletionImpact * lossPercentage,
                virginImpact.WaterWithdrawalImpact * lossPercentage);
        }

        public EnvironmentalImpact GetRecyclingProcessImpact()
        {
            return RecyclingProcess.EnvironmentalImpact;
        }

        public EnvironmentalImpact GetLandfillingProcessImpact()
        {
            return LandfillingProcess.EnvironmentalImpact;
        }

        public EnvironmentalImpact GetIncinerationProcessImpact()
        {
            return IncinerationProcess.EnvironmentalImpact;
        }

        public override int GetHashCode()
        {
            return VirginProductionProcess.GetHashCode()*373
                   ^ RecyclingProcess.GetHashCode()*373
                   ^ LandfillingProcess.GetHashCode()*373
                   ^ IncinerationProcess.GetHashCode()*373;
        }

        public bool Equals(MaterialComponentDismantlingProcess other)
        {
            return other != null
                   && VirginProductionProcess == other.VirginProductionProcess
                   && RecyclingProcess == other.RecyclingProcess
                   && LandfillingProcess == other.LandfillingProcess
                   && IncinerationProcess == other.IncinerationProcess;
        }

        public override bool Equals(object other)
        {
            return Equals(other as MaterialComponentDismantlingProcess);
        }

        public static bool operator ==(
            MaterialComponentDismantlingProcess objA, 
            MaterialComponentDismantlingProcess objB)
        {
            return Equals(objA, objB);
        }

        public static bool operator !=(
            MaterialComponentDismantlingProcess objA,
            MaterialComponentDismantlingProcess objB)
        {
            return !Equals(objA, objB);
        }
    }
}