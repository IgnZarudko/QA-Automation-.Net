using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Aquality.Selenium.Browsers;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using UserInyerface.Tests.Page;

namespace UserInyerface.Tests
{
    public class UserInyerfaceTests
    {
        private Browser _browser;
        
        private WelcomePage _welcomePage;
        private CreateProfilePage _createProfilePage;
        
        private static string _russianAlphabet = "АаБбВвГгДдЕеЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";
        private static string _englishAlphabet = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsRrSsTtUuVvWwXxYyZz";
        
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
            Randomizer rand = new Randomizer();
            
            string email = rand.GetString(7, _englishAlphabet);
            string domain = rand.GetString(5, _englishAlphabet);
            string password = rand.GetString(9, _englishAlphabet);
            password += rand.GetString(2, _russianAlphabet);
            password += rand.Next() % 10;
            password += email.Last();
            
            yield return new TestCaseData(password, email, domain);
        }

        [TestCaseSource(typeof(UserInyerfaceTests), nameof(PasswordAndEmail))]
        public void EnterInfo_Test(string password, string email, string domain)
        {
            Assert.IsTrue(_welcomePage.State.IsDisplayed, $"{_welcomePage.Name} isn't displayed as expected");
            
            _welcomePage.ToFormButton.Click(); 
            
            Assert.IsTrue(_createProfilePage.State.IsDisplayed, $"{_createProfilePage.Name} isn't displayed as expected");
            
            _createProfilePage.LoginDataForm.PasswordBox.ClearAndType(password);
            _createProfilePage.LoginDataForm.EmailNameBox.ClearAndType(email);
            _createProfilePage.LoginDataForm.EmailDomainBox.ClearAndType(domain);
            _createProfilePage.LoginDataForm.EmailAfterDotDropdownButton.Click();
            _createProfilePage.LoginDataForm.EmailAfterDotItems[3].Click();
            _createProfilePage.LoginDataForm.AcceptTermsCheckBox.Click();
            _createProfilePage.LoginDataForm.ConfirmLoginDataButton.Click();
            
            //a["class='avatar-and-interests__upload-button'"]
            
            
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