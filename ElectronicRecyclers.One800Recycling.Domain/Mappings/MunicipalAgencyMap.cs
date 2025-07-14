using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Mapping.ByCode.Conformist;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class MunicipalAgencyMap : JoinedSubclassMapping<MunicipalAgency>
    {
        public MunicipalAgencyMap() 
        {
            Key(x => x.Column("Id"));
        }
    }
}