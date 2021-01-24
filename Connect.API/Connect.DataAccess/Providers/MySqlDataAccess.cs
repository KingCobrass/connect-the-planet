using Connect.Interface.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Connect.DataAccess.Providers
{
    public class MySqlDataAccess : IDatabaseHandler
    {
        private string ConnectionString { get; set; }

        public MySqlDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DbConnection CreateConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        public DbCommand CreateCommand(string commandText, CommandType commandType)
        {
            return new MySqlCommand
            {
                CommandText = commandText,
                CommandType = commandType

            };
        }

        public DbParameter CreateParameter(IDbCommand command)
        {
            MySqlCommand SQLcommand = (MySqlCommand)command;
            return SQLcommand.CreateParameter();
        }
    }
}
