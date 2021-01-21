using System.Collections.Generic;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace HelloWebdriver.Tests.pages
{
    public class LandingPage : CommonPage
    {
        private static string LoginButtonXPath =
            "//a[@class=\"zsSJkfeAPw _2sWJL7-h2E eD98J84A1g _36y1jOUHs5 _1xlw1z4vqj\"]";
        private readonly By _loginButton = By.XPath(LoginButtonXPath);


        private static string CategoriesXPath = "//a[@class=\"_3Lwc_UVFq4\"]";
        private readonly By _categories = By.XPath(CategoriesXPath);

        private static string AdBannerXPath = "//img[@class=\"_10KRGrktZR\"]";
        private readonly By _adBanner = By.XPath(AdBannerXPath);
        
        public LandingPage(IWebDriver driver) : base(driver)
        {
        }

        public LandingPage(IWebDriver driver, string pageUrl) : base(driver, pageUrl)
        {
            
        }
        
        public void Open()
        {
            Driver.Navigate().GoToUrl(PageUrl);
        }

        public bool IsBannerDisplayed()
        {
            return Driver.FindElement(_adBanner).Displayed;
        }

        public void GoToLogin()
        {
            Driver.FindElement(_loginButton).Click();
        }
    }
}