using System.Collections.Generic;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveRecyclingTips : AbstractOperation
    {
        private readonly ISession session;
        public SaveRecyclingTips(ISession session)
        {
            this.session = session;
        }

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
             using (session)
             using (var transaction = session.BeginTransaction())
             {
                 var number = 1;

                 foreach (var row in rows)
                 {
                     var title = row["Title"] as string;
                     var description = row["Description"] as string;
                     var image = row["ImageName"] as string;

                     if (string.IsNullOrWhiteSpace(title) 
                         || string.IsNullOrWhiteSpace(description)
                         || string.IsNullOrWhiteSpace(image))
                         continue;

                     session.Save(new RecyclingTip(title, description, image, number++));
                 }

                 if (transaction.IsActive)
                     transaction.Commit();

                 yield break;  
             }
        }
    }
}