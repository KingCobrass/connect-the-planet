using connect.Interface.User;
using Connect.Interface.Jwt;
using Connect.Interface.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Interface.Account
{
    public interface IAccountService
    {
        Task<IConnectResponse> SignupUser(IUser user);
        Task<IConnectRootResponse<IConnectToken>> GetLogin(string email);
    }
}
