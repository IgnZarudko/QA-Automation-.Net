using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HelloWebdriver.Tests.model;
using OpenQA.Selenium;

namespace HelloWebdriver.Tests.pages
{
    public class LandingPage : CommonPage
    {
        private static string FilterString = "скидки и акции, начать продавать на маркете, покупки, уцененные товары";
        
        private static string AdBannerXPath = "//img[@class=\"_10KRGrktZR\"]";
        private readonly By _adBanner = By.XPath(AdBannerXPath);
        
        private static string LoginButtonXPath =
            "//a[@class=\"zsSJkfeAPw _2sWJL7-h2E eD98J84A1g _36y1jOUHs5 _1xlw1z4vqj\"]";
        private readonly By _loginButton = By.XPath(LoginButtonXPath);
        
        private static string ProfilePopupButtonXPath = "//button[@class=\"_1YeOF5Jcfi _3rdp77Plde\"]";
        private readonly By _profilePopupButton = By.XPath(ProfilePopupButtonXPath);
        
        private static string ProfileUsernameXPath = "//div[@class=\"_2H0oszg-Y8 p0V3oBq2SG _3rYu_TSC-x\"]";
        private readonly By _profileUsername = By.XPath(ProfileUsernameXPath);
        
        private static string ProfileEmailXPath = "//div[@class=\"_10BSdt90pf _3rYu_TSC-x\"]";
        private readonly By _profileEmail = By.XPath(ProfileEmailXPath);

        private static string PopularCategoriesXPath = "//a[@class=\"_3Lwc_UVFq4\"]";
        private readonly By _popularCategories = By.XPath(PopularCategoriesXPath);

        private static string AllCategoriesPopupXPath = "//button[@class=\"zsSJkfeAPw _16jABpOZ2- gjdzW5ajbI _3WgR56k47x\"]";
        private readonly By _allCategoriesPopup = By.XPath(AllCategoriesPopupXPath);

        private static string CategoriesXPath = "//button[@class=\"_35SYuInI1T _2BRGNp7I5O\"]/a";
        private readonly By _categories = By.XPath(CategoriesXPath);
        

        public LandingPage(IWebDriver driver) : base(driver)
        {
        }

        public LandingPage(IWebDriver driver, string pageUrl) : base(driver, pageUrl)
        {
            
        }
        
        public void Open()
        {
            Driver.Navigate().GoToUrl(PageUrl);
        }

        public bool IsBannerDisplayed()
        {
            return Driver.FindElement(_adBanner).Displayed;
        }

        public LoginPage GoToLogin()
        {
            Driver.FindElement(_loginButton).Click();
            Driver.SwitchTo().Window(Driver.WindowHandles[^1]);
            
            return new LoginPage(Driver);
        }

        public User CurrentUser()
        {
            WaitUntilDisplayed(_profilePopupButton);
            Driver.FindElement(_profilePopupButton).Click();

            string username = Driver.FindElement(_profileUsername).Text;
            string userEmail = Driver.FindElement(_profileEmail).Text;
            
            return new User("", "", username, userEmail);
        }

        public HashSet<String> PopularCategoriesUrls()
        {
            ReadOnlyCollection<IWebElement> webElements = Driver.FindElements(_popularCategories);
            
            HashSet<string> popularCategoriesUrls = new HashSet<string>();
            
            foreach (var element in webElements)
            {
                if (!FilterString.Contains(element.Text.ToLower()) && element.Displayed)
                {
                    popularCategoriesUrls.Add(element.GetAttribute("href"));
                }
            }

            return popularCategoriesUrls;
        }

        public CategoryPage GoToRandomPopularCategory(out string categoryUrl)
        {
            
            ReadOnlyCollection<IWebElement> webElements = Driver.FindElements(_popularCategories);
            List<IWebElement> popularCategoriesLinks = new List<IWebElement>();
            
            foreach (var element in webElements)
            {
                if (!FilterString.Contains(element.Text) && element.Displayed)
                {
                    popularCategoriesLinks.Add(element);
                }
            }

            int amountOfCategories = popularCategoriesLinks.Count;
            int randomIndex = new Random(amountOfCategories).Next() % amountOfCategories;
            
            categoryUrl = popularCategoriesLinks[randomIndex].GetAttribute("href");
            popularCategoriesLinks[randomIndex].Click();
            
            return new CategoryPage(Driver);
        }

        public List<(string categoryName, string categoryUrl)> AllCategories()
        {
            WaitUntilDisplayed(_allCategoriesPopup);
            Driver.FindElement(_allCategoriesPopup).Click();
            
            WaitUntilDisplayed(_categories);
            
            List<(string categoryName, string categoryUrl)> listOfCategories = new List<(string categoryName, string categoryUrl)>();

            foreach (var element in Driver.FindElements(_categories))
            {
                if (!FilterString.Contains(element.Text.ToLower()))
                {
                    listOfCategories.Add((element.Text, element.GetAttribute("href")));
                }
            }

            return listOfCategories;
        }
        
    }
}