using System;
using System.Text.Json;
using System.Threading;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements;
using BasicAuth.Tests.Page;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BasicAuth.Tests
{
    public class BasicAuthTests
    {
        private static string _pageUrl = "https://httpbin.org/basic-auth/user/passwd";
        
        private static Browser _browser;

        private ResponsePage _responsePage;
        
        [SetUp]
        public void Setup()
        {
            _browser = AqualityServices.Browser;
            _browser.Maximize();

            _responsePage = new ResponsePage();
        }

        [TestCase("user", "passwd")]
        public void BasicAuth_Test(string username, string password)
        {
            string urlWithCredentials = _pageUrl.Replace("//", $"//{username}:{password}@");
            
            _browser.GoTo(urlWithCredentials);
            
            _browser.WaitForPageToLoad();
            
            if (_browser.BrowserName == BrowserName.Firefox)
            {
                _responsePage.SwitchToOtherView();
            }

            Response actualResponse = _responsePage.Response();
            
            Assert.IsTrue(actualResponse.authenticated, $"actual response is not correct (got {actualResponse.authenticated})");
            
            Assert.AreEqual(username, actualResponse.user, $"user is different ({actualResponse.user})");
        }

        [TearDown]
        public void TearDown()
        {
            _browser.Quit();
        }
    }
}