using connect.Interface.User;
using Connect.API.Models;
using Connect.API.Models.Configuration;
using Connect.API.Models.Response;
using Connect.Interface.Account;
using Connect.Interface.Constants;
using Connect.Interface.Database;
using Connect.Interface.Jwt;
using Connect.Interface.Logger;
using Connect.Interface.Response;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Connect.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConnectDatabaseRepository _connectDatabaseRepository;
        private readonly ICPLogger _cpLogger;
        private readonly AppSettings _appSettings;
        public AccountService(
            IConnectDatabaseRepository connectDatabaseRepository,
            IOptions<AppSettings> options,
            ICPLogger cpLogger)
        {
            this._connectDatabaseRepository = connectDatabaseRepository;
            this._appSettings = options.Value;
            this._cpLogger = cpLogger;
        }
        public async Task<IConnectResponse> SignupUser(IUser user)
        {
            try
            {
                IConnectResponse response = new ConnectResponse();
                var isUserExist = await this._connectDatabaseRepository.IsUserExistAsync(user.Email);
                if (!isUserExist)
                {
                    var dbResponse = await this._connectDatabaseRepository.AddUserAsync(user);
                    if (dbResponse)
                    {
                        response.Status = ConnectConstants.Success;
                        response.Message = ConnectResponseCodes.CP021_MESSAGE;
                        response.ResponseCode = ConnectResponseCodes.CP021;
                    }
                    else
                    {
                        response.Message = ConnectResponseCodes.CP022_MESSAGE;
                        response.ResponseCode = ConnectResponseCodes.CP022;
                    }
                }
                else
                {
                    response.Message = ConnectResponseCodes.CP028_MESSAGE;
                    response.ResponseCode = ConnectResponseCodes.CP028;
                }
                return response;
            }
            catch (Exception) { throw; }
        }
        public async Task<IConnectRootResponse<IConnectToken>> GetLogin(string email)
        {
            try
            {
                IConnectRootResponse<IConnectToken> response = new ConnectRootResponse<IConnectToken>();
                IConnectToken resposeToken = new ConnectToken();

                var userInfo = await this._connectDatabaseRepository.GetUserAsync(email);
                if (userInfo.Status.Equals(ConnectConstants.Success))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(this._appSettings.ConnectJwt.ApiSecret);

                    DateTime expiryTime = DateTime.UtcNow.AddSeconds(this._appSettings.ConnectJwt.AccessTokenExpireTime);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.Name,userInfo.ResponseData.Name),
                        new Claim(ClaimTypes.Email,userInfo.ResponseData.Email)
                        }),
                        Expires = expiryTime,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);


                    this._cpLogger.LogInfo($"AccountService->GetLogin-> Success for the email:: {email}");

                    resposeToken.ExpiresIn = this._appSettings.ConnectJwt.AccessTokenExpireTime;
                    resposeToken.TokenType = "bearer";
                    resposeToken.AccessToken = tokenString;

                    response.Status = ConnectConstants.Success;
                    response.Message = ConnectResponseCodes.CP026_MESSAGE;
                    response.ResponseCode = ConnectResponseCodes.CP026;
                    response.ResponseData = resposeToken;
                }
                else
                {
                    response.Message = ConnectResponseCodes.CP029_MESSAGE;
                    response.ResponseCode = ConnectResponseCodes.CP029;
                }

                return response;
            }
            catch (Exception) { throw; }
        }
    }
}

