using connect.Interface.User;
using Connect.Interface.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Interface.User
{
    public interface IUsersService
    {
        Task<IConnectResponse> UpdateUser(string email, IUser user);
        Task<IConnectResponse> DeleteUser(string email);
        Task<IConnectRootResponse<IUser>> GetUser(string email);
        Task<IConnectRootResponse<IResponseItem<IUser>>> GetUsers();
    }
}
