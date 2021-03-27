using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Api.Tests.Models;
using Api.Tests.Utils;
using Flurl;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Api.Tests
{
    public class ApiTests
    {
        private static string _configPath = "../../../Resources/settings.json";
        private static readonly Configs Configs = JsonParser.ParseObject<Configs>(File.ReadAllText(Path.GetFullPath(_configPath)));

        private static ILog _logger;

        [SetUp]
        public void SetUp()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo(Configs.LoggerConfigFile));

            _logger = LogManager.GetLogger(typeof(ApiTests));
        }


        private static IEnumerable<TestCaseData> IsSortedByIdTestData()
        {
            yield return new TestCaseData(Configs.BaseUrl.AppendPathSegments("posts"));
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
            var url = Configs.BaseUrl.AppendPathSegments("posts", expectedId);
            
            yield return new TestCaseData(url, expectedId, expectedUserId);
        }

        [TestCaseSource(typeof(ApiTests), nameof(GetPostByIdIsCorrectTestCaseData))]
        public void GetPostByIdIsCorrect(Url url, int expectedId, int expectedUserId)
        {
            (int responseCode, string postJson) = ApiUtils.Get(url);

            Post actualPost = JsonParser.ParseObject<Post>(postJson);
            
            Assert.AreEqual(ResponseCodes.GET_OK, responseCode, "Expected other response code");
            Assert.AreEqual(expectedId, actualPost.Id, "Ids are not equal");
            Assert.AreEqual(expectedUserId, actualPost.UserId, "User Ids are not equal");
            Assert.IsFalse(actualPost.Title.Length == 0, "Title is empty");
            Assert.IsFalse(actualPost.Body.Length == 0, "Body is empty");
        }
        
        private static IEnumerable<TestCaseData> GetByUnrealIdReturnsEmptyTestCaseData()
        {
            int id = 150;
            var url = Configs.BaseUrl.AppendPathSegments("posts", id);
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

            var url = Configs.BaseUrl.AppendPathSegment("posts");
            
            Post post = new Post
            {
                UserId = 1,
                Title = randomizer.GetString(),
                Body = randomizer.GetString()
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
            
            Assert.AreEqual(newPost.UserId, responsePost.UserId, "Post ids are different");
            Assert.AreEqual(newPost.Title, responsePost.Title, "Posts titles are different");
            Assert.AreEqual(newPost.Body, responsePost.Body, "Posts bodies are different");
        }



        public static IEnumerable<TestCaseData> GetUsersTestCaseData()
        {
            var url = Configs.BaseUrl.AppendPathSegment("users");
            string userJson = File.ReadAllText(Path.GetFullPath("../../../Resources/user.json"));

            User user = JsonParser.ParseObject<User>(userJson);

            yield return new TestCaseData(url, user);
        }


        [TestCaseSource(typeof(ApiTests), nameof(GetUsersTestCaseData))]
        public void GetUsersTest(Url url, User userExpected)
        {
            (int responseCode, string usersJson) = ApiUtils.Get(url);
            
            Assert.AreEqual(ResponseCodes.GET_OK, responseCode, "Response status code is different");
            
            List<User> users = JsonParser.ParseList<User>(usersJson);

            User userToCheck = null;
            foreach (var user in users)
            {
                if (user.Id == userExpected.Id)
                {
                    userToCheck = user;
                    break;
                }
            }
            Assert.NotNull(userToCheck, "User having this id was not found");
            
            Assert.AreEqual(userExpected, userToCheck, "Users are not equal");
        }
        
        public static IEnumerable<TestCaseData> GetUserByIdTestCaseData()
        {
            string userJson = File.ReadAllText(Path.GetFullPath("../../../Resources/user.json"));

            User user = JsonParser.ParseObject<User>(userJson);

            var url = Configs.BaseUrl.AppendPathSegments("users", user.Id);
            
            yield return new TestCaseData(url, user);
        }

        [TestCaseSource(typeof(ApiTests), nameof(GetUserByIdTestCaseData))]
        public void GetUserByIdTest(Url url, User expectedUser)
        {
            (int responseCode, string userJson) = ApiUtils.Get(url);
            
            Assert.AreEqual(ResponseCodes.GET_OK, responseCode, "Response status code is different");
            
            User user = JsonParser.ParseObject<User>(userJson);
            
            Assert.AreEqual(expectedUser, user, "Users are not equal");
        }
    }
}