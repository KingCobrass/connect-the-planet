using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Logger
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICPLogger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void LogDebug(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void LogInfo(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void LogError(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void LogCritical(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void LogWarning(string message);
    }
}
