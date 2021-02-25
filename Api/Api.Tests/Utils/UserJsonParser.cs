using System.Collections.Generic;
using Api.Tests.Models;
using Newtonsoft.Json;

namespace Api.Tests.Utils
{
    public class UserJsonParser
    {
        public static List<User> ParseListOfUsers(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<User>>(jsonString);
        }

        public static User ParseUser(string jsonString)
        {
            return JsonConvert.DeserializeObject<User>(jsonString);
        }
    }
}