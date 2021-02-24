using System.Collections.Generic;
using System.IO;
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
        
        [Test]
        public void Test1()
        {
            _logger.Error($"got url: {_baseUrl}");

            var url = _baseUrl.AppendPathSegment("posts");

            var response = url.GetAsync();
            var jsonListStr = response.Result.GetStringAsync().Result;

            int code = response.Result.StatusCode;
            _logger.Info(code);

            List<Post> posts = JsonParser.ParseListOfPosts(jsonListStr);

            int i = 0;
            do
            {
                _logger.Info(posts[i].id);
                i++;
            } while (i + 1 <= posts.Count);


        }
    }
}