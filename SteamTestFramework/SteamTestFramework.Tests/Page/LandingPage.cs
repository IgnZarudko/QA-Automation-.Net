using OpenQA.Selenium;

namespace SteamTestFramework.Tests.Page
{
    public class LandingPage
    {
        private readonly IWebDriver _driver;

        private static string DownloadSteamAppButtonXPath = "//a[@class='header_installsteam_btn_content']";
        private readonly By _downloadSteamAppButton = By.XPath(DownloadSteamAppButtonXPath);
        
        public LandingPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void GoToDownloadSteamAppPage()
        {
            _driver.FindElement(_downloadSteamAppButton).Click();
        }
        
    }
}