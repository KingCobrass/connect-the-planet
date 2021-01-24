using Connect.Interface.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Interface.Chat
{
    public interface IConnectChatService
    {
        Task<IConnectResponse> PublishMessage(IConnectMessage connectMessage);
        Task<IConnectResponse> PublishToGroupMessage(IConnectMessage connectMessage);
    }
}
