using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveDismantlingProcesses 
    {
        private readonly IStatelessSession session;
        public SaveDismantlingProcesses(IStatelessSession session)
        {
            this.session = session;
        }

        private static EnvironmentalImpact GetEnvironmentalImpact(Row row)
        {
            var climateImpactStr = row["ClimateChangeImpact"].ToString();
            var resourceImpactStr = row["ResourceDepletionImpact"].ToString();
            var waterImpactStr = row["WaterWithdrawalImpact"].ToString();
            decimal climateImpact;
            decimal resourceImpact;
            decimal waterImpact;

            if (string.IsNullOrEmpty(climateImpactStr)
                || string.IsNullOrEmpty(resourceImpactStr)
                || string.IsNullOrEmpty(waterImpactStr)
                || decimal.TryParse(climateImpactStr, out climateImpact) == false
                || decimal.TryParse(resourceImpactStr, out resourceImpact) == false
                || decimal.TryParse(waterImpactStr, out waterImpact) == false)
                return null;

            return new EnvironmentalImpact(climateImpact, resourceImpact, waterImpact);
        }

        private static decimal ParseLossPercentage(Row row)
        {
            var loss = row["LossPercentageDuringRecycling"];
            double result;
            if (loss != null && double.TryParse(loss.ToString(), out result))
                return (decimal) result * 100;

            return 0M;
        }

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            using (session)
            using (var transaction = session.BeginTransaction())
            {
                foreach (var row in rows.Where(row => !(bool)row["IsDuplicate"]))
                {

                    if (row["Name"] == null)
                        continue;

                    DismantlingProcessType type;
                    if (Enum.TryParse(row["Type"].ToString(), true, out type) == false)
                        continue;

                    var environmentalImpact = GetEnvironmentalImpact(row);
                    if (environmentalImpact == null)
                        continue;

                    var process = new DismantlingProcess(
                        row["Name"].ToString(),
                        environmentalImpact,
                        type)
                    {
                        LossPercentageDuringRecycling = ParseLossPercentage(row)
                    };

                    session.Insert(process);
                }

                if (transaction.IsActive)
                    transaction.Commit();

                yield break;
            }
        }
    }
}