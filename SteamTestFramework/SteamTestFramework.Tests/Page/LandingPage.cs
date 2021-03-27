using log4net;
using OpenQA.Selenium;
using SteamTestFramework.Tests.Page.Element;

namespace SteamTestFramework.Tests.Page
{
    public class LandingPage
    {
        public StoreMenu StoreMenu { get; }
        
        private readonly IWebDriver _driver;
        private readonly ILog _log = LogManager.GetLogger(typeof(LandingPage));

        private static string HomePageGiftCardXPath = "//img[@class='home_page_gutter_giftcard']";
        private readonly By _homePateGiftCard = By.XPath(HomePageGiftCardXPath);
        
        private static string DownloadSteamAppButtonXPath = "//a[@class='header_installsteam_btn_content']";
        private readonly By _downloadSteamAppButton = By.XPath(DownloadSteamAppButtonXPath);

        public LandingPage(IWebDriver driver)
        {
            _log.Info("Initializing landing page");
            _driver = driver;
            StoreMenu = new StoreMenu();
        }

        public bool IsDisplayed()
        {
            _log.Info("Checking that landing page is displayed");
            return _driver.FindElement(_homePateGiftCard).Displayed;
        }
        
        public void GoToDownloadSteamAppPage()
        {
            _log.Info("Going to download steam app page");
            _driver.FindElement(_downloadSteamAppButton).Click();
        }
        
        
    }
}