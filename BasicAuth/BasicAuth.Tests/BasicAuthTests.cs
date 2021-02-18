using System;
using System.Text.Json;
using System.Threading;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements;
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
        
        private static readonly By SwitchToAnotherViewButtonLocator = By.XPath("//a[@aria-controls='rawdata-panel']");
        private static readonly By ResponseBodyLocator = By.XPath("//pre");
        
        [SetUp]
        public void Setup()
        {
            _browser = AqualityServices.Browser;
            _browser.Maximize();
        }

        [TestCase("user", "passwd")]
        public void BasicAuth_Test(string username, string password)
        {
            string urlWithCredentials = _pageUrl.Split("//")[0] +
                                        $"//{username}:{password}@" +
                                        _pageUrl.Split("//")[1];
            
            _browser.GoTo(urlWithCredentials);
            
            _browser.WaitForPageToLoad();

            
            if (_browser.BrowserName == BrowserName.Firefox)
            {
                new WebDriverWait(_browser.Driver, TimeSpan.FromSeconds(10))
                    .Until(d => _browser.Driver.FindElement(SwitchToAnotherViewButtonLocator).Enabled);
                _browser.Driver.FindElement(SwitchToAnotherViewButtonLocator).Click();
            }
            
            string responseString = _browser.Driver.FindElement(ResponseBodyLocator).Text;

            Response actualResponse = JsonSerializer.Deserialize<Response>(responseString);
            
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