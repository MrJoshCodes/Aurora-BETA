using AuroraEmu.Config;
using AuroraEmu.Database.Pool;
using MySql.Data.MySqlClient;

namespace AuroraEmu.Database
{
    public class DatabaseManager
    {
        private readonly string connectionString =
            $"Server={ConfigLoader.GetInstance().DbConfig.Server};" +
            "Port=3306; " +
            $"Uid={ConfigLoader.GetInstance().DbConfig.User}; " +
            $"Password={ConfigLoader.GetInstance().DbConfig.Password}; " +
            $"Database={ConfigLoader.GetInstance().DbConfig.Database}; " +
            "Pooling=true;" +
            "MinimumPoolSize=5; " +
            "MaximumPoolSize=15";
        
        private ObjectPool<DatabaseConnection> connectionPool;
        private static DatabaseManager databaseManagerInstance;

        public DatabaseManager()
        {
            Init(connectionString);
        }

        public void Init(string connectionString)
        {
            connectionPool = new ObjectPool<DatabaseConnection>(() => new DatabaseConnection(connectionString, connectionPool));
        }

        public bool TryConnection()
        {
            try
            {
                using (var DbConnection = GetConnection())
                {
                    DbConnection.Open();
                    DbConnection.SetQuery("SELECT 1+1;");
                    DbConnection.Execute();
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
            return new DatabaseConnection(connectionString, connectionPool);
        }

        public static DatabaseManager GetInstance()
        {
            if (databaseManagerInstance == null)
                databaseManagerInstance = new DatabaseManager();
            return databaseManagerInstance;
        }
    }
}
