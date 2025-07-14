using Newtonsoft.Json;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class EnvironmentalOrganization_History
    {
        public virtual long Id { get; set; }


        public virtual string EntityType { get; set; }
        public virtual string OriginalData { get; set; }
        public virtual string ChangedData { get; set; }


        public virtual DateTimeOffset ChangedOn { get; set; }

        public virtual string ChangedBy { get; set; }

    }


}