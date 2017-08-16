using AuroraEmu.Database.Pool;
using AuroraEmu.DI.Database;
using MySql.Data.MySqlClient;

namespace AuroraEmu.Database
{
    public class DatabaseController : IDatabaseController
    {
        private readonly string _connectionString =
            $"Server={Engine.MainDI.ConfigController.DbConfig.Server};" +
            "Port=3306; " +
            $"Uid={Engine.MainDI.ConfigController.DbConfig.User}; " +
            $"Password={Engine.MainDI.ConfigController.DbConfig.Password}; " +
            $"Database={Engine.MainDI.ConfigController.DbConfig.Database}; " +
            "Pooling=true;" +
            "MinimumPoolSize=5; " +
            "MaximumPoolSize=15";
        
        private ObjectPool<DatabaseConnection> _connectionPool;

        public DatabaseController()
        {
            Init(_connectionString);
        }

        public void Init(string connectionString)
        {
            _connectionPool = new ObjectPool<DatabaseConnection>(() => new DatabaseConnection(connectionString, _connectionPool));
        }

        public bool TryConnection()
        {
            try
            {
                using (var dbClient = GetConnection())
                {
                    dbClient.Open();
                    dbClient.SetQuery("SELECT 1+1;");
                    dbClient.Execute();
                    
                    dbClient.BeginTransaction();
                    dbClient.Commit();
                    dbClient.Dispose();
                }
            }
            catch (MySqlException)
            {
                return false;
            }
            return true;
        }

        public DatabaseConnection GetConnection()
        {
            return _connectionPool.GetObject() ?? new DatabaseConnection(_connectionString, _connectionPool);
        }
    }
}
