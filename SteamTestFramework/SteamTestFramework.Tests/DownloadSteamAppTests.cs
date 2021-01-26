using System.IO;
using System.Text.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using SteamTestFramework.Tests.Page;
using SteamTestFramework.Tests.Singleton;
using SteamTestFramework.Tests.Util;

namespace SteamTestFramework.Tests
{
    public class DownloadSteamAppTests
    {
        private static string _settingsPath = "../../../Resources/TestConfig.json";
        private static TestConfig _config;
        private static IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            string jsonString = File.ReadAllText(Path.GetFullPath(_settingsPath));
            _config = JsonSerializer.Deserialize<TestConfig>(jsonString);
            _webDriver = DriverSingleton.GetWebDriver(_config); 
        }

        [Test]
        public void DownloadSteamApp_Successful()
        {
            _webDriver.Navigate().GoToUrl(_config.StartUrl);
            
            LandingPage landingPage = new LandingPage(_webDriver);
            landingPage.GoToDownloadSteamAppPage();
            
            DownloadSteamAppPage downloadSteamAppPage = new DownloadSteamAppPage(_webDriver);
            downloadSteamAppPage.DownloadSteamAppInstaller();

            Assert.IsTrue(File.Exists(Path.GetFullPath(_config.DownloadDirectory + "\\SteamSetup.exe")));
            
            File.Delete(Path.GetFullPath(_config.DownloadDirectory + "\\SteamSetup.exe"));
            
            Assert.IsFalse(File.Exists(Path.GetFullPath(_config.DownloadDirectory + "\\SteamSetup.exe")));

        }

        [TearDown]
        public void TearDown()
        {
            DriverSingleton.CloseDriver();
        }
    }
}