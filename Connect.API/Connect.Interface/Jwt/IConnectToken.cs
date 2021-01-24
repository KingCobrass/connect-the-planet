using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Jwt
{

    /// <summary>
    /// 
    /// </summary>
    public interface IConnectToken
    {
        /// <summary>
        /// Access token
        /// </summary>
        string AccessToken { get; set; }
        /// <summary>
        /// Expires in seconds
        /// </summary>
        double ExpiresIn { get; set; }
        /// <summary>
        /// Token type
        /// </summary>
        string TokenType { get; set; }
        /// <summary>
        /// Error while generating/validating token
        /// </summary>
        string Error { get; set; }
        /// <summary>
        /// Error description if there any error occurred while generating/validating token
        /// </summary>
        string ErrorDescription { get; set; }
    }
}
