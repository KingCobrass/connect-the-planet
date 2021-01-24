using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Database
{
    public class DatabaseConnectionParams : IDatabaseConnectionParams
    {
        public string DatabaseProvider { get; set; }
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public int ConnectionTimeOut { get; set; }
        public int CommandTimeOut { get; set; }

        public string GetConnectionString()
        {
            var connectionString = this.DatabaseProvider switch
            {
                DatabaseConenctionProviders.MySQLProvider =>
                        $"Server={this.Server};Database={this.DatabaseName};Uid={this.UserID};Pwd={this.Password}",
                DatabaseConenctionProviders.PostgreSQLProvider =>
                        $"Server={this.Server};Port={this.Port};Database={this.DatabaseName};User Id={this.UserID};Password={this.Password};Pooling=true;MaxPoolSize=200;CommandTimeout={this.CommandTimeOut};Connection Idle Lifetime={this.ConnectionTimeOut};",
                DatabaseConenctionProviders.SQLServerProvider =>
                        $"Server={this.Server};Database={this.DatabaseName};User ID={this.UserID};Password={this.Password};Max Pool Size=200;",

                _ => string.Empty

            };

            return connectionString;
        }

        public string GetProvider()
        {
            return this.DatabaseProvider;
        }
    }
}
