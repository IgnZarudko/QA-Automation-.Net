using Newtonsoft.Json;
using NUnit.Framework.Internal;

namespace DataBase.Tests.Models
{
    public class TestWithProjectAndStartDate
    {
        [JsonProperty("projectName")]
        public string ProjectName { get; set; } 

        [JsonProperty("testName")]
        public string TestName { get; set; } 

        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        public TestWithProjectAndStartDate(string projectName, string testName, string startDate)
        {
            ProjectName = projectName;
            TestName = testName;
            StartDate = startDate;
        }
    }
}