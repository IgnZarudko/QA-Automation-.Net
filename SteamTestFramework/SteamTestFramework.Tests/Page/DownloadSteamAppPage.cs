using OpenQA.Selenium;

namespace SteamTestFramework.Tests.Page
{
    public class DownloadSteamAppPage
    {
        private readonly IWebDriver _driver;

        private static string DownloadSteamAppButtonXPath = "//a[@class='about_install_steam_link']";
        private readonly By _installSteamAppButton = By.XPath(DownloadSteamAppButtonXPath);

        public DownloadSteamAppPage(IWebDriver driver)
        {
            _driver = driver;
        }
        
        public void DownloadSteamAppInstaller()
        {
            _driver.FindElement(_installSteamAppButton).Click();
        }
    }
}