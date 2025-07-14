using Common.Logging;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate;
using NHibernate.Linq;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveOrganizationMaterials<T>  where T : EnvironmentalOrganization
    {

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            ISession session;
            ITransaction transaction;
            using (session = NHSessionProvider.OpenSession())
            using (transaction = session.BeginTransaction())
            {
                var materials = session
                      .CreateCriteria<Material>()
                      .List<Material>();
                const int transactionCommitSize = 500;
                var count = 0;

                foreach (var row in rows)
                {
                    if (!long.TryParse(row["OrganizationId"]?.ToString(), out var orgId) ||
                        !long.TryParse(row["MaterialId"]?.ToString(), out var materialId))
                        continue;

                    var organization = session.Query<T>().FirstOrDefault(x => x.Id == orgId);
                    if (!(organization is EnvironmentalOrganization envOrg))
                        continue;

                    var material = materials.FirstOrDefault(m => m.Id == materialId);
                    if (material == null) continue;

                    var business = Parse(row["MaterialBusinessDeliveryOption"]?.ToString());
                    var residential = Parse(row["MaterialResidentialDeliveryOption"]?.ToString());

                    envOrg.AddMaterial(material, residential, business);
                    session.SaveOrUpdate(envOrg);

                    if (++count % transactionCommitSize == 0 && transaction.IsActive)
                    {
                        transaction.Commit();
                        session.Close();
                        session = NHSessionProvider.OpenSession();
                        transaction = session.BeginTransaction();


                    }
                    yield return row;
                }

                if (transaction.IsActive)
                    transaction.Commit();

                session.Dispose();
                yield break;
            }
        }

        private static IList<string> Parse(string input)
        {
            return string.IsNullOrWhiteSpace(input)
                ? new List<string>()
                : input.Split(',').Select(x => x.Trim()).ToList();
        }

    }


}