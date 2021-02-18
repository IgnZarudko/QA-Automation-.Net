using System;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements;
using OpenQA.Selenium;

namespace IFrame.Tests.Page
{
    public class IFramePage : Aquality.Selenium.Forms.Form
    {

        private Button BoldButton => FormElement.FindChildElement<Button>(By.XPath("//button[@aria-label='Bold']"), "Bold button");
        public IFramePage(By locator, string name) : base(locator, name)
        {
        }

        public void EnterText(string text)
        {
            if (State.IsDisplayed)
            {
                SwitchToIFrame();
            }
            AqualityServices.Browser.Driver.FindElementByTagName("p").Clear();
            AqualityServices.Browser.Driver.FindElementByTagName("p").SendKeys(text);
            SwitchToPage();
        }

        public void SetTextToBold()
        {
            if (State.IsDisplayed)
            {
                SwitchToIFrame();
            }
            
            SwitchToPage();
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