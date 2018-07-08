using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace AuroraEmu.Database
{
    public class DatabaseConnection : IDisposable
    {
        private readonly MySqlConnection _connection;
        private readonly MySqlCommand _command;

        private MySqlTransaction _transaction;

        public DatabaseConnection(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
            _command = _connection.CreateCommand();
            _connection.Open();
        }

        public void Open()
        {
            _connection.Open();
        }

        public bool IsOpen()
        {
            return _connection.State == ConnectionState.Open;
        }

        /// <summary>
        /// Adds a parameter to the MySqlCommand.
        /// </summary>
        /// <param name="parameter">The parameter with prefixed with an '@'</param>
        /// <param name="value">The value of the parameter.</param>
        public void AddParameter(string parameter, object value)
        {
            _command.Parameters.AddWithValue(parameter, value);
        }

        public void AddParameters(MySqlParameter[] parameters)
        {
            _command.Parameters.AddRange(parameters);
        }

        public void SetQuery(string query)
        {
            _command.CommandText = query;
        }

        /// <summary>
        /// Executes a query.
        /// </summary>
        /// <returns>The number of rows affected.</returns>
        public int Execute()
        {
            try
            {
                return _command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Engine.Logger.Error("MySQL Error: ", ex);

                return -1;
            }
            finally
            {
                _command.CommandText = string.Empty;
                _command.Parameters.Clear();
            }
        }
        
        public MySqlDataReader ExecuteReader()
        {
            try
            {
                return _command.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                Engine.Logger.Error("MySQL Error: ", ex);

                return null;
            }
            finally
            {
                _command.CommandText = string.Empty;
                _command.Parameters.Clear();
            }
        }

        public string GetString()
        {
            try
            {
                return _command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Engine.Logger.Error(ex.Message);
                return string.Empty;
            }
        }

        public int GetInt()
        {
            return (int) _command.ExecuteScalar();
        }

        /// <summary>
        /// Executes an 'insert'-query. Instead of 'Execute', it returns the inserted ID rather than the amount of affected rows.
        /// </summary>
        /// <returns>The inserted ID.</returns>
        public int Insert()
        {
            try
            {
                _command.ExecuteNonQuery();

                return (int) _command.LastInsertedId;
            }
            catch (MySqlException ex)
            {
                Engine.Logger.Error("MySQL Error: ", ex);

                return -1;
            }
            finally
            {
                _command.CommandText = string.Empty;
                _command.Parameters.Clear();
            }
        }

        public void Dispose()
        {
            _transaction = _connection.BeginTransaction();
            _transaction.Commit();

            if (IsOpen())
                _connection.Close();

            _command.Parameters?.Clear();
            _transaction?.Dispose();
            _command.Dispose();
            Engine.Locator.ConnectionPool.ReturnConnection(this);
            GC.SuppressFinalize(this);
        }
    }
}