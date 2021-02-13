using System.Collections.Generic;
using Aquality.Selenium.Elements;
using OpenQA.Selenium;

namespace UserInyerface.Tests.Page.Form
{
    public class LoginDataForm : Aquality.Selenium.Forms.Form
    {
        private readonly By _passwordBoxLocator = By.XPath("//input[@placeholder='Choose Password']");
        public TextBox PasswordBox => FormElement.FindChildElement<TextBox>(_passwordBoxLocator, "Password text box");
        

        private readonly By _emailNameBoxLocator = By.XPath("//input[@placeholder='Your email']");
        public TextBox EmailNameBox => FormElement.FindChildElement<TextBox>(_emailNameBoxLocator, "Email name text box");
        
        
        private readonly By _emailDomainBoxLocator = By.XPath("//input[@placeholder='Domain']");
        public TextBox EmailDomainBox => FormElement.FindChildElement<TextBox>(_emailDomainBoxLocator, "Email domain text box");
        
        
        private readonly By _emailAfterDotDropdownButtonLocator = By.XPath("//div[@class='dropdown__opener']");
        public Button EmailAfterDotDropdownButton => FormElement.FindChildElement<Button>(_emailAfterDotDropdownButtonLocator, "Email after dot dropdown");
        

        private readonly By _emailAfterDotItems = By.XPath("//div[@class='dropdown__list-item']");
        public IList<Button> EmailAfterDotItems => FormElement.FindChildElements<Button>(_emailAfterDotItems, "Item");

        
        private readonly By _acceptTermsCheckboxLocator = By.XPath("//span[@class='checkbox__box']");
        public CheckBox AcceptTermsCheckBox =>
            FormElement.FindChildElement<CheckBox>(_acceptTermsCheckboxLocator, "Accept terms checkbox");
        
        
        private readonly By _nextStepButtonLocator = By.XPath("//a[@class='button--secondary']");
        public Button NextStepButton =>
            FormElement.FindChildElement<Button>(_nextStepButtonLocator, "Confirm login data button");
        
        public LoginDataForm(By locator, string name) : base(locator, name)
        {
            
        }
    }
}