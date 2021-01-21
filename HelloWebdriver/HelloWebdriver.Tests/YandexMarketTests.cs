using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using HelloWebdriver.Tests.Model;
using HelloWebdriver.Tests.pages;
using HelloWebdriver.Tests.service;
using HelloWebdriver.Tests.singleton;
using NUnit.Framework;
using OpenQA.Selenium;

namespace HelloWebdriver.Tests
{
    public class YandexMarketTests
    {
        private static TestConfig Config;
        private static User TestUser;
        private static IWebDriver WebDriver;
        
        [SetUp]
        public static void SetUp()
        {
            string jsonString = File.ReadAllText(Path.GetFullPath("../../../resources/settings.json"));
            Config = JsonSerializer.Deserialize<TestConfig>(jsonString);
            WebDriver = DriverSingleton.GetWebDriver(Config.Browser);
            
            TestUser = UserCreator.WithCredentialsFromConfig(Config); 
        }
        
        [Test]
        public static void MarketTest()
        {
            LandingPage landingPage = new LandingPage(WebDriver, Config.StartUrl);
            landingPage.Open();
            
            Assert.IsTrue(landingPage.IsBannerDisplayed(), "Banner wasn't detected, page cannot be detected");
            
            LoginPage loginPage = landingPage.GoToLogin();
            
            loginPage.EnterLogin(TestUser.UserLogin);
            loginPage.SubmitLogin();
            
            loginPage.EnterPassword(TestUser.UserPassword);
            loginPage.SubmitPassword();

            User userActual = landingPage.CurrentUser();
            
            Assert.AreEqual(TestUser, userActual, $"Users are not the same:\n" +
                                                  $"expected email {TestUser.UserEmail}, but found {userActual.UserEmail}\n" +
                                                  $"expected username {TestUser.Username}, but found {userActual.Username}");

            HashSet<string> popularCategoriesNames = landingPage.PopularCategoriesNames();
        }

        [TearDown]
        public static void TearDown()
        {
            DriverSingleton.CloseDriver();
        }
    }
}