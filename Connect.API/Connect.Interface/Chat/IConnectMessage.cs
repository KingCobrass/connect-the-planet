using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Chat
{
    public interface IConnectMessage
    {
        long Id { get; set; }
        string UserName { get; set; }
        string UserId { get; set; }
        string Message { get; set; }
        DateTime MessageDate { get; set; }
        DateTime? ModifyDate { get; set; }
        string GroupId { get; set; }
        string ToConnectionId { get; set; }
        int IsActive { get; set; }
    }
}
