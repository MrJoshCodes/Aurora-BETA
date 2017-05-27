using AuroraEmu.Database.Pool;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraEmu.Database
{
    public class DatabaseConnection : IDisposable
    {
        private readonly ObjectPool<DatabaseConnection> objectPool;

        private MySqlConnection connection;
        private MySqlCommand command;

        private MySqlTransaction transaction;
        private List<MySqlParameter> parameters;

        public DatabaseConnection(string connectionString, ObjectPool<DatabaseConnection> pool)
        {
            objectPool = pool;
            connection = new MySqlConnection(connectionString);
            command = connection.CreateCommand();
        }

        public void Open()
        {
            if(connection.State == ConnectionState.Open)
            {
                throw new InvalidOperationException("Connection is already opened...");
            }
            connection.Open();
        }

        public bool IsOpen()
        {
            return connection.State == ConnectionState.Open;
        }

        public void AddParameter(string parameter, object value)
        {
            command.Parameters.AddWithValue(parameter, value);
        }

        public void WriteQuery(string query)
        {
            command.CommandText = query;
        }

        public int ExecuteNonQuery()
        {
            if (parameters != null && parameters.Count > 0)
                parameters.AddRange(parameters.ToArray());

            try
            {
                return this.command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Engine.Logger.Error("MySQL Error: ", ex);
                throw ex;
            }
            finally
            {
                command.CommandText = string.Empty;
                command.Parameters.Clear();

                if (parameters != null && parameters.Count > 0)
                    parameters.Clear();
            }
        }

        public DataRow GetRow()
        {
            DataRow row = null;
            try
            {
                DataSet dataSet = new DataSet();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataSet);
                }
                if ((dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count == 1))
                {
                    row = dataSet.Tables[0].Rows[0];
                }
            }
            catch (Exception exception)
            {
                //TODO:
            }

            return row;
        }

        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            if (transaction == null)
                throw new InvalidOperationException("Transaction hasn't started yet.");
            transaction.Commit();
        }

        public void Rollback()
        {
            if (transaction == null)
                throw new InvalidOperationException("Transaction hasn't started yet.");
            transaction.Rollback();
        }

        public void Dispose()
        {
            if(connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            if(parameters != null)
            {
                parameters.Clear();
            }

            if(transaction != null)
            {
                transaction.Dispose();
            }

            if(command != null)
            {
                command.Dispose();
            }
        }
    }
}
