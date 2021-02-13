using Aquality.Selenium.Elements;
using OpenQA.Selenium;

namespace UserInyerface.Tests.Page
{
    public class WelcomePage : Aquality.Selenium.Forms.Form
    {
        private readonly By _toFormButtonLocator = By.XPath("//a[@class='start__link']");
        public Button ToFormButton => FormElement.FindChildElement<Button>(_toFormButtonLocator, "To form button");

        public WelcomePage(By locator, string name) : base(locator, name)
        {
            
        }

    }
}