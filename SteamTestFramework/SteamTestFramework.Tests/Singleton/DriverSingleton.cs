using OpenQA.Selenium;
using SteamTestFramework.Tests.Factory;
using SteamTestFramework.Tests.Util;

namespace SteamTestFramework.Tests.Singleton
{
    public class DriverSingleton
    {
        private static IWebDriver _webDriver;

        public static IWebDriver GetWebDriver(TestConfig config)
        {
            if (_webDriver == null)
            {
                switch (config.Browser)
                {
                    case "firefox":
                        _webDriver = new FirefoxDriverCreator().CreateDriver(config);
                        break;
                    default:
                        _webDriver = new ChromeDriverCreator().CreateDriver(config);
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