using Connect.Interface.Chat;
using Connect.Interface.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Services
{
    public class ConnectGroupInfoService : IConnectGroupInfoService
    {
        public Task<IConnectResponse> AddNewGroup(IConnectGroupInfo connectGroupInfo)
        {
            throw new NotImplementedException();
        }

        public Task<IConnectRootResponse<IConnectGroupInfo>> GetGroupInfo(List<string> memberIds)
        {
            throw new NotImplementedException();
        }

        public Task<IConnectRootResponse<IResponseItem<IConnectGroupInfo>>> GetGroupInfos()
        {
            throw new NotImplementedException();
        }

        public Task<IConnectResponse> UpdateGroup(string groupId, IConnectGroupInfo connectGroupInfo)
        {
            throw new NotImplementedException();
        }
    }
}
