using OpenQA.Selenium;

namespace UserInyerface.Tests.Page.Form
{
    public class PersonalDetailsForm : Aquality.Selenium.Forms.Form
    {
        public PersonalDetailsForm() : base(By.XPath("//div[@class='personal-details__form']"), "Personal details form")
        {
            
        }
    }
}