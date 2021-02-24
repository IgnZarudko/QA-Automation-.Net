using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using Api.Tests.Models;
using Api.Tests.Utils;
using Flurl;
using Flurl.Http;
using log4net;
using log4net.Config;
using NUnit.Framework;

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


        public static IEnumerable<TestCaseData> IsSortedByIdTestData()
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

        public static IEnumerable<TestCaseData> GetByIdIsCorrectTestCaseData()
        {
            int expectedId = 99;
            int expectedUserId = 10;
            var url = _baseUrl.AppendPathSegments("posts", expectedId);
            
            yield return new TestCaseData(url, expectedId, expectedUserId);
        }

        [TestCaseSource(typeof(ApiTests), nameof(GetByIdIsCorrectTestCaseData))]
        public void GetByIdIsCorrect(Url url, int expectedId, int expectedUserId)
        {
            var response = url.AllowAnyHttpStatus().GetAsync();
            var jsonStr = response.Result.GetStringAsync().Result;
            
            Assert.AreEqual(ResponseCodes.GET_OK, response.Result.StatusCode, "Expected other response code");

            Post actualPost = PostJsonParser.ParsePost(jsonStr);
            
            Assert.AreEqual(expectedId, actualPost.id, "Ids are not equal");
            Assert.AreEqual(expectedUserId, actualPost.userId, "User Ids are not equal");
            Assert.IsFalse(actualPost.title.Length == 0, "Title is empty");
            Assert.IsFalse(actualPost.body.Length == 0, "Body is empty");
        }
        
        public static IEnumerable<TestCaseData> GetByUnrealIdReturnsEmptyTestCaseData()
        {
            int id = 150;
            var url = _baseUrl.AppendPathSegments("posts", id);
            string expectedResponseString = "{}";
            
            yield return new TestCaseData(url, expectedResponseString);
        }

        [TestCaseSource(typeof(ApiTests), nameof(GetByUnrealIdReturnsEmptyTestCaseData))]
        public void GetByUnrealIdReturnsEmptyTest(Url url, string expectedResponseString)
        {
            var response = url.AllowAnyHttpStatus().GetAsync();
            var jsonStr = response.Result.GetStringAsync().Result;

            Assert.AreEqual(ResponseCodes.PAGE_NOT_FOUND, response.Result.StatusCode, "Expected other response code");
            
            Assert.AreEqual(expectedResponseString, jsonStr, "Response body is not empty");
        }
    }
}