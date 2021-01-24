using connect.Interface.User;
using Connect.API.Infrastructure;
using Connect.API.Models;
using Connect.API.Models.Response;
using Connect.Interface.Constants;
using Connect.Interface.Logger;
using Connect.Interface.Response;
using Connect.Interface.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Controllers
{
    /// <summary>
    /// Handle all kinds of Users business
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(ConnectConstants.CORSS_POLICY_NAME)]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ICPLogger _cpLogger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usersService"></param>
        /// <param name="cpLogger"></param>
        public UsersController(
            IUsersService usersService,
        ICPLogger cpLogger)
        {
            this._usersService = usersService;
            this._cpLogger = cpLogger;
        }
       
        /// <summary>
        /// Returns user against given email address.
        /// You have to put valid email address with maximum length 200.
        /// </summary>
        /// <remarks>
        /// All Possible Connect Planet API Response Code
        ///  <br>CP023: User Not Found.</br>
        ///  CP001: Unexpected System Error.
        /// </remarks>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("{email}")]
        [Authorize]
        public async Task<ActionResult<IConnectRootResponse<IUser>>> GetUser([Required, EmailAddress, StringLength(200)] string email)
        {

            IConnectRootResponse<IUser> response = new ConnectRootResponse<IUser>();

            try
            {
                this._cpLogger.LogInfo($">>[UsersController->GetUser][Email: {email}]: START.");

                response = await this._usersService.GetUser(email);
                this._cpLogger.LogInfo($">> [UsersController->GetUser][Email: {email}]: END, Response message: {response.Message}, code: {response.ResponseCode}");

                if (response.Status.Equals(ConnectConstants.Failed)) return BadRequest(response);
                else return Ok(response);


            }
            catch (Exception ex)
            {
                response.Message = ConnectResponseCodes.CP001_MESSAGE;
                response.ResponseCode = ConnectResponseCodes.CP001;
                this._cpLogger.LogError($">> [UsersController->GetUser][Email: {email}]: Exception - {ex.Message}.");
                this._cpLogger.LogError($">> [UsersController->GetUser][Email: {email}]: END, Response message: {response.Message}, code: {response.ResponseCode}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        /// <summary>
        /// Returns all users including inactive users 
        /// </summary>
        /// <remarks>
        /// All Possible Connect Planet API Response Code
        ///  <br>CP024: User Retriving Success.</br>
        ///  CP025: User Retriving Failed.
        ///  <br>CP001: Unexpected System Error.</br>
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<ActionResult<IConnectRootResponse<IResponseItem<IUser>>>> GetUsers()
        {
            IConnectRootResponse<IResponseItem<IUser>> response = new ConnectRootResponse<IResponseItem<IUser>>();
            try
            {
                this._cpLogger.LogInfo($">>[UsersController->GetUsers] : START.");

                response = await this._usersService.GetUsers();
                this._cpLogger.LogInfo($">> [UsersController->GetUsers]: END, Response message: {response.Message}, code: {response.ResponseCode}");

                if (response.Status.Equals(ConnectConstants.Failed)) return BadRequest(response);
                else return Ok(response);

            }
            catch (Exception ex)
            {
                response.Message = ConnectResponseCodes.CP001_MESSAGE;
                response.ResponseCode = ConnectResponseCodes.CP001;
                this._cpLogger.LogError($">> [UsersController->GetUsers]: Exception - {ex.Message}.");
                this._cpLogger.LogError($">> [UsersController->GetUsers]: END, Response message: {response.Message}, code: {response.ResponseCode}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
