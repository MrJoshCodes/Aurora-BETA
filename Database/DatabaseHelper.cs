using AuroraEmu.Database.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace AuroraEmu.Database
{
    public class DatabaseHelper
    {
        private static DatabaseHelper databaseHelperInstance;
        private const string CONNECTION_STRING = "Server=127.0.0.1; " +
                                                 "Port=3306; " +
                                                 "Uid=root; " +
                                                 "Password=; " +
                                                 "Database=aurora_beta; " +
                                                 "MinimumPoolSize=5; " +
                                                 "MaximumPoolSize=15";

        public ISessionFactory SessionFactory { get; private set; }

        public DatabaseHelper()
        {
            try
            {
                SessionFactory = Fluently.Configure()
                    .Database(MySQLConfiguration.Standard.ConnectionString(CONNECTION_STRING))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PlayerMap>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CatalogPageMap>())
                    .ExposeConfiguration(config =>
                    {
                        new SchemaUpdate(config).Execute(false, true);
                    })
                    .BuildSessionFactory();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        public static DatabaseHelper GetInstance()
        {
            if (databaseHelperInstance == null)
                databaseHelperInstance = new DatabaseHelper();
            return databaseHelperInstance;
        }
    }
}
