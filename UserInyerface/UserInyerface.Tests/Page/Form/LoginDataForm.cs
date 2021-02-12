using Aquality.Selenium.Elements;
using OpenQA.Selenium;

namespace UserInyerface.Tests.Page.Form
{
    public class LoginDataForm : Aquality.Selenium.Forms.Form
    {
        private readonly By _passwordBoxLocator = By.XPath("//input[@placeholder='Choose Password']");
        public TextBox PasswordBox => FormElement.FindChildElement<TextBox>(_passwordBoxLocator, "Password text box");

        private readonly By _emailNameBoxLocator = By.XPath("");
        public TextBox EmailNameBox => FormElement.FindChildElement<TextBox>(_emailNameBoxLocator, "Email name text box");
        
        private readonly By _emailDomainBoxLocator = By.XPath("");
        public TextBox EmailDomainBox => FormElement.FindChildElement<TextBox>(_emailDomainBoxLocator, "Email domain text box");

        private readonly By _emailAfterDotBoxLocator = By.XPath("");

        public ComboBox EmailAfterDotBox => 
            FormElement.FindChildElement<ComboBox>(_emailAfterDotBoxLocator, "Email after dot combobox");
        
        
        private readonly By _confirmLoginDataButtonLocator = By.XPath("//a[@class='button--secondary']");

        public Button ConfirmLoginDataButton =>
            FormElement.FindChildElement<Button>(_confirmLoginDataButtonLocator, "Confirm login data button");
        
        public LoginDataForm(By locator, string name) : base(locator, name)
        {
            
        }
    }
}