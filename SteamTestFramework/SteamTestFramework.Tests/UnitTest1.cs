using System.IO;
using System.Text.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using SteamTestFramework.Tests.singleton;
using SteamTestFramework.Tests.Util;

namespace SteamTestFramework.Tests
{
    public class Tests
    {
        private static string _settingsPath = "../../../Resources/TestConfig.json";
        private static TestConfig _config;
        private static IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            string jsonString = File.ReadAllText(Path.GetFullPath(_settingsPath));
            _config = JsonSerializer.Deserialize<TestConfig>(jsonString);
            _webDriver = DriverSingleton.GetWebDriver(_config.Browser); }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [TearDown]
        public void TearDown()
        {
            DriverSingleton.CloseDriver();
        }
    }
}