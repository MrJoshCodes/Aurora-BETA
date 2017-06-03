using AuroraEmu.Database.Pool;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace AuroraEmu.Database
{
    public class DatabaseConnection : IDisposable
    {
        private readonly ObjectPool<DatabaseConnection> objectPool;

        private MySqlConnection connection;
        private MySqlCommand command;

        private MySqlTransaction transaction;

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

        /// <summary>
        /// Adds a parameter to the MySqlCommand.
        /// </summary>
        /// <param name="parameter">The parameter with prefixed with an '@'</param>
        /// <param name="value">The value of the parameter.</param>
        public void AddParameter(string parameter, object value)
        {
            command.Parameters.AddWithValue(parameter, value);
        }
        
        public void SetQuery(string query)
        {
            command.CommandText = query;
        }

        /// <summary>
        /// Executes a query.
        /// </summary>
        /// <returns>The number of rows affected.</returns>
        public int Execute()
        {
            try
            {
                return command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Engine.Logger.Error("MySQL Error: ", ex);

                return -1;
            }
            finally
            {
                command.CommandText = string.Empty;
                command.Parameters.Clear();
            }
        }

        public DataSet GetDataSet()
        {
            try
            {
                DataSet dataSet = new DataSet();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataSet);
                }

                return dataSet;
            }
            catch (Exception ex)
            {
                Engine.Logger.Error("MySQL Error: ", ex);

                return null;
            }
        }

        public DataTable GetTable()
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                Engine.Logger.Error("MySQL Error: ", ex);

                return null;
            }
        }

        public DataRow GetRow()
        {
            try
            {
                DataRow row = null;
                DataSet dataSet = GetDataSet();

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count == 1)
                {
                    row = dataSet.Tables[0].Rows[0];
                }

                return row;
            }
            catch (Exception ex)
            {
                Engine.Logger.Error("MySQL Error: ", ex);

                return null;
            }
        }

        public string GetString()
        {
            return command.ExecuteScalar().ToString();
        }

        public int GetInt()
        {
            return (int)command.ExecuteScalar();
        }

        /// <summary>
        /// Executes an 'insert'-query. Instead of 'Execute', it returns the inserted ID rather than the amount of affected rows.
        /// </summary>
        /// <returns>The inserted ID.</returns>
        public int Insert()
        {
            try
            {
                command.ExecuteNonQuery();

                return (int)command.LastInsertedId;
            }
            catch (MySqlException ex)
            {
                Engine.Logger.Error("MySQL Error: ", ex);

                return -1;
            }
            finally
            {
                command.CommandText = string.Empty;
                command.Parameters.Clear();
            }
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
            if(IsOpen())
            {
                connection.Close();
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
