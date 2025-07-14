
using System.Reflection;
using ElectronicRecyclers.One800Recycling.Domain.Mappings;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Event;
using NHibernate.Mapping.ByCode;
using Configuration = NHibernate.Cfg.Configuration;

namespace ElectronicRecyclers.One800Recycling.Infrastructure.Core
{
    public static class NHSessionProvider
    {
        public static Action<IDbIntegrationConfigurationProperties> DbConfigurationProperties;
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static string GetConnectionString()
        {
            var connectionString = _configuration.GetConnectionString("1800RecyclingDb");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string '1800RecyclingDb' not found.");

            return connectionString;
        }

        private static void SetDbConfigurationDialect(IDbIntegrationConfigurationProperties db)
        {
            var dialect = _configuration["NHibernateConfigurationDbDialect"];

            switch (dialect)
            {
                case "MsSql2012Dialect":
                    db.Dialect<MsSql2012Dialect>();
                    break;

                case "MsSqlAzure2008Dialect":
                    db.Dialect<MsSqlAzure2008Dialect>();
                    break;

                default:
                    db.Dialect<MsSql2012Dialect>();
                    break;
            }
        }

        private static void SetDbConfiguration(Configuration configuration)
        {
            if (DbConfigurationProperties == null)
            {
                DbConfigurationProperties = db =>
                {
                    db.ConnectionString = GetConnectionString();
                    db.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
                    db.Driver<SqlClientDriver>();
                    db.BatchSize = 300;
                    db.AutoCommentSql = false;
                    SetDbConfigurationDialect(db);
                };
            }

            configuration.DataBaseIntegration(DbConfigurationProperties);

            // Use AsyncLocalSessionContext for web apps in .NET Core
            configuration.CurrentSessionContext<AsyncLocalSessionContext>();
        }

        private static void SetModelMappings(Configuration configuration)
        {
            var mapper = new ModelMapper();
            MappingConventionConfiguration.Configure(mapper);

            var domainAssembly = Assembly.Load("ElectronicRecyclers.One800Recycling.Domain");

            mapper.AddMappings(domainAssembly
                .GetTypes()
                .Where(p => p.Namespace != null && p.Namespace.EndsWith(".Mappings")));

            configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
        }

        public static void EnableAuditEntry(bool enabled)
        {
            var configuration = Configuration;

            configuration.SetListener(ListenerType.PreInsert, null);
            configuration.SetListener(ListenerType.PreUpdate, null);
            configuration.SetListener(ListenerType.PreDelete, null);

            if (!enabled) return;

            configuration.AppendListeners(ListenerType.PreInsert, new[] { new AuditEventListener() });
            configuration.AppendListeners(ListenerType.PreUpdate, new[] { new AuditEventListener() });
            configuration.AppendListeners(ListenerType.PreDelete, new[] { new AuditEventListener() });
        }

        private static Configuration cfg;

        public static Configuration Configuration
        {
            get
            {
                if (cfg == null)
                {
                    cfg = new Configuration();
                    SetDbConfiguration(cfg);
                    SetModelMappings(cfg);
                    EnableAuditEntry(true);
                }

                return cfg;
            }
        }

        private static ISessionFactory sessionFactory;

        public static ISessionFactory SessionFactory =>
            sessionFactory ??= Configuration.BuildSessionFactory();

        public static ISession CurrentSession
        {
            get
            {
                if (CurrentSessionContext.HasBind(SessionFactory))
                    return SessionFactory.GetCurrentSession();

                return OpenSession(); // You may choose to avoid this and throw instead
            }
        }

        public static ISession OpenSession()
        {
            var session = SessionFactory.OpenSession(new AuditInterceptor());
            return session;
        }

        public static IStatelessSession OpenStatelessSession()
        {
            return SessionFactory.OpenStatelessSession();
        }
    }
}
