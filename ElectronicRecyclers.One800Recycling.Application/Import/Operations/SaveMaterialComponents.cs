using System.Collections.Generic;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveMaterialComponents 
    {
        private readonly IStatelessSession session;
        public SaveMaterialComponents(IStatelessSession session)
        {
            this.session = session;
        }

        public  IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
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