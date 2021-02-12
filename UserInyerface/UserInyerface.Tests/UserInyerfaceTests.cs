using System.Collections.Generic;
using System.Threading;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using UserInyerface.Tests.Page;

namespace UserInyerface.Tests
{
    public class UserInyerfaceTests
    {
        private Browser _browser;
        
        private WelcomePage _welcomePage;
        private CreateProfilePage _createProfilePage;
        
        [SetUp]
        public void Setup()
        {
            _browser = AqualityServices.Browser;
            _browser.Maximize();
            _browser.GoTo("https://userinyerface.com/game.html%20target=");
            _browser.WaitForPageToLoad();
            
            _welcomePage = new WelcomePage(By.XPath("//div[@class='start view view--center']"), "Welcome page");
            _createProfilePage = new CreateProfilePage(By.XPath("//div[@class='game view']"), "Create profile page");
        }

        private static IEnumerable<TestCaseData> PasswordAndEmail()
        {
            yield return new TestCaseData("pssЦoRd228zzz", "validmail", "domain");
        }

        [TestCaseSource(typeof(UserInyerfaceTests), nameof(PasswordAndEmail))]
        public void EnterInfo_Test(string password, string email, string domain)
        {
            Assert.IsTrue(_welcomePage.State.IsDisplayed, $"{_welcomePage.Name} isn't displayed as expected");
            
            _welcomePage.ToFormButton.Click(); 
            
            Assert.IsTrue(_createProfilePage.State.IsDisplayed, $"{_createProfilePage.Name} isn't displayed as expected");

        }
        
        [Test]
        public void HideHelp_Test()
        {
            Assert.IsTrue(_welcomePage.State.IsDisplayed, $"{_welcomePage.Name} isn't displayed as expected");
            
            _welcomePage.ToFormButton.Click();   
            
            Assert.IsTrue(_createProfilePage.State.IsDisplayed, $"{_createProfilePage.Name} isn't displayed as expected");
            
            _createProfilePage.HelpForm.SendToBottomButton.Click();
                
            Assert.IsFalse(_createProfilePage.HelpForm.State.IsDisplayed);
        }
        
        [Test]
        public void AcceptCookies_Test()
        {
            Assert.IsTrue(_welcomePage.State.IsDisplayed, $"{_welcomePage.Name} isn't displayed as expected");
            
            _welcomePage.ToFormButton.Click();   
            
            Assert.IsTrue(_createProfilePage.State.IsDisplayed, $"{_createProfilePage.Name} isn't displayed as expected");
            
            _createProfilePage.CookiesForm.AcceptButton.Click();

            Assert.IsFalse(_createProfilePage.CookiesForm.State.IsDisplayed);
        }

        [Test]
        public void Timer_Test()
        {
            Assert.IsTrue(_welcomePage.State.IsDisplayed, $"{_welcomePage.Name} isn't displayed as expected");
            
            _welcomePage.ToFormButton.Click();   
            
            Assert.IsTrue(_createProfilePage.State.IsDisplayed, $"{_createProfilePage.Name} isn't displayed as expected");
            
            Assert.AreEqual("00", _createProfilePage.Timer.Text.Split(":")[^1]);
        }

        [TearDown]
        public void TearDown()
        {
            _browser.Quit();
        }
    }
}