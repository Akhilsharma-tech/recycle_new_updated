using ElectronicRecyclers.One800Recycling.Domain.Common;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class ImportHistory : DomainObject
    {
        public virtual string FileName { get; set; }
    }
}