using System.Collections.Generic;
using System.Linq;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SavePostalCodes 
    {
        private readonly IStatelessSession session;
        public SavePostalCodes(IStatelessSession session)
        {
            this.session = session;
        }

        private static double ParseDouble(object obj)
        {
            if (obj == null)
                return 0;

            double result;
            return double.TryParse(obj.ToString(), out result) == false 
                ? 0 
                : result;
        }

        public  IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            using(session)
            using (var transaction = session.BeginTransaction())
            {
                foreach (var row in rows.Where(row => !(bool)row["IsDuplicate"]))
                {
                    session.Insert(new PostalCode(
                        (string)row["PostalCode"],
                        (string)row["City"],
                        (string)row["Region"],
                        (string)row["State"],
                        (string)row["Country"],
                        ParseDouble(row["Latitude"]),
                        ParseDouble(row["Longitude"])));
                }

                if(transaction.IsActive)
                    transaction.Commit();

                yield break;
            }
        }
    }
}