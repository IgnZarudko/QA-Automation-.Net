using System;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace IFrame.Tests.Page
{
    public class IFramePage : Aquality.Selenium.Forms.Form
    {

        private Button BoldButton => FormElement.FindChildElement<Button>(By.XPath("//button[@aria-label='Bold']"), "Bold button");

        private ITextBox TextBox => ElementFactory.GetTextBox(By.XPath("//p"), "Text field of iFrame");
        private ITextBox BoldTextBox => ElementFactory.GetTextBox(By.XPath("//strong"), "Bold text");
        
        public IFramePage() : base(By.XPath("//div[@class='example']"), "IFrame page")
        {
            
        }

        public void EnterTextAndSelect(string text)
        {
            if (State.IsDisplayed)
            {
                SwitchToIFrame();
            }
            TextBox.SendKeys(Keys.Control+"a");
            TextBox.Type(text);
            TextBox.SendKeys(Keys.Control+"a");
            SwitchToPage();
        }

        public void SetTextToBold()
        {
            if (!State.IsDisplayed)
            {
                SwitchToPage();
            }
            BoldButton.Click();
        }

        public string BoldTextValue()
        {
            if (State.IsDisplayed)
            {
                SwitchToIFrame();
            }
            string text = BoldTextBox.Text;
            SwitchToPage();
            return text;
        }
        
        private void SwitchToIFrame()
        {
            AqualityServices.Browser.Driver.SwitchTo().Frame("mce_0_ifr");
        }

        private void SwitchToPage()
        {
            AqualityServices.Browser.Driver.SwitchTo().DefaultContent();
        }
    }
}