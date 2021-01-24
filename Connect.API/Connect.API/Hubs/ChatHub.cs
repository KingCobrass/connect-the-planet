using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Hubs
{
    public class ChatHub: Hub
    {
  
        public Task SendMessage(string userName, string message)
        {
            return Clients.All.SendAsync("ReceiveOne", userName, message);
        }
       
    }
}
