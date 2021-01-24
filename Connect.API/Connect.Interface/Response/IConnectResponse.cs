using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Response
{
    public interface IConnectResponse
    {
        string Status { get; set; }
        string Message { get; set; }
        string ResponseCode { get; set; }
    }
}
