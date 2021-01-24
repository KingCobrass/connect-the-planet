using connect.Interface.User;
using Connect.API.Models.Response;
using Connect.Interface.Constants;
using Connect.Interface.Database;
using Connect.Interface.Logger;
using Connect.Interface.Response;
using Connect.Interface.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IConnectDatabaseRepository _connectDatabaseRepository;
        private readonly ICPLogger _cpLogger;
        public UsersService(
            IConnectDatabaseRepository connectDatabaseRepository,
            ICPLogger cpLogger)
        {
            this._connectDatabaseRepository = connectDatabaseRepository;
            this._cpLogger = cpLogger;
        }
       
        public async Task<IConnectRootResponse<IUser>> GetUser(string email)
        {
            try
            {
                return await this._connectDatabaseRepository.GetUserAsync(email);
            }
            catch (Exception) { throw; }
        }

        public async Task<IConnectRootResponse<IResponseItem<IUser>>> GetUsers()
        {
            try
            {
                IConnectRootResponse<IResponseItem<IUser>> response = new ConnectRootResponse<IResponseItem<IUser>>();

                var dbResponse = await this._connectDatabaseRepository.GetAllUsersAsync();
                if (dbResponse.Items?.Count > 0)
                {
                    response.Status = ConnectConstants.Success;
                    response.Message = ConnectResponseCodes.CP024_MESSAGE;
                    response.ResponseCode = ConnectResponseCodes.CP024;
                    response.ResponseData = dbResponse;
                }
                else
                {
                    response.Message = ConnectResponseCodes.CP023_MESSAGE;
                    response.ResponseCode = ConnectResponseCodes.CP023;

                }
                return response;
            }
            catch (Exception) { throw; }
        }

        public Task<IConnectResponse> UpdateUser(string email, IUser user)
        {
            throw new NotImplementedException();
        }
        public Task<IConnectResponse> DeleteUser(string email)
        {
            throw new NotImplementedException();
        }
    }
}
