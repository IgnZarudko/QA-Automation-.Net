using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using log4net;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SteamTestFramework.Tests.Model;
using SteamTestFramework.Tests.Page;
using SteamTestFramework.Tests.Util;

namespace SteamTestFramework.Tests
{
    public class DiscountCalculationTest : CommonSetup
    {
        private static readonly string DataPath = $"../../../Resources/Locale-{Config.Language}/DiscountCalculationTestData.json";
        
        private static LandingPage _landingPage;
        private static BrowsingPage _browsingPage;

        [SetUp]
        public void SetupAdditional()
        {
            WebDriver.Navigate().GoToUrl(Config.StartUrl);
            
            _landingPage = new LandingPage(WebDriver);
            _browsingPage = new BrowsingPage(WebDriver);
        }

        private static IEnumerable<TestCaseData> DiscountCalculationData()
        {
            string jsonString = File.ReadAllText(Path.GetFullPath(DataPath));
            JArray jArray = JArray.Parse(jsonString);

            foreach (var jObject in jArray)
            {
                DiscountCalculationTestData data =
                    JsonSerializer.Deserialize<DiscountCalculationTestData>(jObject.ToString());

                CalculationType type;
                switch (data.DiscountType)
                {
                    case "Highest":
                        type = CalculationType.Highest;
                        break;
                    default:
                        type = CalculationType.Lowest;
                        break;
                }
                
                yield return new TestCaseData(data.Genre, type);
            }
        }
        
        [TestCaseSource(typeof(DiscountCalculationTest), nameof(DiscountCalculationData))]
        public void DiscountCalculation_IsCorrect(string genre, CalculationType discountType)
        {
            LogManager.GetLogger("loggerInTest").Info($"got Genre {genre}");
            LogManager.GetLogger("loggerInTest").Info($"got DiscountType {discountType}");
            
            Assert.IsTrue(_landingPage.IsDisplayed());
            
            _landingPage.StoreMenu.GetMenuElement(genre).Click();
            
            Assert.IsTrue(_browsingPage.PageHeaderText().Contains(genre));
            
            _browsingPage.GoToTopSellers();

            Assert.IsTrue(_browsingPage.TopSellersIsActive());
            
            Game game;
            _browsingPage.GoToGameWithSpecifiedDiscount(discountType, out game);

            Thread.Sleep(1000);
        }
        
    }
}