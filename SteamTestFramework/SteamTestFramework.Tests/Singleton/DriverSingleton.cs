using System;
using OpenQA.Selenium;
using SteamTestFramework.Tests.Factory;
using SteamTestFramework.Tests.Util;

namespace SteamTestFramework.Tests.Singleton
{
    public class DriverSingleton
    {
        private static IWebDriver _webDriver;
        public static TestConfig Config { get; set; }

        public static IWebDriver GetWebDriver()
        {
            if (_webDriver == null)
            {
                switch (Config.Browser)
                {
                    case "firefox":
                        _webDriver = new FirefoxDriverCreator().CreateDriver(Config);
                        break;
                    default:
                        _webDriver = new ChromeDriverCreator().CreateDriver(Config);
                        break;
                }
                _webDriver.Manage().Window.Maximize();
                _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
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