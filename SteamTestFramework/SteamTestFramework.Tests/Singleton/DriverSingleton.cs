using OpenQA.Selenium;
using SteamTestFramework.Tests.factory;

namespace SteamTestFramework.Tests.singleton
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
                        _webDriver = new FirefoxDriverCreator().CreateDriver();
                        break;
                    default:
                        _webDriver = new ChromeDriverCreator().CreateDriver();
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