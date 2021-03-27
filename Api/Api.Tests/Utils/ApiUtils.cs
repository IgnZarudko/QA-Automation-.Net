using Flurl;
using Flurl.Http;

namespace Api.Tests.Utils
{
    public class ApiUtils
    {
        public static (int responseCode, string responseJson) Get(Url url)
        {
            var response = url.AllowAnyHttpStatus().GetAsync().Result;
            var postsJson = response.GetStringAsync().Result;

            return (response.StatusCode, postsJson);
        }

        public static (int responseCode, string responseJson) Post(Url url, object obj)
        {
            var response = url.AllowAnyHttpStatus().PostJsonAsync(obj).Result;
            var jsonStr = response.GetStringAsync().Result;

            return (response.StatusCode, jsonStr);
        }
    }
}