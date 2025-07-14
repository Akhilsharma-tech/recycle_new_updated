using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class SystemLog
    {
        public virtual int Id { get; set; }

        public virtual string Application { get; set; }
        
        public virtual string Logger { get; set; }

        public virtual string Message { get; set; }

        public virtual string MachineName { get; set; }

        public virtual string LogLevel { get; set; }

        public virtual string Thread { get; set; }

        public virtual string UserName { get; set; }

        public virtual string CallSite { get; set; }

        public virtual string Exception { get; set; }

        public virtual string StackTrace { get; set; }

        public virtual DateTimeOffset CreatedOn { get; set; }
    }
}