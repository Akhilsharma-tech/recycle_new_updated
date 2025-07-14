using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CreateMaterialComposition 
    {
         private readonly ISession session;

         public CreateMaterialComposition(ISession session)
        {
            this.session = session;
        }

        public IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            using (session)
            using (var transaction = session.BeginTransaction())
            {
                var materials = session
                    .CreateCriteria<Material>()
                    .List<Material>();

                foreach (var row in rows.Where(row => (bool) row["HasError"]))
                {
                    var material = materials.FirstOrDefault(m => m.Name.Equals(
                        row["Name"].ToString(),
                        StringComparison.CurrentCultureIgnoreCase));

                    if (material == null)
                        continue;

                    if (row["MaterialComponents"] != null)
                    {
                        ((List<Tuple<MaterialComponent, string>>) row["MaterialComponents"])
                            .ForEach(c =>
                            {
                                decimal percentage;
                                if (decimal.TryParse(c.Item2, out percentage) && percentage > 0)
                                {
                                    material.AddComposition(c.Item1, percentage*100);
                                }
                            });
                    }

                    if (row["ProductDismantlingProcesses"] != null)
                    {
                        ((List<Tuple<ProductDismantlingProcess, string>>) row["ProductDismantlingProcesses"])
                            .ForEach(c =>
                            {
                                decimal percentage;
                                if (decimal.TryParse(c.Item2, out percentage) && percentage > 0)
                                {
                                    material.AddProcess(c.Item1, percentage*100);
                                }
                            });
                    }
                }

                if (transaction.IsActive)
                    transaction.Commit();
            }
            yield break;
        }
    }
}