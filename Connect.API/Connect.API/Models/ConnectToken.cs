using Connect.Interface.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Models
{
    public class ConnectToken : IConnectToken
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public string TokenType { get; set; }
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
    }
}
