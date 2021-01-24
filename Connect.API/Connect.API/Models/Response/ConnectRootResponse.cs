using Connect.Interface.Constants;
using Connect.Interface.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Models.Response
{
    public class ConnectRootResponse<T> : IConnectRootResponse<T>
    {
        
        public ConnectRootResponse()
        {
            Status = ConnectConstants.Failed;
        }
        /// <summary>
        /// This generic object contains response data
        /// </summary>
        public T ResponseData { get; set; }
        /// <summary>
        /// Request status. The value will be Success/Failed. If this value is success that means the request has success otherwise you should check message and response code to get the proper information regarding a request.
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Contains request success/failed message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// This value contains CP response code.
        /// </summary>
        public string ResponseCode { get; set; }
    }
}
