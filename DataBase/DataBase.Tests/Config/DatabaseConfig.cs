using Newtonsoft.Json;

namespace DataBase.Tests.Config
{
    public class DatabaseConfig
    {
        [JsonProperty("server")]
        public string Server { get; set; } 

        [JsonProperty("user")]
        public string User { get; set; } 

        [JsonProperty("password")]
        public string Password { get; set; } 

        [JsonProperty("database")]
        public string Database { get; set; } 
    }
}