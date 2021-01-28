using log4net;
using OpenQA.Selenium;
using SteamTestFramework.Tests.Model;
using SteamTestFramework.Tests.Page.Element;

namespace SteamTestFramework.Tests.Page
{
    public class GamePage
    {
        public StoreMenu StoreMenu { get; }
        
        private readonly IWebDriver _driver;
        private readonly ILog _log = LogManager.GetLogger(typeof(LandingPage));

        private static string GameNameHeaderXPath = "//div[@class='apphub_AppName']";
        private readonly By _gameNameHeader = By.XPath(GameNameHeaderXPath);
        
        private static string AddToCartSectionXpath ="//div[@class='game_area_purchase_game_wrapper']";
        private readonly By _addToCartSection = By.XPath(AddToCartSectionXpath);

        private static string ChildOldPriceXPath = ".//div[@class='discount_original_price']";
        private readonly By _childOldPrice = By.XPath(ChildOldPriceXPath);

        private static string ChildNewPriceXPath = ".//div[@class='discount_final_price']";
        private readonly By _childNewPrice = By.XPath(ChildNewPriceXPath);
        
        private static string ChildDiscountAmountXPath = ".//div[@class='discount_pct']";
        private readonly By _childDiscountAmount = By.XPath(ChildDiscountAmountXPath);


        public GamePage(IWebDriver driver)
        {
            _log.Info("Initializing game page");
            _driver = driver;
            StoreMenu = new StoreMenu();
        }
        
        public Game GameInfo()
        {
            IWebElement addToCartSection = _driver.FindElement(_addToCartSection);

            string name = _driver.FindElement(_gameNameHeader).Text;

            string oldPrice = addToCartSection.FindElement(_childOldPrice).Text;
            string newPrice = addToCartSection.FindElement(_childNewPrice).Text.Split(" ")[0];

            string discountStr = addToCartSection.FindElement(_childDiscountAmount).Text;
            int discountAmount = int.Parse(discountStr.Substring(1, discountStr.Length - 2));
            
            return new Game(name, oldPrice, newPrice, discountAmount);
        }

    }
}