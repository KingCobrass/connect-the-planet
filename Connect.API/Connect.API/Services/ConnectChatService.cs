using Connect.API.Hubs;
using Connect.API.Models.Response;
using Connect.Interface.Chat;
using Connect.Interface.Constants;
using Connect.Interface.Response;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Services
{
    public class ConnectChatService : IConnectChatService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IUserIdProvider _userIdProvider;
        public ConnectChatService(IHubContext<ChatHub> hubContext, IUserIdProvider userIdProvider)
        {
            this._hubContext = hubContext;
            this._userIdProvider = userIdProvider;
        }
        public async Task<IConnectResponse> PublishMessage(IConnectMessage connectMessage)
        {
            try
            {
                await this._hubContext.Clients.All.SendAsync("ReceiveGroupMessage", connectMessage.UserName, connectMessage.Message);
                //this._userIdProvider.GetUserId(connectMessage.Email);
                //await this._hubContext.Clients.Client(connectMessage.ToEmail).SendAsync("ReceiveDirectMessage", connectMessage.FromEmail, connectMessage.ToEmail, connectMessage.Message);
                return new ConnectResponse
                {
                    Status = ConnectConstants.Success,
                    Message = ConnectResponseCodes.CP041_MESSAGE,
                    ResponseCode = ConnectResponseCodes.CP041
                };
            }
            catch (Exception) { throw; }
        }
        public async Task<IConnectResponse> PublishToGroupMessage(IConnectMessage connectMessage)
        {
            try
            {
                await this._hubContext.Clients.All.SendAsync("ReceiveGroupMessage", connectMessage.UserName, connectMessage.Message);
                return new ConnectResponse
                {
                    Status = ConnectConstants.Success,
                    Message = ConnectResponseCodes.CP041_MESSAGE,
                    ResponseCode = ConnectResponseCodes.CP041
                };
            }
            catch (Exception) { throw; }
        }
    }
}
