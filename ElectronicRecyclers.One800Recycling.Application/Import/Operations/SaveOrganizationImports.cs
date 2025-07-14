using System;
using System.Collections.Generic;

using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveOrganizationImports 
    {
        private readonly IStatelessSession session;
        public SaveOrganizationImports(IStatelessSession session)
        {
            this.session = session;
        }

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            using (session)
            using (var transaction = session.BeginTransaction())
            {
                foreach (var row in rows)
                {
                    session.Insert(row.ToObject<EnvironmentalOrganizationImport>());

                    if (RecordInserted != null)
                        RecordInserted(this, row);

                    yield return row;
                }

                if (transaction != null && transaction.IsActive)
                    transaction.Commit();
            }
        }

        public event EventHandler<Row> RecordInserted;
    }
}