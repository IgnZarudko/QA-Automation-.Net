using OpenQA.Selenium;

namespace HelloWebdriver.Tests.pages
{
    public class LoginPage : CommonPage
    {
        private static string LoginFieldXPath = "//input[@type=\"text\"]";
        private readonly By _loginField = By.XPath(LoginFieldXPath);
        
        private static string PasswordFieldXPath = "//input[@type=\"password\"]";
        private readonly By _passwordField = By.XPath(PasswordFieldXPath);

        private static string SubmitButtonXPath = "//button[@type=\"submit\"]";
        private readonly By _submitButton = By.XPath(SubmitButtonXPath);

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public LoginPage(IWebDriver driver, string pageUrl) : base(driver, pageUrl)
        {
            
        }

        public void EnterLogin(string login)
        {
            WaitUntilDisplayed(_loginField);
            Driver.FindElement(_loginField).SendKeys(login);
        }
        
        public void EnterPassword(string password)
        {
            WaitUntilDisplayed(_passwordField);
            Driver.FindElement(_passwordField).SendKeys(password);
        }
        
        public void SubmitLogin()
        {
            Driver.FindElement(_submitButton).Click();
        }

        public LandingPage SubmitPassword()
        {
            Driver.FindElement(_submitButton).Click();
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
            
            return new LandingPage(Driver);
        }

    }
}