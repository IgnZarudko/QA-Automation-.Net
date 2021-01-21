using OpenQA.Selenium;

namespace HelloWebdriver.Tests.pages
{
    public class CategoryPage : CommonPage
    {

        private static string LandingPageButtonXPath = "//a[@class=\"_2qvOOvezty _19m_jhLgZR _3UvzPq_Wh1 _22t_hIJUa9 _1dfsrwbzxz\"]";
        private readonly By _landingPageButton = By.XPath(LandingPageButtonXPath);

        public CategoryPage(IWebDriver driver) : base(driver)
        {
        }

        public CategoryPage(IWebDriver driver, string pageUrl) : base(driver, pageUrl)
        {

        }

        public LandingPage GoToLandingPage()
        {
            WaitUntilDisplayed(_landingPageButton);
            Driver.FindElement(_landingPageButton).Click();
            
            return new LandingPage(Driver);
        }
    }
}