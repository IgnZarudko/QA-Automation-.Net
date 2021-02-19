using Aquality.Selenium.Elements;
using OpenQA.Selenium;

namespace UserInyerface.Tests.Page.Form
{
    public class HelpForm : Aquality.Selenium.Forms.Form
    {
        private readonly By _sendToBottomButtonLocator = By.XPath("//button[contains(@class,'send-to-bottom-button')]");
        private Button SendToBottomButton => FormElement.FindChildElement<Button>(_sendToBottomButtonLocator, "Accept cookies");
        
        public HelpForm() : base(By.XPath("//div[@class='help-form']"), "Help form")
        {
            
        }

        public void HideForm()
        {
            SendToBottomButton.Click();
        }
    }
}