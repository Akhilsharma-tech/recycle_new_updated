

using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Util;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    public class RecyclingResourceDepletionImpact : RecyclingEnvironmentalImpact
    {
        public RecyclingResourceDepletionImpact(Material material) : base(material) { }

        public RecyclingResourceDepletionImpact(Material material, decimal weight) 
            : base(material, weight) {}

        public override decimal GetVirginProductionProcessImpact()
        {
            var result = 0M;
            Material.GetCompositions().ForEach(c =>
            {
                var process = c
                    .MaterialComponent
                    .DismantlingProcess;

                var resourceDepletionImpact = process
                    .VirginProductionProcess
                    .EnvironmentalImpact
                    .ResourceDepletionImpact;

                var lossPercentage = process
                    .RecyclingProcess
                    .LossPercentageDuringRecycling;

                result += (c.CompositionPercentage * 0.01M)
                    * resourceDepletionImpact
                    * (1 - (lossPercentage * 0.01M));
            });

            return result * Weight;
        }

        public override decimal GetRecyclingProcessImpact()
        {
            var result = 0M;
            Material.GetCompositions().ForEach(c =>
            {
                result += (c.CompositionPercentage * 0.01M)
                    * c.MaterialComponent
                            .DismantlingProcess
                            .RecyclingProcess
                            .EnvironmentalImpact
                            .ResourceDepletionImpact;
            });

            return result * Weight;
        }

        public override decimal GetLandfillingProcessImpact()
        {
            var result = 0M;
            Material.GetCompositions().ForEach(c =>
            {
                result += ((c.CompositionPercentage * 0.01M)
                    * c.MaterialComponent
                            .DismantlingProcess
                            .LandfillingProcess
                            .EnvironmentalImpact
                            .ResourceDepletionImpact)
                    * (LandfillingPercentage * 0.01M);
            });

            return result * Weight;
        }

        public override decimal GetIncinerationProcessImpact()
        {
            var result = 0M;
            Material.GetCompositions().ForEach(c =>
            {
                result += ((c.CompositionPercentage * 0.01M)
                    * c.MaterialComponent
                            .DismantlingProcess
                            .IncinerationProcess
                            .EnvironmentalImpact
                            .ResourceDepletionImpact)
                    * (IncinerationPercentage * 0.01M);
            });

            return result * Weight;
        }

        public override decimal GetProductDismantlingProcessImpact()
        {
            var result = 0M;
            Material.GetProcesses().ForEach(p =>
            {
                result += p.ProductDismantlingProcess
                            .DismantlingProcess
                            .EnvironmentalImpact
                            .ResourceDepletionImpact 
                    * (p.CompositionPercentage * 0.01M);
            });

            return result * Weight;
        }

        public override decimal GetNetImpact()
        {
            return GetRecyclingProcessImpact()
                   + GetProductDismantlingProcessImpact()
                   - GetVirginProductionProcessImpact()
                   - GetIncinerationProcessImpact()
                   - GetLandfillingProcessImpact();
        }
    }
}