using Aquality.Selenium.Elements;
using OpenQA.Selenium;
using UserInyerface.Tests.Page.Form;

namespace UserInyerface.Tests.Page
{
    public class CreateProfilePage : Aquality.Selenium.Forms.Form
    {
        private By _loginDataFormLocator = By.XPath("//div[@class='login-form__container']");
        public LoginDataForm LoginDataForm { get; }
        
        private readonly By _cookiesFormLocator = By.XPath("//div[@class='cookies']");
        public CookiesForm CookiesForm { get; }

        private readonly By _helpFormLocator = By.XPath("//div[@class='help-form']");
        public HelpForm HelpForm { get; }
        
        private readonly By _timerLocator = By.XPath("//div[contains(@class,'timer')]");
        
        public Label Timer => FormElement.FindChildElement<Label>(_timerLocator, "Timer");
        public CreateProfilePage(By locator, string name) : base(locator, name)
        {
            CookiesForm = new CookiesForm(_cookiesFormLocator, "Cookies form");
            HelpForm = new HelpForm(_helpFormLocator, "Help form");
            
            LoginDataForm = new LoginDataForm(_loginDataFormLocator, "Login data form");
            
        }
    }
}