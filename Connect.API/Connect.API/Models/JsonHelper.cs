using Connect.Interface.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.API.Models
{
    public class JsonHelper : IJsonHelper
    {

        /// <summary>
        /// Get Json from any Object
        /// Null or empty object will be ignored
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetJson<T>(T obj) where T : class
        {
            var serializedObject = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            });

            return serializedObject.ToString();
        }
        /// <summary>
        /// Get Base64 of Json from any Object
        /// Null or empty object will be ignored
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetBae64Json<T>(T obj) where T : class
        {
            var serializedObject = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return StringtoBase64(serializedObject.ToString());

        }
        /// <summary>
        /// Desirilize json and returns to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public T GetDeserilizedObject<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
                //StringEscapeHandling = StringEscapeHandling.EscapeHtml
            });
        }
        /// <summary>
        /// Desirilize json and returns to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public T GetFromBase64DeserilizedObject<T>(string json) where T : class
        {
            try
            {
                json = Base64ToString(json);
                return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                    //StringEscapeHandling = StringEscapeHandling.EscapeHtml
                });
            }
            catch (Exception) { throw; }
        }

        public string Base64ToString(string base64String)
        {
            byte[] data = Convert.FromBase64String(base64String);
            string decodedString = Encoding.UTF8.GetString(data);
            return decodedString;
        }
        /// <summary>
        /// string to base64
        /// </summary>
        /// <param name="stringToBase64"></param>
        /// <returns></returns>
        public string StringtoBase64(string stringToBase64)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(stringToBase64);
            return Convert.ToBase64String(bytes);
        }
    }
}
