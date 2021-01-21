using OpenQA.Selenium;

namespace HelloWebdriver.Tests.pages
{
    public class CategoryPage : CommonPage
    {
        public CategoryPage(IWebDriver driver) : base(driver)
        {
        }

        public CategoryPage(IWebDriver driver, string pageUrl) : base(driver, pageUrl)
        {
            
        }
    }
}