using Connect.Interface.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Connect.DataAccess
{
    public abstract class DBContext
    {
        private readonly IDatabaseHandler database;
        public DBContext(IDatabaseConnectionParams databaseConnectionParams)
        {
            DatabaseHandlerFactory factory = new DatabaseHandlerFactory(databaseConnectionParams);
            database = factory.CreateDatabase();
        }

        public DbConnection GetDatabasecOnnection()
        {
            return database.CreateConnection();
        }

        public DbCommand GetCommand(string commandText, CommandType commandType)
        {
            return database.CreateCommand(commandText, commandType);
        }

        public void CreateParameter(DbCommand command, string name, object value, DbType dbType)
        {
            var param = command.CreateParameter();
            param.DbType = dbType;
            param.ParameterName = name;
            param.Value = value;

            command.Parameters.Add(param);
        }

        public async Task<(DbConnection, DbDataReader)> ExecuteReaderAsync(DbCommand command)
        {
            try
            {
                var connection = database.CreateConnection();
                await connection.OpenAsync();
                command.Connection = connection;
                var reader = await command.ExecuteReaderAsync();

                return (connection, reader);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> DeleteAsync(DbCommand command)
        {
            try
            {
                using var connection = database.CreateConnection();
                await connection.OpenAsync();
                command.Connection = connection;
                int affected = await command.ExecuteNonQueryAsync();
                return affected > 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> InsertAsync(DbCommand command)
        {
            try
            {
                using var connection = database.CreateConnection();
                await connection.OpenAsync();
                command.Connection = connection;
                int affected = await command.ExecuteNonQueryAsync();
                return affected > 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> UpdateAsync(DbCommand command)
        {
            try
            {
                using var connection = database.CreateConnection();
                await connection.OpenAsync();
                command.Connection = connection;
                int affected = await command.ExecuteNonQueryAsync();
                return affected > 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<T> GetScalarValue<T>(DbCommand command)
        {
            try
            {
                using var connection = database.CreateConnection();
                await connection.OpenAsync();
                command.Connection = connection;
                object objValue = await command.ExecuteScalarAsync();
                if (objValue != null && objValue != DBNull.Value)
                    return (T)Convert.ChangeType(objValue, typeof(T));
                else
                    return default;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
