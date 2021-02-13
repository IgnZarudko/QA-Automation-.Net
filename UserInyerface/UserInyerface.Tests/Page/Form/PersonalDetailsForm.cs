using OpenQA.Selenium;

namespace UserInyerface.Tests.Page.Form
{
    public class PersonalDetailsForm : Aquality.Selenium.Forms.Form
    {
        public PersonalDetailsForm(By locator, string name) : base(locator, name)
        {
            
        }
    }
}