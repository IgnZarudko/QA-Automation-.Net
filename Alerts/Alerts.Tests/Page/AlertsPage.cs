using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Alerts.Tests.Page
{
    public class AlertsPage : Form
    {
        private Button CallDefaultAlertButton => FormElement.FindChildElement<Button>(By.XPath("//button[@onclick='jsAlert()']"));

        private Button CallConfirmAlertButton =>
            FormElement.FindChildElement<Button>(By.XPath("//button[@onclick='jsConfirm()']"));
        
        private Button CallPromptAlertButton => FormElement.FindChildElement<Button>(By.XPath("//button[@onclick='jsPrompt()']"));

        private Label ResultsLabel => FormElement.FindChildElement<Label>(By.XPath("//p[@id='result']"));
        
        public AlertsPage() : base(By.XPath("//div[@id='content']"), "Alerts page")
        {
            
        }

        public void CallDefaultAlert()
        {
            CallDefaultAlertButton.Click();
        }

        public void CallConfirmAlert()
        {
            CallConfirmAlertButton.Click();
        }

        public void CallPromptAlert()
        {
            CallPromptAlertButton.Click();
        }

        public string HandleAlertAccept(string text = null)
        {
            string alertText = AqualityServices.Browser.Driver.SwitchTo().Alert().Text;
            AqualityServices.Browser.HandleAlert(AlertAction.Accept, text);
            
            return alertText;
        }

        public string ResultText()
        {
            return ResultsLabel.Text;
        }
    }
}