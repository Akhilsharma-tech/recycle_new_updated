//using Quartz;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ElectronicRecyclers.One800Recycling.JobScheduler.Job
//{
//    using Quartz;
//    using System;
//    using System.Linq;
//    using System.Threading;
//    using System.Threading.Tasks;

//    public class GeocodeEnvironmentalOrganizationImportAddressJob
//        : AbstractJob<GeocodeEnvironmentalOrganizationImportAddressJob>
//    {
//        private const int MaximumAddressNumberAllowedByGooglePerDay = 200;

//        public override async Task Execute(IJobExecutionContext context)
//        {
//            using (var session = NHSessionProvider.OpenSession())
//            using (var transaction = session.BeginTransaction())
//            {
//                var organizations = session
//                    .QueryOver<EnvironmentalOrganizationImport>()
//                    .Where(o => o.IsAddressValid == false)
//                    .Take(MaximumAddressNumberAllowedByGooglePerDay)
//                    .List();

//                foreach (var o in organizations)
//                {
//                    var address = await GoogleGeocodingService.GeocodeAddressAsync(o.Address);
//                    if (address != null)
//                    {
//                        o.Address = address;
//                        o.IsAddressValid = true;
//                    }

//                    await Task.Delay(300); // replaces Thread.Sleep
//                }

//                if (transaction.IsActive)
//                    transaction.Commit();
//            }
//        }

//        public override ITrigger CreateExecutionTrigger()
//        {
//            return TriggerBuilder.Create()
//                .WithIdentity("GeocodeEnvOrgJobTrigger")
//                .StartNow()
//                .WithSimpleSchedule(x => x
//                    .WithIntervalInHours(24)
//                    .RepeatForever())
//                .Build();
//        }

//        public override string ToString()
//        {
//            return nameof(GeocodeEnvironmentalOrganizationImportAddressJob);
//        }
//    }

//}
