using Newtonsoft.Json;

namespace DataBase.Tests.Models
{
    public class ProjectAndTestsAmount
    {
        [JsonProperty("projectName")]
        public string ProjectName { get; set; } 

        [JsonProperty("testAmount")]
        public int TestAmount { get; set; }

        public ProjectAndTestsAmount(string projectName, int testAmount)
        {
            ProjectName = projectName;
            TestAmount = testAmount;
        }
    }
}