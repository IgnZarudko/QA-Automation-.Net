using System.Collections.Generic;
using Aquality.Selenium.Elements;
using NUnit.Framework.Internal;
using OpenQA.Selenium;

namespace UserInyerface.Tests.Page.Form
{
    public class LoginDataForm : Aquality.Selenium.Forms.Form
    {
        private readonly By _passwordBoxLocator = By.XPath("//input[@placeholder='Choose Password']");
        private TextBox PasswordBox => FormElement.FindChildElement<TextBox>(_passwordBoxLocator, "Password text box");
        

        private readonly By _emailNameBoxLocator = By.XPath("//input[@placeholder='Your email']");
        private TextBox EmailNameBox => FormElement.FindChildElement<TextBox>(_emailNameBoxLocator, "Email name text box");
        
        
        private readonly By _emailDomainBoxLocator = By.XPath("//input[@placeholder='Domain']");
        private TextBox EmailDomainBox => FormElement.FindChildElement<TextBox>(_emailDomainBoxLocator, "Email domain text box");
        
        
        private readonly By _emailAfterDotDropdownButtonLocator = By.XPath("//div[@class='dropdown__opener']");
        private Button EmailAfterDotDropdownButton => FormElement.FindChildElement<Button>(_emailAfterDotDropdownButtonLocator, "Email after dot dropdown");
        

        private readonly By _emailAfterDotItems = By.XPath("//div[@class='dropdown__list-item']");
        private IList<Button> EmailAfterDotItems => FormElement.FindChildElements<Button>(_emailAfterDotItems, "Item");

        
        private readonly By _acceptTermsCheckboxLocator = By.XPath("//span[@class='checkbox__box']");
        private CheckBox AcceptTermsCheckBox =>
            FormElement.FindChildElement<CheckBox>(_acceptTermsCheckboxLocator, "Accept terms checkbox");
        
        
        private readonly By _nextStepButtonLocator = By.XPath("//a[@class='button--secondary']");
        private Button NextStepButton =>
            FormElement.FindChildElement<Button>(_nextStepButtonLocator, "Confirm login data button");
        
        public LoginDataForm() : base(By.XPath("//div[@class='login-form__container']"), "Login data form")
        {
            
        }

        public void EnterPassword(string text)
        {
            PasswordBox.ClearAndType(text);
        }

        public void EnterEmail(string text)
        {
            EmailNameBox.ClearAndType(text);
        }

        public void EnterDomain(string text)
        {
            EmailDomainBox.ClearAndType(text);
        }

        public void EnterAfterDotData()
        {
            EmailAfterDotDropdownButton.Click();
            EmailAfterDotItems[0].Click();
        }

        public void EnterData(string password, string email, string domain)
        {
            EnterPassword(password);
            EnterEmail(email);
            EnterDomain(domain);
            EnterAfterDotData();
        }

        public void AcceptTerms()
        {
            AcceptTermsCheckBox.Click();
        }

        public void GoToNextStep()
        {
            NextStepButton.Click();
        }
    }
}