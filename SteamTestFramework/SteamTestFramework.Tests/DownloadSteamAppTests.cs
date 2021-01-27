using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using NUnit.Framework;
using SteamTestFramework.Tests.Page;
using SteamTestFramework.Tests.Util;

namespace SteamTestFramework.Tests
{
    public class DownloadSteamAppTests : CommonSetup
    {
        private static string _testDataPath = "../../../Resources/DownloadTestData.json";

        private static LandingPage _landingPage;
        private static DownloadSteamAppPage _downloadSteamAppPage;

        [SetUp] 
        public void SetupAdditional()
        {
            WebDriver.Navigate().GoToUrl(Config.StartUrl);
            
            _landingPage = new LandingPage(WebDriver);
            _downloadSteamAppPage = new DownloadSteamAppPage(WebDriver);
        }

        private static IEnumerable<TestCaseData> FileNameData()
        {
            string jsonString = File.ReadAllText(Path.GetFullPath(_testDataPath));
            DownloadTestData data = JsonSerializer.Deserialize<DownloadTestData>(jsonString);
            yield return new TestCaseData(data.ExpectedFileName);
        }

        [TestCaseSource(typeof(DownloadSteamAppTests), nameof(FileNameData))]
        public void DownloadSteamApp_Successful(string expectedFileName)
        {
            Assert.IsTrue(_landingPage.IsDisplayed());
            _landingPage.GoToDownloadSteamAppPage();
            
            Assert.IsTrue(_downloadSteamAppPage.IsDisplayed());
            _downloadSteamAppPage.DownloadSteamAppInstaller();

            if (Config.Browser == "chrome")
                Thread.Sleep(2000); 
            
            //так как это не ожидание внутри страницы
            //я себе позволил такую шалость, потому что зашёл в тупик
            //памагити тупенькому((

            Assert.IsTrue(File.Exists(Path.GetFullPath(Config.DownloadDirectory + "\\" + expectedFileName)), "File wasn't downloaded");
        }

        [TearDown]
        public void TearDownAdditional()
        {
            string[] files = Directory.GetFiles(Path.GetFullPath(Config.DownloadDirectory));
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}