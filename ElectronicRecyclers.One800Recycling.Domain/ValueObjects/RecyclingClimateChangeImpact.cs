
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Util;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    public class RecyclingClimateChangeImpact : RecyclingEnvironmentalImpact
    {
        public RecyclingClimateChangeImpact(Material material) : base(material) { }

        public RecyclingClimateChangeImpact(Material material, decimal weight) 
            : base(material, weight) { }

        public override decimal GetVirginProductionProcessImpact()
        {
            var result = 0M;
            Material.GetCompositions().ForEach(c =>
            {
                var process = c
                    .MaterialComponent
                    .DismantlingProcess;

                var climateChangeImpact = process
                    .VirginProductionProcess
                    .EnvironmentalImpact
                    .ClimateChangeImpact;

                var lossPercentage = process
                    .RecyclingProcess
                    .LossPercentageDuringRecycling;

                result += (c.CompositionPercentage * 0.01M)
                    * climateChangeImpact
                    * (1-(lossPercentage * 0.01M));
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
                            .ClimateChangeImpact;
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
                            .ClimateChangeImpact) 
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
                            .ClimateChangeImpact) 
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
                            .ClimateChangeImpact
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