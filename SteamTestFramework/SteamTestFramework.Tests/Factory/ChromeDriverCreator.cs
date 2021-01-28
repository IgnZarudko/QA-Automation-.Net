using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SteamTestFramework.Tests.Util.Config;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SteamTestFramework.Tests.Factory
{
    public class ChromeDriverCreator : IWebDriverCreator
    {
        public IWebDriver CreateDriver(TestConfig config)
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            ChromeOptions options = new ChromeOptions();
            
            string downloadPath = Path.GetFullPath(config.DownloadDirectory);
            options.AddUserProfilePreference("download.default_directory", downloadPath);
            options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            // options.AddUserProfilePreference("download.prompt_for_download", false);
            // options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("safebrowsing.enabled", true);
            
            options.AddArgument("--lang=" + config.Language);
            
            return new ChromeDriver(options);
        }
    }
}