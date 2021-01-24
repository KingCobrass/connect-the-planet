using Connect.Interface.Logger;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Services
{
    public class CPLogger : ICPLogger
    {
        private readonly ILogger<CPLogger> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CPLogger(ILogger<CPLogger> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Critical
        /// </summary>
        /// <param name="message"></param>
        public void LogCritical(string message)
        {
            this._logger?.LogCritical(message);
        }
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message"></param>
        public void LogDebug(string message)
        {
            this._logger?.LogDebug(message);
        }
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message)
        {
            this._logger?.LogError(message);
        }
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(string message)
        {
            this._logger?.LogInformation(message);
        }
        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="message"></param>
        public void LogWarning(string message)
        {
            this._logger?.LogWarning(message);
        }
    }
}
