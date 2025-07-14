using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class SystemLogMap : ClassMapping<SystemLog>
    {
        public SystemLogMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            Property(x => x.Application, m =>
            {
                m.Length(200);
                m.NotNullable(false);
            });

            Property(x => x.LogLevel, m =>
            {
                m.Length(100);
                m.NotNullable(false);
            });

            Property(x => x.Thread, m =>
            {
                m.Length(100);
                m.NotNullable(false);
            });

            Property(x => x.UserName, m =>
            {
                m.Length(80);
                m.NotNullable(false);
            });

            Property(x => x.Logger, m =>
            {
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(false);
            });

            Property(x => x.CallSite, m =>
            {
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(false);
            });
            
            Property(x => x.Message, m =>
            {
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(false);    
            });

            Property(x => x.MachineName, m =>
            {
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(false);
            });

            Property(x => x.Exception, m =>
            {
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(false);
            });

            Property(x => x.StackTrace, m =>
            {
                m.Type(NHibernateUtil.StringClob);
                m.NotNullable(false);
            });

            Property(x => x.CreatedOn, m => m.Type<DateTimeOffsetType>());
        }
    }
}