using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SteamTestFramework.Tests.Util;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SteamTestFramework.Tests.Factory
{
    public class FirefoxDriverCreator : IWebDriverCreator
    {
        public IWebDriver CreateDriver(TestConfig config)
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            FirefoxOptions options = new FirefoxOptions();
            
            string downloadPath = Path.GetFullPath(config.DownloadDirectory);
            options.SetPreference("browser.download.dir", downloadPath);
            options.SetPreference("browser.download.folderList", 2);
            options.SetPreference("browser.download.manager.alertOnEXEOpen", false);
            options.SetPreference("browser.helperApps.neverAsk.saveToDisk","application/octet-stream");

            return new FirefoxDriver(options);
        }
    }
}