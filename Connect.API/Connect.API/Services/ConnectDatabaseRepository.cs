using connect.Interface.User;
using Connect.API.Models;
using Connect.API.Models.Configuration;
using Connect.API.Models.Response;
using Connect.DataAccess;
using Connect.Interface.Configuration;
using Connect.Interface.Constants;
using Connect.Interface.Database;
using Connect.Interface.Response;
using Connect.Interface.ServerException;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Services
{
    public class ConnectDatabaseRepository : DBContext, IConnectDatabaseRepository
    {
        public ConnectDatabaseRepository(IOptions<AppSettings> options) : base(options.Value.DatabaseConnection) { }

        public async Task<bool> AddUserAsync(IUser user)
        {
            bool success = false;
            try
            {


                string commandText = @"INSERT INTO USERS (ID, NAME, FIRSTNAME, LASTNAME, EMAIL, CREATEDATE, ISLIVE, ISACTIVE) VALUES (@ID, @NAME, @FIRSTNAME, @LASTNAME, @EMAIL, @CREATEDATE, @ISLIVE, @ISACTIVE);";
                using var command = this.GetCommand(commandText, CommandType.Text);

                user.Id = Guid.NewGuid().ToString();
                user.CreateDate = DateTime.UtcNow;
                user.Name = $"{user.FirstName} {user.lastName}";

                this.CreateParameter(command, "@ID", user.Id, DbType.String);
                this.CreateParameter(command, "@NAME", user.Name, DbType.String);
                this.CreateParameter(command, "@FIRSTNAME", user.FirstName, DbType.String);
                this.CreateParameter(command, "@LASTNAME", user.lastName, DbType.String);
                this.CreateParameter(command, "@EMAIL", user.Email, DbType.String);
                this.CreateParameter(command, "@CREATEDATE", user.CreateDate, DbType.DateTime);

                this.CreateParameter(command, "@ISLIVE", 0, DbType.Int16);
                this.CreateParameter(command, "@ISACTIVE", 1, DbType.Int16);

                success = await this.InsertAsync(command);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex);
            }
            return success;


        }

        public async Task<bool> IsUserExistAsync(string email)
        {
            try
            {
                string commandText = @"SELECT EMAIL FROM USERS WHERE EMAIL = @EMAIL;";
                DbCommand command = this.GetCommand(commandText, CommandType.Text);

                this.CreateParameter(command, "@EMAIL", email, DbType.String);

                string emailInfo = await this.GetScalarValue<string>(command);

                return string.IsNullOrEmpty(emailInfo) ? false : true;
                /*
                IConnectResponse response = new ConnectResponse();

                if (!string.IsNullOrEmpty(emailInfo))
                {
                    response.Status = ConnectConstants.Success;
                    response.Message = ConnectResponseCodes.CP024_MESSAGE;
                    response.ResponseCode = ConnectResponseCodes.CP024;
                }
                else
                {
                    response.Message = ConnectResponseCodes.CP023_MESSAGE;
                    response.ResponseCode = ConnectResponseCodes.CP023;
                }
                */


            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex);
            }
        }
        public async Task<IResponseItem<IUser>> GetAllUsersAsync()
        {
            IResponseItem<IUser> response = new ResponseItem<IUser>();
            DbConnection connection = null;
            DbDataReader reader;

            IUser user;

            try
            {
                string commandText = @"SELECT * FROM USERS";
                using var command = this.GetCommand(commandText, CommandType.Text);

                (connection, reader) = await this.ExecuteReaderAsync(command);

                while (await reader.ReadAsync())
                {
                    user = new User
                    {
                        Id = DBNullExt.ToValue<string>(reader["Id"]),
                        Name = DBNullExt.ToValue<string>(reader["NAME"]),

                        FirstName = DBNullExt.ToValue<string>(reader["FIRSTNAME"]),
                        lastName = DBNullExt.ToValue<string>(reader["LASTNAME"]),
                        Email = DBNullExt.ToValue<string>(reader["EMAIL"]),
                        CreateDate = DBNullExt.ToValue<DateTime>(reader["CREATEDATE"]),
                        ModifyDate = DBNullExt.ToValue<DateTime>(reader["MODIFYDATE"]),
                        LastSignInDate = DBNullExt.ToValue<DateTime>(reader["LASTSIGNINDATE"]),
                        ConnectionId = DBNullExt.ToValue<string>(reader["CONNECTIONID"]),
                        //Token = DBNullExt.ToValue<string>(reader["TOKEN"]),
                        IsLive = DBNullExt.ToValue<int>(reader["ISLIVE"]),
                        IsActive = DBNullExt.ToValue<int>(reader["ISACTIVE"])
                    };

                    response.Items.Add(user);
                }

                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex);
            }
            finally
            {
                if (connection?.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }

            return response;
        }
        public async Task<IConnectRootResponse<IUser>> GetUserAsync(string email)
        {
            IConnectRootResponse<IUser> response = new ConnectRootResponse<IUser>();
            DbConnection connection = null;
            DbDataReader reader;

            IUser user = null;

            try
            {
                string commandText = @"SELECT * FROM USERS WHERE EMAIL=@EMAIL";
                using var command = this.GetCommand(commandText, CommandType.Text);
                this.CreateParameter(command, "@EMAIL", email, DbType.String);

                (connection, reader) = await this.ExecuteReaderAsync(command);

                if (await reader.ReadAsync())
                {
                    user = new User
                    {
                        Id = DBNullExt.ToValue<string>(reader["Id"]),
                        Name = DBNullExt.ToValue<string>(reader["NAME"]),

                        FirstName = DBNullExt.ToValue<string>(reader["FIRSTNAME"]),
                        lastName = DBNullExt.ToValue<string>(reader["LASTNAME"]),
                        Email = DBNullExt.ToValue<string>(reader["EMAIL"]),
                        CreateDate = DBNullExt.ToValue<DateTime>(reader["CREATEDATE"]),
                        ModifyDate = DBNullExt.ToValue<DateTime>(reader["MODIFYDATE"]),
                        LastSignInDate = DBNullExt.ToValue<DateTime>(reader["LASTSIGNINDATE"]),
                        ConnectionId = DBNullExt.ToValue<string>(reader["CONNECTIONID"]),
                        //Token = DBNullExt.ToValue<string>(reader["TOKEN"]),
                        IsLive = DBNullExt.ToValue<int>(reader["ISLIVE"]),
                        IsActive = DBNullExt.ToValue<int>(reader["ISACTIVE"])
                    };
                }
                await reader.CloseAsync();

                if (user != null)
                {
                    response.Status = ConnectConstants.Success;
                    response.Message = ConnectResponseCodes.CP024_MESSAGE;
                    response.ResponseCode = ConnectResponseCodes.CP024;
                    response.ResponseData = user;
                }
                else
                {
                    response.Status = ConnectConstants.Failed;
                    response.Message = ConnectResponseCodes.CP023_MESSAGE;
                    response.ResponseCode = ConnectResponseCodes.CP023;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex);
            }
            finally
            {
                if (connection?.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }

            return response;
        }

    }
}
