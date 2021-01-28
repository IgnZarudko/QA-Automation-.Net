using System.Collections.Generic;
using log4net;
using OpenQA.Selenium;
using SteamTestFramework.Tests.Model;
using SteamTestFramework.Tests.Page.Element;
using SteamTestFramework.Tests.Util;

namespace SteamTestFramework.Tests.Page
{
    public class BrowsingPage
    {
        public StoreMenu StoreMenu { get; }
        
        private readonly IWebDriver _driver;
        private readonly ILog _log = LogManager.GetLogger(typeof(LandingPage));

        private static string PageHeaderXPath = "//h2[@class='pageheader']";
        private readonly By _pageHeader = By.XPath(PageHeaderXPath);

        private static string TopSellersXPath = "//div[@id='tab_select_TopSellers']";
        private readonly By _topSellersTab = By.XPath(TopSellersXPath);

        private static string FirstPageItemsXPath = "//div[@id='TopSellersRows']/a";
        private readonly By _firstPageItems = By.XPath(FirstPageItemsXPath);

        public BrowsingPage(IWebDriver driver)
        {
            _log.Info("Initializing browsing page");
            _driver = driver;
            StoreMenu = new StoreMenu();
        }

        public string PageHeaderText()
        {
            _log.Info("Getting header of page where driver is located");
            return _driver.FindElement(_pageHeader).Text;
        }

        public void GoToTopSellers()
        {
            _log.Info("Going to Top Sellers tab");
            _driver.FindElement(_topSellersTab).Click();
        }

        public bool TopSellersIsActive()
        {
            return _driver.FindElement(_topSellersTab).GetAttribute("class").Contains("active");
        }

        public void GoToGameWithSpecifiedDiscount(CalculationType type, out Game game)
        {
            var firstPageItems = _driver.FindElements(_firstPageItems);

            List<GameItem> gameItems = new List<GameItem>();
            
            _log.Info($"Got {firstPageItems.Count} items on page");
            _log.Info("Getting games only with discount");
            
            foreach (var firstPageItem in firstPageItems)
            {
                GameItem item = new GameItem(firstPageItem);
                
                if (item.Model != null)
                {
                    _log.Info($"item with name {item.Model.Name} has discount of {item.Model.DiscountAmount}");
                    gameItems.Add(item);
                }
            }
            
            _log.Info($"There are {gameItems.Count} games with discount on this page");
            
            switch (type)
            {
                case CalculationType.Highest:
                    _log.Info("Searching game with highest discount");
                    game = GoToGameWithBorderDiscount(gameItems, 1);
                    break;
                default:
                    _log.Info("Searching game with lowest discount");
                    game = GoToGameWithBorderDiscount(gameItems, -1);
                    break;
            }

        }
    
        private Game GoToGameWithBorderDiscount(List<GameItem> gameItems, int compareResult)
        {
            GameItem itemWithBorderDiscount = gameItems[0];

            for (int i = 1; i < gameItems.Count; i++)
            {
                if (gameItems[i].Model.CompareTo(itemWithBorderDiscount.Model) == compareResult)
                {
                    itemWithBorderDiscount = gameItems[i];
                }
            }
            Game model = itemWithBorderDiscount.Model;
            
            _log.Info("Game found, navigating to its page");
            itemWithBorderDiscount.GoToGamePage();

            return model;
        }

    }
}