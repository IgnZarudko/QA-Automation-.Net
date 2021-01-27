using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using log4net;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SteamTestFramework.Tests.Page;
using SteamTestFramework.Tests.Util;

namespace SteamTestFramework.Tests
{
    public class DiscountCalculationTest : CommonSetup
    {
        private static string _dataPath = "../../../Resources/Locale/DiscountCalculationTestData.json";
        
        private static LandingPage _landingPage;

        [SetUp]
        public void SetupAdditional()
        {
            WebDriver.Navigate().GoToUrl(Config.StartUrl);
            
            _landingPage = new LandingPage(WebDriver);
        }

        private static IEnumerable<TestCaseData> DiscountCalculationData()
        {
            _dataPath = _dataPath.Replace("Locale", $"Locale-{Config.Language}");
            string jsonString = File.ReadAllText(Path.GetFullPath(_dataPath));
            JArray jArray = JArray.Parse(jsonString);

            foreach (var jObject in jArray)
            {
                DiscountCalculationTestData data =
                    JsonSerializer.Deserialize<DiscountCalculationTestData>(jObject.ToString());
                yield return new TestCaseData(data.Genre, data.DiscountType);
            }
        }
        
        [TestCaseSource(typeof(DiscountCalculationTest), nameof(DiscountCalculationData))]
        public void DiscountCalculation_IsCorrect(string genre, string discountType)
        {
            LogManager.GetLogger("loggerInTest").Info($"got Genre {genre}");
            LogManager.GetLogger("loggerInTest").Info($"got DiscountType {discountType}");
            
            Assert.IsTrue(_landingPage.IsDisplayed());
            
            _landingPage.StoreMenu.GetMenuElement(genre).Click();
            
            Thread.Sleep(1000);
        }
        
    }
}