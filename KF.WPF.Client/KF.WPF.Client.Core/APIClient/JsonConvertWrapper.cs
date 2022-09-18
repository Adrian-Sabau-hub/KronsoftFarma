using Newtonsoft.Json;

namespace KF.WPF.Client.Core.APIClient
{
    /// <summary>
    /// Wrapper over Json Serialization
    /// </summary>
    public static class JsonConvertWrapper
    {
        /// <summary>
        /// Deserializes the JSON to the specified .NET type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryDeserializeObject<T>(string value, out T result)
        {
            result = default(T);
            try
            {
                result = JsonConvert.DeserializeObject<T>(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  Deserializes the JSON to the specified .NET type using a collection of Newtonsoft.Json.JsonConverter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="converters"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string value, JsonConverter[] converters)
        {
            return JsonConvert.DeserializeObject<T>(value, converters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }

        public static string SerializeObjectWithUTC(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            });
        }

        public static T DeserializeObject<T>(string value, JsonSerializerSettings? settings = null)
        {
            return JsonConvert.DeserializeObject<T>(value, settings);
        }
    }
}
