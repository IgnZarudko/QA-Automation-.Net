using System.Threading;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SteamTestFramework.Tests.Singleton;

namespace SteamTestFramework.Tests.Page.Element
{
    public class StoreMenu
    {
        private readonly IWebDriver _driver;
        private readonly ILog _log = LogManager.GetLogger(typeof(LandingPage)); 

        private static string BrowsePulldownXPath = "//div[@id='genre_tab']";
        private readonly By _browsePulldown = By.XPath(BrowsePulldownXPath);

        private static string BrowsePopupMenuXPath = "//div[@id='genre_flyout']";
        private readonly By _browsePopupMenu = By.XPath(BrowsePopupMenuXPath);

        public StoreMenu()
        {
            _driver = DriverSingleton.GetWebDriver();
        }

        public IWebElement GetMenuElement(string name)
        {
            _log.Info("Navigating to browse pulldown");
            Actions actions = new Actions(_driver);
            actions.MoveToElement(_driver.FindElement(_browsePulldown)).Build().Perform();
            
            Thread.Sleep(1000);
            
            IWebElement browseMenu = _driver.FindElement(_browsePopupMenu);

            return browseMenu.FindElement(By.XPath($"//a[contains(text(), '{name}')]"));
        }


    }
}