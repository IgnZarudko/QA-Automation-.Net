using System.Threading;
using Aquality.Selenium.Browsers;
using IFrame.Tests.Page;
using NUnit.Framework;
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
            
            _iFramePage = new IFramePage(By.XPath("//div[@class='example']"), "IFrame page");
        }

        [Test]
        public void IFrameBoldText_Test()
        {
            Assert.IsTrue(_iFramePage.State.IsDisplayed);
            
            _iFramePage.EnterText("dadad");
            
            Thread.Sleep(2000);

        }

        [TearDown]
        public void TearDown()
        {
            _browser.Quit();
        }
    }
}