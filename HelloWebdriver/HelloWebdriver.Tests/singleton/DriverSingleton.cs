using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace HelloWebdriver.Tests.singleton
{
    public class DriverSingleton
    {
        private static IWebDriver _webDriver;

        public static IWebDriver GetWebDriver(string browser)
        {
            if (_webDriver == null)
            {
                switch (browser)
                {
                    case "firefox":
                        new DriverManager().SetUpDriver(new FirefoxConfig());
                        _webDriver = new FirefoxDriver();
                        break;
                    default:
                        new DriverManager().SetUpDriver(new ChromeConfig());
                        _webDriver = new ChromeDriver();
                        break;
                }
                _webDriver.Manage().Window.Maximize();
                return _webDriver;
            }
            return _webDriver;
        }

        public static void CloseDriver()
        {
            _webDriver.Quit();
            _webDriver = null;
        }
    }
}