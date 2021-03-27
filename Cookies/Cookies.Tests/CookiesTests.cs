using System.Collections.Generic;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Elements.Actions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Cookies.Tests
{
    public class CookiesTests
    {
        private static Browser _browser;

        private static string _url;
        
        [SetUp]
        public void Setup()
        {
            _browser = AqualityServices.Browser;
            _browser.Maximize();

            _url = AqualityServices.Get<ISettingsFile>().GetValue<string>(".hostUrl");
        }

        private static IEnumerable<TestCaseData> CookiesProvider()
        {
            List<Cookie> toInsert = new List<Cookie>();
            toInsert.Add(new Cookie("example_key_1", "example_value_1"));
            toInsert.Add(new Cookie("example_key_2", "example_value_2"));
            toInsert.Add(new Cookie("example_key_3", "example_value_3"));

            Cookie toDelete = new Cookie("example_key_1", "");
            
            Cookie toUpdate = new Cookie("example_key_3", "example_value_300");
            
            yield return new TestCaseData(toInsert, toDelete, toUpdate);
        }

        [TestCaseSource(typeof(CookiesTests), nameof(CookiesProvider))]
        public void Cookies_Test(List<Cookie> toInsert, Cookie toDelete, Cookie toUpdate)
        {
            _browser.GoTo(_url);
            _browser.WaitForPageToLoad();
            
            foreach (var cookie in toInsert)
            {
                _browser.Driver.Manage().Cookies.AddCookie(cookie);
            }
            
            Assert.AreEqual(toInsert.Count, _browser.Driver.Manage().Cookies.AllCookies.Count);
            
            _browser.Driver.Manage().Cookies.DeleteCookie(toDelete);
            
            Assert.IsNull(_browser.Driver.Manage().Cookies.GetCookieNamed(toDelete.Name));
            
            _browser.Driver.Manage().Cookies.AddCookie(toUpdate);
            
            Assert.AreEqual(toUpdate.Value, _browser.Driver.Manage().Cookies.GetCookieNamed(toUpdate.Name).Value);
            
            _browser.Driver.Manage().Cookies.DeleteAllCookies();
            
            Assert.AreEqual(0, _browser.Driver.Manage().Cookies.AllCookies.Count);
        }

        [TearDown]
        public void TearDown()
        {
            _browser.Quit();
        }
        
    }
}