using System.Collections.Generic;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveServiceConsumer : AbstractOperation
    {
        private readonly ISession session;
        public SaveServiceConsumer(ISession session)
        {
            this.session = session;
        }

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
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