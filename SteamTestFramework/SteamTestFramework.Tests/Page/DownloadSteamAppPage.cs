using log4net;
using OpenQA.Selenium;

namespace SteamTestFramework.Tests.Page
{
    public class DownloadSteamAppPage
    {
        private readonly IWebDriver _driver;
        private readonly ILog _log = LogManager.GetLogger(typeof(DownloadSteamAppPage));

        private static string DownloadSteamAppButtonXPath = "//a[@class='about_install_steam_link']";
        private readonly By _installSteamAppButton = By.XPath(DownloadSteamAppButtonXPath);
        
        public DownloadSteamAppPage(IWebDriver driver)
        {
            _log.Info("Initializing download steam page");
            _driver = driver;
        }

        public bool IsDisplayed()
        {
            _log.Info("Checking that download steam page is displayed");
            return _driver.FindElement(_installSteamAppButton).Displayed;
        }
        
        public void DownloadSteamAppInstaller()
        {
            _log.Info("Trying to download steam app installer");
            _driver.FindElement(_installSteamAppButton).Click();
        }
    }
}