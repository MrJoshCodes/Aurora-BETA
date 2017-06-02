using AuroraEmu.Database.Pool;
using MySql.Data.MySqlClient;

namespace AuroraEmu.Database
{
    public class DatabaseManager
    {
        private const string CONNECTION_STRING = 
            "Server=127.0.0.1; " +
            "Port=3306; " +
            "Uid=root; " +
            "Password=123; " +
            "Database=aurora_beta; " +
            "Pooling=true;" +
            "MinimumPoolSize=5; " +
            "MaximumPoolSize=15";
        
        private ObjectPool<DatabaseConnection> connectionPool;
        private static DatabaseManager databaseManagerInstance;

        public DatabaseManager()
        {
            Init(CONNECTION_STRING);
        }

        public void Init(string connectionString)
        {
            connectionPool = new ObjectPool<DatabaseConnection>(() => new DatabaseConnection(CONNECTION_STRING, connectionPool));
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
            return new DatabaseConnection(CONNECTION_STRING, connectionPool);
        }

        public static DatabaseManager GetInstance()
        {
            if (databaseManagerInstance == null)
                databaseManagerInstance = new DatabaseManager();
            return databaseManagerInstance;
        }
    }
}
