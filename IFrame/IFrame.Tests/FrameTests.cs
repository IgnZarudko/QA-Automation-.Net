using System.Collections.Generic;
using System.Threading;
using Aquality.Selenium.Browsers;
using IFrame.Tests.Page;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;

namespace IFrame.Tests
{
    public class FrameTests
    {
        private Browser _browser;
        
        private IFramePage _iFramePage;
        
        [SetUp]
        public void Setup()
        {
            _browser = AqualityServices.Browser;
            _browser.Maximize();
            _browser.GoTo("http://the-internet.herokuapp.com/iframe");
            
            _iFramePage = new IFramePage();
        }

        private static IEnumerable<TestCaseData> RandomStringToInput()
        {
            Randomizer randomizer = new Randomizer();
            string stringToType = randomizer.GetString(randomizer.Next() % 255 + 1);
            yield return new TestCaseData(stringToType);
        }
        
        [TestCaseSource(typeof(FrameTests), nameof(RandomStringToInput))]
        public void IFrameBoldText_Test(string inputString)
        {
            Assert.IsTrue(_iFramePage.State.IsDisplayed);
            
            _iFramePage.EnterTextAndSelect(inputString);
            
            _iFramePage.SetTextToBold();
            
            Assert.AreEqual(inputString, _iFramePage.BoldTextValue());
        }

        [TearDown]
        public void TearDown()
        {
            _browser.Quit();
        }
    }
}