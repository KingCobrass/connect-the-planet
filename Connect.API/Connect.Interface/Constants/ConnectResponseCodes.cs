using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Constants
{
    public class ConnectResponseCodes
    {
        #region Common CP001-020
        /// <summary>
        /// CP001: Unexpected System Error.
        /// </summary>
        public const string CP001 = "CP001";
        /// <summary>
        /// CP001: Unexpected System Error.
        /// </summary>
        public const string CP001_MESSAGE = "Unexpected System Error.";
        #endregion

        #region Users CP021 - 040
        /// <summary>
        /// CP021: User Sign up/Update/Delete Success.
        /// </summary>
        public const string CP021 = "CP021";
        /// <summary>
        /// CP021: User Sign up/Update/Delete Success.
        /// </summary>
        public const string CP021_MESSAGE = "User Sign up/Update/Delete Success.";
        /// <summary>
        /// CP022: User Sign up/Update/Delete Failed.
        /// </summary>
        public const string CP022 = "CP022";
        /// <summary>
        /// CP022: User Sign up/Update/Delete Failed.
        /// </summary>
        public const string CP022_MESSAGE = "User Sign up/Update/Delete Failed.";
        /// <summary>
        /// CP023: User Not Found.
        /// </summary>
        public const string CP023 = "CP023";
        /// <summary>
        /// CP023: User Not Found.
        /// </summary>
        public const string CP023_MESSAGE = "User Not Found.";
        /// <summary>
        /// CP024: User Retriving Success.
        /// </summary>
        public const string CP024 = "CP024";
        /// <summary>
        /// CP024: User Retriving Success.
        /// </summary>
        public const string CP024_MESSAGE = "User Retriving Success.";
        /// <summary>
        /// CP025: User Retriving Failed.
        /// </summary>
        public const string CP025 = "CP025";
        /// <summary>
        /// CP025: User Retriving Failed.
        /// </summary>
        public const string CP025_MESSAGE = "User Retriving Failed.";
        /// <summary>
        /// CP026: Login Success.
        /// </summary>
        public const string CP026 = "CP026";
        /// <summary>
        /// CP026: Login Success.
        /// </summary>
        public const string CP026_MESSAGE = "Login Success.";
        /// <summary>
        /// CP027: User Is InActive.
        /// </summary>
        public const string CP027 = "CP027";
        /// <summary>
        /// CP027: User Is InActive.
        /// </summary>
        public const string CP027_MESSAGE = "User Is InActive.";
        /// <summary>
        /// CP028: Email Already Exist!.
        /// </summary>
        public const string CP028 = "CP028";
        /// <summary>
        /// CP028: Email Already Exist!.
        /// </summary>
        public const string CP028_MESSAGE = "Email Already Exist!.";
        /// <summary>
        /// CP029: Login Failed. Please check the email address and try again.
        /// </summary>
        public const string CP029 = "CP029";
        /// <summary>
        /// CP029: Login Failed. Please check the email address and try again.
        /// </summary>
        public const string CP029_MESSAGE = "Login Failed. Please check the email address and try again.";

        #endregion

        #region Chat CP041 - 100
        /// <summary>
        /// CP041: Message Publish Success.
        /// </summary>
        public const string CP041 = "CP041";
        /// <summary>
        /// CP041: Message Publish Success.
        /// </summary>
        public const string CP041_MESSAGE = "Message Publish Success.";
        /// <summary>
        /// CP042: Message Publish Failed.
        /// </summary>
        public const string CP042 = "CP042";
        /// <summary>
        /// CP042: Message Publish Failed.
        /// </summary>
        public const string CP042_MESSAGE = "Message Publish Failed.";
        #endregion
    }
}
