using Connect.API.Infrastructure;
using Connect.API.Models.Chat;
using Connect.API.Models.Response;
using Connect.Interface.Chat;
using Connect.Interface.Constants;
using Connect.Interface.Logger;
using Connect.Interface.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConnectChatsController : ControllerBase
    {
        private readonly IConnectChatService _connectChatService;
        private readonly ICPLogger _cpLogger;
        public ConnectChatsController(IConnectChatService connectChatService, ICPLogger cpLogger)
        {
            this._connectChatService = connectChatService;
            this._cpLogger = cpLogger;
        }
        /// <summary>
        /// Publish message
        /// </summary>
        /// <remarks>
        /// All Possible Connect Planet API Response Code
        ///  <br>CP021: User Sign up/Update/Delete Success.</br>
        /// CP022: User Sign up/Update/Delete Failed.
        ///  <br>CP028: Email Already Exist!.</br>
        ///  CP001: Unexpected System Error.
        /// </remarks>
        /// <param name="connectMessage"></param>
        /// <returns></returns>
        [HttpPost("direct")]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<ActionResult<IConnectResponse>> PublishMessage([FromBody] ConnectMessage connectMessage)
        {
            IConnectResponse response = new ConnectResponse();
            try
            {
                this._cpLogger.LogInfo($">>[ConnectChatsController->PublishMessage][{connectMessage.UserId}] : START.");

                response = await this._connectChatService.PublishMessage(connectMessage);
                this._cpLogger.LogInfo($">> [ConnectChatsController->PublishMessage][{connectMessage.UserId}]: END, Response message: {response.Message}, code: {response.ResponseCode}");

                if (response.Status.Equals(ConnectConstants.Failed)) return BadRequest(response);
                else return Ok(response);

            }
            catch (Exception ex)
            {
                response.Message = ConnectResponseCodes.CP001_MESSAGE;
                response.ResponseCode = ConnectResponseCodes.CP001;
                this._cpLogger.LogError($">> [ConnectChatsController->PublishMessage][{connectMessage.UserId}]: Exception - {ex.Message}.");
                this._cpLogger.LogError($">> [ConnectChatsController->PublishMessage][{connectMessage.UserId}]: END, Response message: {response.Message}, code: {response.ResponseCode}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        /// <summary>
        /// Publish message
        /// </summary>
        /// <remarks>
        /// All Possible Connect Planet API Response Code
        ///  <br>CP021: User Sign up/Update/Delete Success.</br>
        /// CP022: User Sign up/Update/Delete Failed.
        ///  <br>CP028: Email Already Exist!.</br>
        ///  CP001: Unexpected System Error.
        /// </remarks>
        /// <param name="connectMessage"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<ActionResult<IConnectResponse>> PublishToGroupMessage([FromBody] ConnectMessage connectMessage)
        {
            IConnectResponse response = new ConnectResponse();
            try
            {
                this._cpLogger.LogInfo($">>[ConnectChatsController->PublishToGroupMessage][{connectMessage.UserId}] : START.");

                response = await this._connectChatService.PublishToGroupMessage(connectMessage);
                this._cpLogger.LogInfo($">> [ConnectChatsController->PublishToGroupMessage][{connectMessage.UserId}]: END, Response message: {response.Message}, code: {response.ResponseCode}");

                if (response.Status.Equals(ConnectConstants.Failed)) return BadRequest(response);
                else return Ok(response);

            }
            catch (Exception ex)
            {
                response.Message = ConnectResponseCodes.CP001_MESSAGE;
                response.ResponseCode = ConnectResponseCodes.CP001;
                this._cpLogger.LogError($">> [ConnectChatsController->PublishToGroupMessage][{connectMessage.UserId}]: Exception - {ex.Message}.");
                this._cpLogger.LogError($">> [ConnectChatsController->PublishToGroupMessage][{connectMessage.UserId}]: END, Response message: {response.Message}, code: {response.ResponseCode}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
