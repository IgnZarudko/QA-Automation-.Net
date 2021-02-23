using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using BasicAuth.Tests.Page;
using Flurl;
using NUnit.Framework;


namespace BasicAuth.Tests
{
    public class BasicAuthTests
    {
        private static Browser _browser;

        private ResponsePage _responsePage;

        private static string _url;
        
        [SetUp]
        public void Setup()
        {
            _browser = AqualityServices.Browser;
            _browser.Maximize();
            
            _url = AqualityServices.Get<ISettingsFile>().GetValue<string>(".hostUrl");
            
            _responsePage = new ResponsePage();
        }

        [TestCase("user", "passwd")]
        public void BasicAuth_Test(string username, string password)
        {
            var urlWithCredentials = _url
                .AppendPathSegments(username, password)
                .ToUri().ToString()
                .Replace("//", $"//{username}:{password}@");

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