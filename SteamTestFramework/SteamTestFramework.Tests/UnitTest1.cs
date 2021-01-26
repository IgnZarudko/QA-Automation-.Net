using System.IO;
using System.Text.Json;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using SteamTestFramework.Tests.Singleton;
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
            _webDriver = DriverSingleton.GetWebDriver(_config); 
        }

        [Test]
        public void Test1()
        {
            _webDriver.Navigate().GoToUrl("https://store.steampowered.com/");
            Thread.Sleep(500);
            _webDriver.FindElement(By.XPath("//a[@class='header_installsteam_btn_content']")).Click();
            Thread.Sleep(500);
            _webDriver.FindElement(By.XPath("//a[@class='about_install_steam_link']")).Click();
            
        }

        [TearDown]
        public void TearDown()
        {
            DriverSingleton.CloseDriver();
        }
    }
}