using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using HelloWebdriver.Tests.Model;
using HelloWebdriver.Tests.pages;
using HelloWebdriver.Tests.service;
using HelloWebdriver.Tests.singleton;
using HelloWebdriver.Tests.util;
using NUnit.Framework;
using OpenQA.Selenium;

namespace HelloWebdriver.Tests
{
    public static class YandexMarketTests
    {
        private static string _settingsPath = "../../../resources/settings.json";
        private static TestConfig _config;
        private static User _testUser;
        private static IWebDriver _webDriver;

        [SetUp]
        public static void SetUp()
        {
            string jsonString = File.ReadAllText(Path.GetFullPath(_settingsPath));
            _config = JsonSerializer.Deserialize<TestConfig>(jsonString);
            _webDriver = DriverSingleton.GetWebDriver(_config.Browser);
            
            _testUser = UserCreator.WithCredentialsFromConfig(_config); 
        }
        
        [Test]
        public static void MarketTest()
        {
            LandingPage landingPage = new LandingPage(_webDriver, _config.StartUrl);
            landingPage.Open();
            
            Assert.IsTrue(landingPage.IsBannerDisplayed(), "Banner wasn't detected, page cannot be detected");
            
            landingPage.GoToLogin();

            LoginPage loginPage = new LoginPage(_webDriver);
            
            loginPage.EnterLogin(_testUser.UserLogin);
            loginPage.SubmitLogin();
            
            loginPage.EnterPassword(_testUser.UserPassword);
            loginPage.SubmitPassword();

            User userActual = landingPage.CurrentUser();
            
            Assert.AreEqual(_testUser, userActual, $"Users are not the same:\n" +
                                                  $"expected email {_testUser.UserEmail}, but found {userActual.UserEmail}\n" +
                                                  $"expected username {_testUser.Username}, but found {userActual.Username}");

            HashSet<string> popularCategoriesUrls = landingPage.PopularCategoriesUrls();
            
            CategoryPage categoryPage = landingPage.GoToRandomPopularCategory(out var categoryUrl);

            Assert.AreEqual(categoryUrl, _webDriver.Url, $"Сategory URLs are not equal:\n" +
                                                         $"driver at category {_webDriver.Url}\n" +
                                                         $"but expected it will be category {categoryUrl}");
            
            landingPage = categoryPage.GoToLandingPage();

            List<(string categoryName, string categoryUrl)> allCategories = landingPage.AllCategories();
            
            CsvSerializer.WriteToFile(_config.CsvFilePath, allCategories);

            HashSet<string> allUrls = GetUrlSet(allCategories);

            Assert.IsTrue(popularCategoriesUrls.IsSubsetOf(allUrls), "There are different categories,\n" +
                                                                     "which are not in set of all categories");
        }

        private static HashSet<string> GetUrlSet(List<(string categoryName, string categoryUrl)> allCategories)
        {
            HashSet<string> allUrls = new HashSet<string>();
            foreach (var category in allCategories)
            {
                allUrls.Add(category.categoryUrl);
            }

            return allUrls;
        }

        [TearDown]
        public static void TearDown()
        {
            DriverSingleton.CloseDriver();
        }
    }
}