using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Response
{
    public interface IConnectRootResponse<T> : IConnectResponse
    {
        T ResponseData { get; set; }
    }
}
