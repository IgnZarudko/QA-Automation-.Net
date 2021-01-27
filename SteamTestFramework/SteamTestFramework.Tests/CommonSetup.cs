using System.IO;
using System.Reflection;
using System.Text.Json;
using log4net;
using log4net.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using SteamTestFramework.Tests.Singleton;
using SteamTestFramework.Tests.Util;

namespace SteamTestFramework.Tests
{
    public class CommonSetup
    {
        private static string _settingsPath = "../../../Resources/TestConfig.json";
        protected static TestConfig Config;
        
        protected static IWebDriver WebDriver;

        [SetUp]
        public void Setup()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            
            string jsonString = File.ReadAllText(Path.GetFullPath(_settingsPath));
            Config = JsonSerializer.Deserialize<TestConfig>(jsonString);

            DriverSingleton.Config = Config;
            WebDriver = DriverSingleton.GetWebDriver(); 
        }
        
        [TearDown]
        public void TearDown()
        {
            DriverSingleton.CloseDriver();
        }
    }
}