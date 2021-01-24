using Connect.Interface.Configuration;
using Connect.Interface.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Models.Configuration
{
    public class ConnectJwt : IConnectJwt
    {
        public string ApiSecret { get; set; }
        public double AccessTokenExpireTime { get; set; }
        public double AccessTokenLongExpireTime { get; set; }
    }
}
