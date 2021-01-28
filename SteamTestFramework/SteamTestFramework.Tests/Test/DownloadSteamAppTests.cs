using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using NUnit.Framework;
using SteamTestFramework.Tests.Page;
using SteamTestFramework.Tests.Util.TestData;
using SteamTestFramework.Tests.Util.Wait;

namespace SteamTestFramework.Tests.Test
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
             
            FileWaiter.WaitUntilFileDownloads(Config);

            Assert.IsTrue(File.Exists(Path.GetFullPath(Config.DownloadDirectory + "\\" + expectedFileName)), "File wasn't downloaded");
        }

        [TearDown]
        public void TearDownAdditional()
        {
            FileInfo[] files = new DirectoryInfo(Path.GetFullPath(Config.DownloadDirectory)).GetFiles();
            foreach (var file in files)
            {
                file.Delete();
            }
        }
    }
}