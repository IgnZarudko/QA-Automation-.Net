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
            (int responseCode, string postsJson) = ApiUtils.Get(url);

            List<Post> posts = JsonParser.ParseList<Post>(postsJson);
            
            Assert.AreEqual(ResponseCodes.GET_OK, responseCode, "Expected other response code");

            Assert.IsTrue(PostsChecker.ArePostsSortedById(posts), "Expected that posts are sorted by id");
        }

        private static IEnumerable<TestCaseData> GetPostByIdIsCorrectTestCaseData()
        {
            int expectedId = 99;
            int expectedUserId = 10;
            var url = _baseUrl.AppendPathSegments("posts", expectedId);
            
            yield return new TestCaseData(url, expectedId, expectedUserId);
        }

        [TestCaseSource(typeof(ApiTests), nameof(GetPostByIdIsCorrectTestCaseData))]
        public void GetPostByIdIsCorrect(Url url, int expectedId, int expectedUserId)
        {
            _logger.Info($"Got url: {url} for GET request");
            
            (int responseCode, string postJson) = ApiUtils.Get(url);

            Post actualPost = JsonParser.ParseObject<Post>(postJson);
            
            Assert.AreEqual(ResponseCodes.GET_OK, responseCode, "Expected other response code");
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

            (int responseCode, string postJson) = ApiUtils.Get(url);
            
            Assert.AreEqual(ResponseCodes.PAGE_NOT_FOUND, responseCode, "Expected other response code");
            Assert.AreEqual(expectedResponseString, postJson, "Response body is not empty");
        }

        private static IEnumerable<TestCaseData> PostRequestOfPostTestCaseData()
        {
            Randomizer randomizer = new Randomizer();

            var url = _baseUrl.AppendPathSegment("posts");
            
            Post post = new Post
            {
                userId = 1,
                title = randomizer.GetString(),
                body = randomizer.GetString()
            };
            yield return new TestCaseData(url, post);
        }

        [TestCaseSource(typeof(ApiTests), nameof(PostRequestOfPostTestCaseData))]
        public void PostRequestOfPostTest(Url url, Post newPost)
        {
            (int responseCode, string jsonStr) = ApiUtils.Post(url, newPost);
            
            Assert.AreEqual(ResponseCodes.POST_OK, responseCode, "Response status code is different");
            
            _logger.Info($"Post was sent: {JsonConvert.SerializeObject(newPost)}");
            
            Post responsePost = JsonConvert.DeserializeObject<Post>(jsonStr);
            
            _logger.Info($"Got response post: {jsonStr}");
            
            Assert.AreEqual(newPost.userId, responsePost.userId, "Post ids are different");
            Assert.AreEqual(newPost.title, responsePost.title, "Posts titles are different");
            Assert.AreEqual(newPost.body, responsePost.body, "Posts bodies are different");
        }



        public static IEnumerable<TestCaseData> GetUsersTestCaseData()
        {
            var url = _baseUrl.AppendPathSegment("users");
            string userJson = File.ReadAllText(Path.GetFullPath("../../../Resources/user.json"));

            User user = UserJsonParser.ParseUser(userJson);

            yield return new TestCaseData(url, user);
        }


        [TestCaseSource(typeof(ApiTests), nameof(GetUsersTestCaseData))]
        public void GetUsersTest(Url url, User user)
        {
            (int responseCode, string usersJson) = ApiUtils.Get(url);

            List<User> users = JsonParser.ParseList<User>(usersJson);
            
            
        }
    }
}