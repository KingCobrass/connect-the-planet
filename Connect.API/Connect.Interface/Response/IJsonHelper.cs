using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Response
{
    public interface IJsonHelper
    {
        /// <summary>
        /// Get Json from any Object
        /// Null or empty object will be ignored
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        string GetJson<T>(T obj) where T : class;
        /// <summary>
        /// Get Base64 of Json from any Object
        /// Null or empty object will be ignored
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        string GetBae64Json<T>(T obj) where T : class;
        /// <summary>
        /// Desirilize json and returns to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        T GetDeserilizedObject<T>(string json) where T : class;
        /// <summary>
        /// Desirilize json and returns to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        T GetFromBase64DeserilizedObject<T>(string json) where T : class;

        string Base64ToString(string base64String);
        /// <summary>
        /// string to base64
        /// </summary>
        /// <param name="stringToBase64"></param>
        /// <returns></returns>
        string StringtoBase64(string stringToBase64);
    }
}
