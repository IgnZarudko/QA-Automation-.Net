using System.IO;
using System.Text.Json;
using System.Threading;
using HelloWebdriver.Tests.pages;
using HelloWebdriver.Tests.singleton;
using NUnit.Framework;
using OpenQA.Selenium;

namespace HelloWebdriver.Tests
{
    public class YandexMarketTests
    {
        private static TestConfig Config;
        private static IWebDriver WebDriver;
        
        [SetUp]
        public static void SetUp()
        {
            string jsonString = File.ReadAllText(Path.GetFullPath("../../../resources/settings.json"));
            Config = JsonSerializer.Deserialize<TestConfig>(jsonString);
            WebDriver = DriverSingleton.GetWebDriver(Config.Browser);
        }
        
        [Test]
        public static void MarketTest()
        {
            Assert.AreEqual(Config.Browser, "chrome", "somethings wrong i can feel it");
            Assert.AreEqual(Config.StartUrl, "https://market.yandex.ru/", "somethings wrong i can feel it");
            Assert.AreEqual(Config.UserLogin, "semen-testerov", "somethings wrong i can feel it");
            Assert.AreEqual(Config.UserPassword, "2vEaCY.,87tZh%8", "somethings wrong i can feel it");
            Assert.AreEqual(Config.Username, "Семён Тестеров", "somethings wrong i can feel it");
            
            LandingPage landingPage = new LandingPage(WebDriver, Config.StartUrl);
            landingPage.Open();
            
            Assert.IsTrue(landingPage.IsBannerDisplayed(), "This page has not banner so it could not be landing page");
            landingPage.GoToLogin();

        }

        [TearDown]
        public static void TearDown()
        {
            DriverSingleton.CloseDriver();
        }
    }
}