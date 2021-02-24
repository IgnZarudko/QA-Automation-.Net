using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Api.Tests.Models;
using Api.Tests.Utils;
using Flurl;
using Flurl.Http;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Api.Tests
{
    public class ApiTests
    {
        private static string _baseUrl = "https://jsonplaceholder.typicode.com";

        private static ILog _logger;
        
        [SetUp]
        public void SetUp()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            _logger = LogManager.GetLogger(typeof(ApiTests));
        }


        private static IEnumerable<TestCaseData> IsSortedByIdTestData()
        {
            yield return new TestCaseData(_baseUrl.AppendPathSegments("posts"));
        }

        [TestCaseSource(typeof(ApiTests), nameof(IsSortedByIdTestData))]
        public void PostsAreSortedById(Url url)
        {
            var response = url.AllowAnyHttpStatus().GetAsync();
            var jsonStr = response.Result.GetStringAsync().Result;
            
            Assert.AreEqual(ResponseCodes.GET_OK, response.Result.StatusCode, "Expected other response code");

            List<Post> posts = PostJsonParser.ParseListOfPosts(jsonStr);

            Assert.IsTrue(PostsChecker.IsListSortedById(posts), "Expected that posts are sorted by id");
        }

        private static IEnumerable<TestCaseData> GetByIdIsCorrectTestCaseData()
        {
            int expectedId = 99;
            int expectedUserId = 10;
            var url = _baseUrl.AppendPathSegments("posts", expectedId);
            
            yield return new TestCaseData(url, expectedId, expectedUserId);
        }

        [TestCaseSource(typeof(ApiTests), nameof(GetByIdIsCorrectTestCaseData))]
        public void GetByIdIsCorrect(Url url, int expectedId, int expectedUserId)
        {
            _logger.Info($"Got url: {url} for GET request");
            
            var response = url.AllowAnyHttpStatus().GetAsync();
            var jsonStr = response.Result.GetStringAsync().Result;

            Assert.AreEqual(ResponseCodes.GET_OK, response.Result.StatusCode, "Expected other response code");

            Post actualPost = PostJsonParser.ParsePost(jsonStr);
            
            Assert.AreEqual(expectedId, actualPost.id, "Ids are not equal");
            Assert.AreEqual(expectedUserId, actualPost.userId, "User Ids are not equal");
            Assert.IsFalse(actualPost.title.Length == 0, "Title is empty");
            Assert.IsFalse(actualPost.body.Length == 0, "Body is empty");
        }
        
        private static IEnumerable<TestCaseData> GetByUnrealIdReturnsEmptyTestCaseData()
        {
            int id = 150;
            var url = _baseUrl.AppendPathSegments("posts", id);
            string expectedResponseString = "{}";
            
            yield return new TestCaseData(url, expectedResponseString);
        }

        [TestCaseSource(typeof(ApiTests), nameof(GetByUnrealIdReturnsEmptyTestCaseData))]
        public void GetByUnrealIdReturnsEmptyTest(Url url, string expectedResponseString)
        {
            _logger.Info($"Got url: {url} for GET request");

            var response = url.AllowAnyHttpStatus().GetAsync();
            var jsonStr = response.Result.GetStringAsync().Result;

            Assert.AreEqual(ResponseCodes.PAGE_NOT_FOUND, response.Result.StatusCode, "Expected other response code");
            
            Assert.AreEqual(expectedResponseString, jsonStr, "Response body is not empty");
        }

        private static IEnumerable<TestCaseData> PostRequestOfPostTestCaseData()
        {
            Randomizer randomizer = new Randomizer();

            var url = _baseUrl.AppendPathSegment("posts");
            
            Post post = new Post
            {
                userId = 1,
                id = 101,
                title = randomizer.GetString(),
                body = randomizer.GetString()
            };
            yield return new TestCaseData(url, post);
        }

        [TestCaseSource(typeof(ApiTests), nameof(PostRequestOfPostTestCaseData))]
        public void PostRequestOfPostTest(Url url, Post newPost)
        {
            string postJson = JsonConvert.SerializeObject(newPost);

            postJson = "{\"userId\": 1, \"title\" : \"titititi\", \"body\" : \"ihfak\"}";
            
            var response = url.AllowAnyHttpStatus().PostStringAsync(postJson).Result;
            var jsonStr = response.GetStringAsync().Result;
            
            Assert.AreEqual(ResponseCodes.POST_OK, response.StatusCode, "Response status code is different");

            Post responsePost = JsonConvert.DeserializeObject<Post>(jsonStr);
            
            _logger.Info($"Post was posted: {postJson}");
            _logger.Info($"Got response post: {jsonStr}");
            Assert.AreEqual(newPost, responsePost, "Posts are different");
        }
        
    }
}