using Connect.DataAccess.Providers;
using Connect.Interface.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.DataAccess
{
    public class DatabaseHandlerFactory
    {
        private readonly IDatabaseConnectionParams _databaseConnectionParams;

        public DatabaseHandlerFactory(IDatabaseConnectionParams databaseConnectionParams)
        {
            this._databaseConnectionParams = databaseConnectionParams;
        }

        public IDatabaseHandler CreateDatabase()
        {
            IDatabaseHandler database = null;

            switch (this._databaseConnectionParams.GetProvider())
            {
                case DatabaseConenctionProviders.SQLServerProvider:
                    database = new SqlDataAccess(this._databaseConnectionParams.GetConnectionString());
                    break;
                case DatabaseConenctionProviders.MySQLProvider:
                    database = new MySqlDataAccess(this._databaseConnectionParams.GetConnectionString());
                    break;
                case DatabaseConenctionProviders.PostgreSQLProvider:
                    database = new PostgresDataAccess(this._databaseConnectionParams.GetConnectionString());
                    break;
                default:
                    database = new SqlDataAccess(this._databaseConnectionParams.GetConnectionString());
                    break;
            }

            return database;
        }
    }
}
