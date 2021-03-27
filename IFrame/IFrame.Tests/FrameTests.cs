using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using IFrame.Tests.Page;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;

namespace IFrame.Tests
{
    public class FrameTests
    {
        private Browser _browser;

        private static string _url;
        
        [SetUp]
        public void Setup()
        {
            _url = AqualityServices.Get<ISettingsFile>().GetValue<string>(".hostUrl");
            
            _browser = AqualityServices.Browser;
            _browser.Maximize();
            _browser.GoTo(_url);
            _browser.WaitForPageToLoad();
        }

        private static IEnumerable<TestCaseData> RandomStringToInput()
        {
            Randomizer randomizer = new Randomizer();
            string stringToType = randomizer.GetString();
            yield return new TestCaseData(stringToType);
        }
        
        [TestCaseSource(typeof(FrameTests), nameof(RandomStringToInput))]
        public void IFrameBoldText_Test(string inputString)
        {
            IFramePage iFramePage = new IFramePage();
            
            Assert.IsTrue(iFramePage.State.IsDisplayed);
            
            iFramePage.EnterTextAndSelect(inputString);
            
            iFramePage.SetTextToBold();
            
            Assert.AreEqual(inputString, iFramePage.BoldTextValue());
        }

        [TearDown]
        public void TearDown()
        {
            _browser.Quit();
        }
    }
}