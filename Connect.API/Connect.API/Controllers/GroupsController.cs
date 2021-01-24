using Connect.API.Infrastructure;
using Connect.API.Models.Chat;
using Connect.API.Models.Response;
using Connect.Interface.Chat;
using Connect.Interface.Constants;
using Connect.Interface.Logger;
using Connect.Interface.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Controllers
{
    /// <summary>
    /// Deal with all kinds of Meassaging groups business. It could be two to hundreads users
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IConnectGroupInfoService _connectGroupInfoService;
        private readonly ICPLogger _cpLogger;
        public GroupsController(IConnectGroupInfoService connectGroupInfoService, ICPLogger cpLogger)
        {
            this._connectGroupInfoService = connectGroupInfoService;
            this._cpLogger = cpLogger;
        }

        /// <summary>
        /// Create group to start messaging
        /// </summary>
        /// <param name="connectGroupInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<ActionResult<IConnectResponse>> AddGroup([FromBody] ConnectGroupInfo connectGroupInfo)
        {
            IConnectResponse response = new ConnectResponse();
            try
            {
                this._cpLogger.LogInfo($">>[GroupsController->AddGroup][{connectGroupInfo.Name}] : START.");

                response = await this._connectGroupInfoService.AddNewGroup(connectGroupInfo);
                this._cpLogger.LogInfo($">> [GroupsController->AddGroup][{connectGroupInfo.Name}]: END, Response message: {response.Message}, code: {response.ResponseCode}");

                if (response.Status.Equals(ConnectConstants.Failed)) return BadRequest(response);
                else return Ok(response);

            }
            catch (Exception ex)
            {
                response.Message = ConnectResponseCodes.CP001_MESSAGE;
                response.ResponseCode = ConnectResponseCodes.CP001;
                this._cpLogger.LogError($">> [GroupsController->AddGroup][{connectGroupInfo.Name}]: Exception - {ex.Message}.");
                this._cpLogger.LogError($">> [GroupsController->AddGroup][{connectGroupInfo.Name}]: END, Response message: {response.Message}, code: {response.ResponseCode}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        /// <summary>
        /// Returns all group information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<ActionResult<IConnectRootResponse<IResponseItem<IConnectGroupInfo>>>> GetGroupInfos()
        {
            IConnectRootResponse<IResponseItem<IConnectGroupInfo>> response = new ConnectRootResponse<IResponseItem<IConnectGroupInfo>>();
            try
            {
                this._cpLogger.LogInfo($">>[GroupsController->GetGroupInfos] : START.");

                response = await this._connectGroupInfoService.GetGroupInfos();
                this._cpLogger.LogInfo($">> [GroupsController->GetGroupInfos]: END, Response message: {response.Message}, code: {response.ResponseCode}");

                if (response.Status.Equals(ConnectConstants.Failed)) return BadRequest(response);
                else return Ok(response);

            }
            catch (Exception ex)
            {
                response.Message = ConnectResponseCodes.CP001_MESSAGE;
                response.ResponseCode = ConnectResponseCodes.CP001;
                this._cpLogger.LogError($">> [GroupsController->GetGroupInfos]: Exception - {ex.Message}.");
                this._cpLogger.LogError($">> [GroupsController->GetGroupInfos]: END, Response message: {response.Message}, code: {response.ResponseCode}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
