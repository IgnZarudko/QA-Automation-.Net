using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace HelloWebdriver.Tests.pages
{
    public abstract class CommonPage
    {
        protected readonly IWebDriver Driver;
        protected readonly string PageUrl = "https://market.yandex.ru/";
        public CommonPage(IWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(Driver, this);
        }

        public CommonPage(IWebDriver driver, string pageUrl)
        {
            PageUrl = pageUrl;
            Driver = driver;
            PageFactory.InitElements(Driver, this);
        }

        protected void WaitUntilDisplayed(By element)
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(10))
                .Until(driver => driver.FindElement(element).Displayed);
        }
    }
}