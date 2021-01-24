using connect.Interface.User;
using Connect.Interface.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Interface.Database
{
    public interface IConnectDatabaseRepository
    {
        Task<bool> AddUserAsync(IUser user);
        Task<bool> IsUserExistAsync(string email);
        Task<IConnectRootResponse<IUser>> GetUserAsync(string email);
        Task<IResponseItem<IUser>> GetAllUsersAsync();
    }
}
