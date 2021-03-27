using Newtonsoft.Json;

namespace DataBase.Tests.Models
{
    public class BrowserWithTestsExecuted
    {
        [JsonProperty("browser")]
        public string Browser { get; set; } 

        [JsonProperty("amountOfTests")]
        public int AmountOfTests { get; set; }

        public BrowserWithTestsExecuted(string browser, int amountOfTests)
        {
            Browser = browser;
            AmountOfTests = amountOfTests;
        }
    }
}