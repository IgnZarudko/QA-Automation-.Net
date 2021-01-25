using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SteamTestFramework.Tests.factory
{
    public class ChromeDriverCreator : IWebDriverCreator
    {
        public IWebDriver CreateDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            return new ChromeDriver();
        }
    }
}