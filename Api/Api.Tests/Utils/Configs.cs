using Newtonsoft.Json;

namespace Api.Tests.Utils
{
    public class Configs
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; } 

        [JsonProperty("loggerConfigFile")]
        public string LoggerConfigFile { get; set; }
    }
}