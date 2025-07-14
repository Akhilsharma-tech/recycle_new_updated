using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.JobScheduler.Job
{
    public abstract class AbstractJob<T> : IJob where T : AbstractJob<T>
    {
        public abstract Task Execute(IJobExecutionContext context);

        public abstract ITrigger CreateExecutionTrigger();

        public abstract override string ToString();

        public virtual IJobDetail ToJobDetail()
        {
            return JobBuilder
                .Create<T>()
                .WithIdentity(ToString())
                .Build();
        }

        
    }
}
