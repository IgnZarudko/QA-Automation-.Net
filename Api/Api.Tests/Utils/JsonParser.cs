using System.Collections.Generic;
using Newtonsoft.Json;

namespace Api.Tests.Utils
{
    public class JsonParser
    {
        public static List<T> ParseList<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<T>>(jsonString);
        }

        public static T ParseObject<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}