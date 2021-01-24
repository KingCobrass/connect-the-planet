using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.ServerException
{

    /// <summary>
    /// 
    /// </summary>
    public class DatabaseException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public DatabaseException() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public DatabaseException(string message) : base(message) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
