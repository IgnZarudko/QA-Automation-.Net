using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Aquality.Selenium.Browsers;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using UserInyerface.Tests.Constants;
using UserInyerface.Tests.Page;
using UserInyerface.Tests.Utils;

namespace UserInyerface.Tests
{
    public class UserInyerfaceTests
    {
        private Browser _browser;
        private Randomizer _randomizer;
        
        private WelcomePage _welcomePage;
        private CreateProfilePage _createProfilePage;
        
        private static string _russianAlphabet = "АаБбВвГгДдЕеЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";
        private static string _englishAlphabet = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsRrSsTtUuVvWwXxYyZz";

        private static string _fileToUploadPath = "..\\..\\..\\Resources\\cheems.png";
        
        [SetUp]
        public void Setup()
        {
            _browser = AqualityServices.Browser;
            _browser.Maximize();
            _browser.GoTo("https://userinyerface.com/game.html%20target=");
            _browser.WaitForPageToLoad();
            
            _randomizer = new Randomizer(12);
            
            _welcomePage = new WelcomePage();
            _createProfilePage = new CreateProfilePage();
        }

        private static IEnumerable<TestCaseData> PasswordAndEmail()
        {
            Randomizer rand = new Randomizer();
            
            string email = rand.GetString(LoginDataLengths.EMAIL_LENGTH, _englishAlphabet);
            string domain = rand.GetString(LoginDataLengths.DOMAIN_LENGTH, _englishAlphabet);
            string password = rand.GetString(LoginDataLengths.PASSWORD_EN_PART_LENGTH, _englishAlphabet);
            password += rand.GetString(LoginDataLengths.PASSWORD_RU_PART_LENGTH, _russianAlphabet);
            password += rand.Next() % 10;
            password += email.Last();
            
            yield return new TestCaseData(password, email, domain);
        }

        [TestCaseSource(typeof(UserInyerfaceTests), nameof(PasswordAndEmail))]
        public void EnterInfo_Test(string password, string email, string domain)
        {
            Assert.IsTrue(_welcomePage.State.IsDisplayed, $"{_welcomePage.Name} isn't displayed as expected");
            
            _welcomePage.GoToForm();
            
            Assert.IsTrue(_createProfilePage.State.IsDisplayed, $"{_createProfilePage.Name} isn't displayed as expected");
            
            _createProfilePage.LoginDataForm.EnterData(password, email, domain);
            
            
            _createProfilePage.LoginDataForm.AcceptTerms();
            
            
            _createProfilePage.LoginDataForm.GoToNextStep();
            
            Assert.IsTrue(_createProfilePage.InterestsAndImageForm.State.IsDisplayed, $"{_createProfilePage.InterestsAndImageForm.Name} isn't displayed as expected");
            
            _createProfilePage.InterestsAndImageForm.ImageUploadButton.Click();
            
            UploadManager.UploadFile(_fileToUploadPath);

            Assert.IsTrue(_createProfilePage.InterestsAndImageForm.UploadedImage.State.WaitForDisplayed(), $"{_createProfilePage.InterestsAndImageForm.UploadedImage.Name} isn't displayed as expected");
            
            _createProfilePage.InterestsAndImageForm.UnselectAll();

            int amountOfInterestsAvailable = _createProfilePage.InterestsAndImageForm.AmountOfInterestsAvailable();
            HashSet<int> checkedIndexes = new HashSet<int>();
            for (int i = 0; i < 3;)
            {
                int nextIndex = _randomizer.Next() % amountOfInterestsAvailable;
                if (!checkedIndexes.Contains(nextIndex))
                {
                    _createProfilePage.InterestsAndImageForm.ChooseInterest(nextIndex);
                    checkedIndexes.Add(nextIndex);
                    i++;
                } 
            }
            
            _createProfilePage.InterestsAndImageForm.GoToNextStep();

            Assert.IsTrue(_createProfilePage.PersonalDetailsForm.State.IsDisplayed, $"{_createProfilePage.PersonalDetailsForm.Name} isn't displayed as expected");
        }
        
        [Test]
        public void HideHelp_Test()
        {
            Assert.IsTrue(_welcomePage.State.IsDisplayed, $"{_welcomePage.Name} isn't displayed as expected");
            
            _welcomePage.GoToForm();
            
            Assert.IsTrue(_createProfilePage.State.IsDisplayed, $"{_createProfilePage.Name} isn't displayed as expected");
            
            _createProfilePage.HelpForm.HideForm();
                
            Assert.IsFalse(_createProfilePage.HelpForm.State.IsDisplayed, $"{_createProfilePage.HelpForm.Name} is still displayed");
        }
        
        [Test]
        public void AcceptCookies_Test()
        {
            Assert.IsTrue(_welcomePage.State.IsDisplayed, $"{_welcomePage.Name} isn't displayed as expected");
            
            _welcomePage.GoToForm(); 
            
            Assert.IsTrue(_createProfilePage.State.IsDisplayed, $"{_createProfilePage.Name} isn't displayed as expected");
            
            _createProfilePage.CookiesForm.AcceptCookies();

            Assert.IsFalse(_createProfilePage.CookiesForm.State.IsDisplayed, $"{_createProfilePage.CookiesForm.Name} is still displayed");
        }

        [Test]
        public void Timer_Test()
        {
            Assert.IsTrue(_welcomePage.State.IsDisplayed, $"{_welcomePage.Name} isn't displayed as expected");
            
            _welcomePage.GoToForm();
            
            Assert.IsTrue(_createProfilePage.State.IsDisplayed, $"{_createProfilePage.Name} isn't displayed as expected");

            Assert.AreEqual("00", _createProfilePage.TimerValue());
        }

        [TearDown]
        public void TearDown()
        {
            _browser.Quit();
        }
    }
}