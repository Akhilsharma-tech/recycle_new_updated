using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveLookupCodes<T> 
        where T : ILookupCode
    {
        private readonly ISession session;
        public SaveLookupCodes(ISession session)
        {
            this.session = session;
        }

        public  IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            using (session)
            using (var transaction = session.BeginTransaction())
            {
                foreach (var row in rows.Where(row => !(bool)row["IsDuplicate"]))
                {
                    session.Save((T)Activator.CreateInstance(
                        typeof(T),
                        row["Name"].ToString(),
                        row["Code"].ToString(),
                        row["Description"].ToString()));
                }

                if(transaction.IsActive)
                    transaction.Commit();

                yield break;
            }
            
        }
    }
}