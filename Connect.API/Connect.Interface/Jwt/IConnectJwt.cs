using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Jwt
{
    public interface IConnectJwt
    {
        string ApiSecret { get; set; }
        double AccessTokenExpireTime { get; set; }
        double AccessTokenLongExpireTime { get; set; }
    }
}

