using System.Text.Json;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace BasicAuth.Tests.Page
{
    public class ResponsePage : Form
    {
        private Button SwitchToAnotherViewButton => FormElement
            .FindChildElement<Button>(By.XPath("//a[@aria-controls='rawdata-panel']"), "Switch to another view button");

        private Label JsonResponse => FormElement
            .FindChildElement<Label>(By.XPath("//pre"), "JSON Response");
        
        
        public ResponsePage() : base(By.XPath("//body"), "Response page")
        {
            
        }

        public void SwitchToOtherView()
        {
            SwitchToAnotherViewButton.Click();
        }

        public Response Response()
        {
            string responseString = JsonResponse.Text;
            return JsonSerializer.Deserialize<Response>(responseString);
        }
        
    }
}