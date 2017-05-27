using AuroraEmu.Database.Pool;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraEmu.Database
{
    public class DatabaseManager
    {
        private string connectionString;
        private ObjectPool<DatabaseConnection> connectionPool;
        private static DatabaseManager databaseManagerInstance;

        public DatabaseManager()
        {
            this.Init(connectionString);
        }

        public void Init(string connectionString)
        {
            this.connectionString = connectionString;
            connectionPool = new ObjectPool<DatabaseConnection>(() => new DatabaseConnection(this.connectionString, connectionPool));
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
