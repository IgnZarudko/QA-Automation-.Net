using Aquality.Selenium.Elements;
using OpenQA.Selenium;
using UserInyerface.Tests.Page.Form;

namespace UserInyerface.Tests.Page
{
    public class CreateProfilePage : Aquality.Selenium.Forms.Form
    {
        public LoginDataForm LoginDataForm { get; }
        
        public InterestsAndImageForm InterestsAndImageForm { get; }
        
        public PersonalDetailsForm PersonalDetailsForm { get; }
        
        public CookiesForm CookiesForm { get; }
        
        public HelpForm HelpForm { get; }
        
        
        private readonly By _timerLocator = By.XPath("//div[contains(@class,'timer')]");
        public Label Timer => FormElement.FindChildElement<Label>(_timerLocator, "Timer");
        
        
        public CreateProfilePage() : base(By.XPath("//div[@class='game view']"), "Create profile page")
        {

            CookiesForm = new CookiesForm();
            HelpForm = new HelpForm();
            
            LoginDataForm = new LoginDataForm();
            InterestsAndImageForm = new InterestsAndImageForm();
            PersonalDetailsForm = new PersonalDetailsForm();
        }
    }
}