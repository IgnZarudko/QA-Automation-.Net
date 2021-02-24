using System.Collections.Generic;
using System.Diagnostics;
using Api.Tests.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Api.Tests.Utils
{
    public class PostJsonParser
    {
        public static List<Post> ParseListOfPosts(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<Post>>(jsonString);
        }

        public static Post ParsePost(string jsonString)
        {
            return JsonConvert.DeserializeObject<Post>(jsonString);
        }
    }
}