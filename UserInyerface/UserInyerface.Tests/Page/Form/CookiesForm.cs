using Aquality.Selenium.Elements;
using OpenQA.Selenium;

namespace UserInyerface.Tests.Page.Form
{
    public class CookiesForm : Aquality.Selenium.Forms.Form
    {
        private readonly By _acceptButtonLocator = By.XPath("//button[contains(@class,'button--transparent')]");
        public Button AcceptButton => FormElement.FindChildElement<Button>(_acceptButtonLocator, "Accept cookies");

        public CookiesForm() : base(By.XPath("//div[@class='cookies']"), "Cookies form")
        {
            
        }
    }
}