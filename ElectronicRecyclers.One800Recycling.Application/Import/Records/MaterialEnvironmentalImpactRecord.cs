using FileHelpers;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Records
{
    [DelimitedRecord("\t")]
    public class MaterialEnvironmentalImpactRecord
    {
        
        public string Name;

        public decimal ClimateChangeImpactVirginProductionProcess;

        public decimal ClimateChangeImpactProductDismantlingProcess;

        public decimal ClimateChangeImpactRecyclingProcess;

        public decimal ClimateChangeImpactLandfillingProcess;

        public decimal ClimateChangeImpactIncinerationProcess;

        public decimal ClimateChangeImpactNetImpact;

        public decimal ResourceDepletionImpactVirginProductionProcess;

        public decimal ResourceDepletionImpactProductDismantlingProcess;

        public decimal ResourceDepletionImpactRecyclingProcess;

        public decimal ResourceDepletionImpactLandfillingProcess;

        public decimal ResourceDepletionImpactIncinerationProcess;

        public decimal ResourceDepletionImpactNetImpact;

        public decimal WaterWithdrawalImpactVirginProductionProcess;

        public decimal WaterWithdrawalImpactProductDismantlingProcess;

        public decimal WaterWithdrawalImpactRecyclingProcess;

        public decimal WaterWithdrawalImpactLandfillingProcess;

        public decimal WaterWithdrawalImpactIncinerationProcess;

        public decimal WaterWithdrawalImpactNetImpact;

        public decimal ClimateChangeImpactEquivalent;

        public decimal ResourceDepletionImpactEquivalent;

        public decimal WaterWithdrawalImpactEquivalent;
    }
}