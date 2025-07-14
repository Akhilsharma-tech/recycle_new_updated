using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System.Collections.Generic;





namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class MarkOrganizationImportAsDuplicateIfOrganizationExist 
    {
        public IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            using (var session = NHSessionProvider.OpenStatelessSession())
            using (var transaction = session.BeginTransaction())
            {
                var results = new List<DynamicReader>();
                foreach (var row in rows)
                {
                    if ((bool)row["IsDuplicateOrganizationFoundDuringMoveOperation"])
                        session.Update(row.ToObject<EnvironmentalOrganizationImport>());

                    results.Add(row);
                }

                if (transaction.IsActive)
                    transaction.Commit();

                return results;
            }
        }
    }
}