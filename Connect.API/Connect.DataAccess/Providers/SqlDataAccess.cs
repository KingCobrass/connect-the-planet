using Connect.Interface.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Connect.DataAccess.Providers
{
    public class SqlDataAccess : IDatabaseHandler
    {
        private string ConnectionString { get; set; }

        public SqlDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public DbCommand CreateCommand(string commandText, CommandType commandType)
        {
            return new SqlCommand
            {
                CommandText = commandText,
                CommandType = commandType
            };
        }

        public DbParameter CreateParameter(IDbCommand command)
        {
            SqlCommand SQLcommand = (SqlCommand)command;
            return SQLcommand.CreateParameter();
        }
    }
}
