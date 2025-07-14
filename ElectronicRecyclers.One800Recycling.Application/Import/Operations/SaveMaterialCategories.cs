using System.Collections.Generic;
using System.Linq;

using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveMaterialCategories 
    {
        private readonly ISession session;

        public SaveMaterialCategories(ISession session)
        {
            this.session = session;
        }

        public  IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            using (session)
            using (var transaction = session.BeginTransaction())
            {
                foreach (var row in rows.Where(row => !(bool)row["IsDuplicate"]))
                    session.Save(row.ToObject<MaterialCategory>());

                if (transaction.IsActive)
                    transaction.Commit();

                yield break;
            }
        }
    }
}