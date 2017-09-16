using AuroraEmu.DI.Database;
using System.Collections.Generic;

namespace AuroraEmu.Database
{
    public class ConnectionPool : IConnectionPool
    {
        private Stack<DatabaseConnection> connections;
        private readonly string _connectionString =
            $"Server={Engine.MainDI.ConfigController.DbConfig.Server};" +
            $"Port={Engine.MainDI.ConfigController.DbConfig.Port}; " +
            $"Uid={Engine.MainDI.ConfigController.DbConfig.User}; " +
            $"Password={Engine.MainDI.ConfigController.DbConfig.Password}; " +
            $"Database={Engine.MainDI.ConfigController.DbConfig.Database}; " +
            "Pooling=true;" +
            "MinimumPoolSize=5; " +
            "MaximumPoolSize=15";

        public ConnectionPool()
        {
             connections = new Stack<DatabaseConnection>();
        }

        public DatabaseConnection PopConnection()
        {
            if (connections.Count == 0)
                return new DatabaseConnection(_connectionString);
            DatabaseConnection connection = connections.Pop();
            connection.Open();
            return connection;
        }

        public void ReturnConnection(DatabaseConnection con)
        {
            connections.Push(con);
        }
    }
}
