using Connect.API.Infrastructure;
using Connect.API.Models;
using Connect.API.Models.Configuration;
using Connect.API.Models.Response;
using Connect.API.Services;
using Connect.Interface.Account;
using Connect.Interface.Constants;
using Connect.Interface.Jwt;
using Connect.Interface.Logger;
using Connect.Interface.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Connect.API.Controllers
{
    /// <summary>
    /// Handle all kinds of Accoutns related business
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(ConnectConstants.CORSS_POLICY_NAME)]
    public class AccoutnsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICPLogger _cpLogger;
        public readonly SymmetricSecurityKey _connectSecurityKey;
        private readonly SigningCredentials _signingCreds; //= new SigningCredentials(Startup.SecurityKey, SecurityAlgorithms.HmacSha256);

        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IOptions<AppSettings> _settings;


        //SignInManager<ApplicationUser> signInManager,
        //UserManager<ApplicationUser> userManager,

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="options"></param>
        /// <param name="cpLogger"></param>
        public AccoutnsController(
            IAccountService accountService,
            IOptions<AppSettings> options,
        ICPLogger cpLogger)
        {
            this._accountService = accountService;
            this._cpLogger = cpLogger;
            this._settings = options;
            this._connectSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this._settings.Value.ConnectJwt.ApiSecret));
            this._signingCreds = new SigningCredentials(this._connectSecurityKey, SecurityAlgorithms.HmacSha256);
            //this._signInManager = signInManager;
            //this._userManager = userManager;
        }
        /// <summary>
        /// Sign up user based on given parameters
        /// </summary>
        /// <remarks>
        /// All Possible Connect Planet API Response Code
        ///  <br>CP021: User Sign up/Update/Delete Success.</br>
        /// CP022: User Sign up/Update/Delete Failed.
        ///  <br>CP028: Email Already Exist!.</br>
        ///  CP001: Unexpected System Error.
        /// </remarks>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("signup")]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<ActionResult<IConnectResponse>> CreateUser([FromBody] User user)
        {
            IConnectResponse response = new ConnectResponse();
            try
            {
                this._cpLogger.LogInfo($">>[AccoutnsController->CreateUser][{user.Email}] : START.");

                response = await this._accountService.SignupUser(user);
                this._cpLogger.LogInfo($">> [AccoutnsController->CreateUser][{user.Email}]: END, Response message: {response.Message}, code: {response.ResponseCode}");

                if (response.Status.Equals(ConnectConstants.Failed)) return BadRequest(response);
                else return Ok(response);

            }
            catch (Exception ex)
            {
                response.Message = ConnectResponseCodes.CP001_MESSAGE;
                response.ResponseCode = ConnectResponseCodes.CP001;
                this._cpLogger.LogError($">> [AccoutnsController->CreateUser][{user.Email}]: Exception - {ex.Message}.");
                this._cpLogger.LogError($">> [AccoutnsController->CreateUser][{user.Email}]: END, Response message: {response.Message}, code: {response.ResponseCode}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        /// <summary>
        /// Login to the system
        /// </summary>
        /// <remarks>
        /// All Possible Connect Planet API Response Code
        ///  <br>CP026: Login Success.</br>
        /// CP029: Login Failed. Please check the email address and try again.
        ///  <br>CP001: Unexpected System Error.</br>
        /// </remarks>
        /// <param name="connectCredentials"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<IConnectRootResponse<IConnectToken>>> Login([FromBody] ConnectCredentials connectCredentials)
        {
            IConnectRootResponse<IConnectToken> response = new ConnectRootResponse<IConnectToken>();
            try
            {
                this._cpLogger.LogInfo($">>[AccoutnsController->Login][{connectCredentials.Email}] : START.");

                response = await this._accountService.GetLogin(connectCredentials.Email);
                this._cpLogger.LogInfo($">> [AccoutnsController->Login][{connectCredentials.Email}]: END, Response message: {response.Message}, code: {response.ResponseCode}");

                /*
                #region AddEmailClaim
                // create a new user
                var user = new ApplicationUser { UserName = connectCredentials.Email, Email = connectCredentials.Email };
                var result = await _userManager.CreateAsync(user);

                // add the email claim and value for this user
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, connectCredentials.Email));
                #endregion
                */
                if (response.Status.Equals(ConnectConstants.Failed)) return BadRequest(response);
                else return Ok(response);

            }
            catch (Exception ex)
            {
                response.Message = ConnectResponseCodes.CP001_MESSAGE;
                response.ResponseCode = ConnectResponseCodes.CP001;
                this._cpLogger.LogError($">> [AccoutnsController->CreateUser][{connectCredentials.Email}]: Exception - {ex.Message}.");
                this._cpLogger.LogError($">> [AccoutnsController->CreateUser][{connectCredentials.Email}]: END, Response message: {response.Message}, code: {response.ResponseCode}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /*
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            //await _signInManager.SignOutAsync();
            this._cpLogger.LogInfo("User logged out.");
            return RedirectToPage("/Index");
        }
        */
    }
}
