using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class EnvironmentalOrganization_HistoryMap : ClassMapping<EnvironmentalOrganization_History>
    {
        public EnvironmentalOrganization_HistoryMap()
        {
            
            Id(x => x.Id, m => m.Generator(Generators.Identity));


            Property(x => x.EntityType, m => m.Length(80));

            Property(x => x.OriginalData, m => m.Length(int.MaxValue));
            Property(x => x.ChangedData,m=> m.Length(int.MaxValue));

            Property(x => x.ChangedOn, m => m.Type<DateTimeOffsetType>());

            Property(x => x.ChangedBy, m => m.Length(80));

        }
    }
}