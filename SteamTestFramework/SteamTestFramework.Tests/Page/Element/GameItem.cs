using System;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SteamTestFramework.Tests.Model;
using SteamTestFramework.Tests.Singleton;

namespace SteamTestFramework.Tests.Page.Element
{
    public class GameItem
    {
        public Game Model
        {
            get
            {
                string name = _parentElement.FindElement(_name).Text;
                try
                {
                    string originalPrice = _parentElement.FindElement(_originalPrice).Text;

                    string newPrice = _parentElement.FindElement(_newPrice).Text;

                    string discountStr = _parentElement.FindElement(_discountAmount).Text;
                    int discountAmount = int.Parse(discountStr.Substring(1, discountStr.Length - 2));

                    return new Game(name, originalPrice, newPrice, discountAmount);
                }
                catch (NoSuchElementException e)
                {
                    return null;
                }
            }
        }
        
        private readonly ILog _log = LogManager.GetLogger(typeof(LandingPage));
        
        private readonly IWebElement _parentElement;
        
        private static string NameXPath = ".//div[@class='tab_item_name']";
        private readonly By _name = By.XPath(NameXPath);

        private static string OriginalPriceXPath = ".//div[@class='discount_original_price']";
        private readonly By _originalPrice = By.XPath(OriginalPriceXPath);

        private static string NewPriceXPath = ".//div[@class='discount_final_price']";
        private readonly By _newPrice = By.XPath(NewPriceXPath);

        private static string DiscountAmountXPath = ".//div[@class='discount_pct']";
        private readonly By _discountAmount = By.XPath(DiscountAmountXPath);

        public GameItem(IWebElement parentElement)
        {
            _parentElement = parentElement;
        }

        public void GoToGamePage()
        {
            _log.Info("Going to game page");
            _parentElement.Click();
        }
    }
}