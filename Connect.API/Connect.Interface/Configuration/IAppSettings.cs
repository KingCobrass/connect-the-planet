using Connect.Interface.Database;
using Connect.Interface.Jwt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Configuration
{
    public interface IAppSettings
    {
        IConnectJwt ConnectJwt { get; set; }
        IDatabaseConnectionParams DatabaseConnection { get; set; }
    }
}
