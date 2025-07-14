using System;
using System.Collections.Generic;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
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

        public  IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
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

        public event EventHandler<DynamicReader> RecordInserted;
    }
}