using System.Collections.Generic;
using Api.Tests.Models;
using Newtonsoft.Json;

namespace Api.Tests.Utils
{
    public class JsonParser
    {
        public static List<Post> ParseListOfPosts(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<Post>>(jsonString);
        }
    }
}