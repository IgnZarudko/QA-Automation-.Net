using Newtonsoft.Json;

namespace DataBase.Tests.Models
{
    public class TestWithProjectAndMinTime
    {
        [JsonProperty("projectName")] 
        public string ProjectName { get; set; }

        [JsonProperty("testName")] 
        public string TestName { get; set; }

        [JsonProperty("timeEstimated")] 
        public int TimeEstimated { get; set; }

        public TestWithProjectAndMinTime(string projectName, string testName, int timeEstimated)
        {
            ProjectName = projectName;
            TestName = testName;
            TimeEstimated = timeEstimated;
        }
    }
}