using System.Collections.Generic;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using NHibernate;
using NHibernate.Transform;


using JoinType = NHibernate.SqlCommand.JoinType;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class GetMaterials 
    {
        private readonly ISession session;

        public GetMaterials(ISession session)
        {
            this.session = session;
        }

        private static void SetEnvironmentalImpact(
            Dictionary<string,object> row, 
            RecyclingEnvironmentalImpactEquivalent equivalent, 
            string column) 
        {
            var impact = equivalent.RecyclingImpact;

            row[column+"VirginProductionProcess"] = impact
                .GetVirginProductionProcessImpact();

            row[column+"ProductDismantlingProcess"] = impact
                .GetProductDismantlingProcessImpact();

            row[column+"RecyclingProcess"] = impact.GetRecyclingProcessImpact();

            row[column+"LandfillingProcess"] = impact.GetLandfillingProcessImpact();

            row[column+"IncinerationProcess"] = impact.GetIncinerationProcessImpact();

            row[column+"NetImpact"] = impact.GetNetImpact();
        }

        public  IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            var materials = session.CreateCriteria<Material>()
                .CreateCriteria("compositions", JoinType.LeftOuterJoin)
                .CreateCriteria("MaterialComponent", JoinType.LeftOuterJoin)
                .CreateCriteria("DismantlingProcess", JoinType.LeftOuterJoin)
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Material>();

            foreach (var material in materials)
            {                    
                var row = new Dictionary<string, object>();
                row["Name"] = material.Name;

                var climateEquivalent = material.GetRecyclingClimateChangeImpactEquivalent();
                var resouceEquivalent = material.GetRecyclingResourceDepletionImpactEquivalent();
                var waterEquivalent = material.GetRecyclingWaterWithdrawalImpactEquivalent();

                if(climateEquivalent == null
                    || resouceEquivalent == null
                    || waterEquivalent == null)
                    continue;

                SetEnvironmentalImpact(row, climateEquivalent, "ClimateChangeImpact");

                SetEnvironmentalImpact(row, resouceEquivalent, "ResourceDepletionImpact");

                SetEnvironmentalImpact(row, waterEquivalent, "WaterWithdrawalImpact");

                row["ClimateChangeImpactEquivalent"] = climateEquivalent.Result;
                row["ResourceDepletionImpactEquivalent"] = resouceEquivalent.Result;
                row["WaterWithdrawalImpactEquivalent"] = waterEquivalent.Result;

                yield return row;
            }
        }
    }
}