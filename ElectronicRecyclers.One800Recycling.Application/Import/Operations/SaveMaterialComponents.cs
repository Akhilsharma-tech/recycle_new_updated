using System.Collections.Generic;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveMaterialComponents : AbstractOperation
    {
        private readonly IStatelessSession session;
        public SaveMaterialComponents(IStatelessSession session)
        {
            this.session = session;
        }

        public override  IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            using(session)
            using (var transaction = session.BeginTransaction())
            {
                foreach (var row in rows)
                {
                    var dismantlingProcess = row["DismantlingProcess"] as MaterialComponentDismantlingProcess;
                    if (dismantlingProcess == null)
                        continue;

                    session.Insert(new MaterialComponent(
                        (string)row["Name"], 
                        (string)row["Description"],
                        dismantlingProcess));
                }

                if(transaction.IsActive)
                    transaction.Commit();

                yield break;
            }
        }
    }
}