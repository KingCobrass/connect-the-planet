using Connect.Interface.Configuration;
using Connect.Interface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Models.Configuration
{
    public class AppSettings
    {
        public ConnectJwt ConnectJwt { get; set; }
        public DatabaseConnectionParams DatabaseConnection { get; set; }
    }
}
