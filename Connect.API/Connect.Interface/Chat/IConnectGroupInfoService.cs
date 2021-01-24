using Connect.Interface.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Interface.Chat
{
    public interface IConnectGroupInfoService
    {
        Task<IConnectResponse> AddNewGroup(IConnectGroupInfo connectGroupInfo);
        Task<IConnectResponse> UpdateGroup(string groupId, IConnectGroupInfo connectGroupInfo);
        Task<IConnectRootResponse<IConnectGroupInfo>> GetGroupInfo(List<string> memberIds);
        Task<IConnectRootResponse<IResponseItem<IConnectGroupInfo>>> GetGroupInfos();

    }
}
