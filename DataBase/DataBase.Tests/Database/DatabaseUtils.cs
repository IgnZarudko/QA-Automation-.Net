using System.Data;
using DataBase.Tests.Config;
using MySql.Data.MySqlClient;

namespace DataBase.Tests.Database
{
    public class DatabaseUtils
    {
        private MySqlConnection _connection;
        
        public void CreateConnectionFromConfig(DatabaseConfig config)
        {
            string connectionString = $"server={config.Server};user={config.User};database={config.Database};password={config.Password};";
            _connection = new MySqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        public MySqlDataReader ExecuteQuery(string queryString)
        {
            MySqlCommand command = new MySqlCommand(queryString, _connection);
            return command.ExecuteReader();
        }
    }
}