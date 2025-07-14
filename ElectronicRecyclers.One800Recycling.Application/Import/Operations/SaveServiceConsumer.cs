using System.Collections.Generic;

using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveServiceConsumer 
    {
        private readonly ISession session;
        public SaveServiceConsumer(ISession session)
        {
            this.session = session;
        }

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            using (session)
            using (var transaction = session.BeginTransaction())
            {
                foreach (var row in rows)
                    session.Save(row.ToObject<ServiceConsumer>());

                if (transaction.IsActive)
                    transaction.Commit();

                yield break;
            }
        }
    }
}