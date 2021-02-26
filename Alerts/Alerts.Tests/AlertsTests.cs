using System.Collections.Generic;
using Alerts.Tests.Page;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Alerts.Tests
{
    public class AlertsTests
    {
        private static string _hostUrl;
        private static Browser _browser;

        [SetUp]
        public void Setup()
        {
            _hostUrl = AqualityServices.Get<ISettingsFile>().GetValue<string>(".hostUrl");

            _browser = AqualityServices.Browser;
            _browser.Maximize();
            _browser.GoTo(_hostUrl);
            _browser.WaitForPageToLoad();
        }

        public static IEnumerable<TestCaseData> AlertsTextsAndInputData()
        {
            string alertMessage = "I am a JS Alert";
            string alertResult = "You successfully clicked an alert";
            string confirmMessage = "I am a JS Confirm";
            string confirmResult = "You clicked: Ok";
            string promptMessage = "I am a JS prompt";
            
            Randomizer randomizer = new Randomizer();
            string promptInputText = randomizer.GetString();
            string promptResult = $"You entered: {promptInputText}";

            yield return new TestCaseData(alertMessage, alertResult, confirmMessage, confirmResult, promptMessage, promptInputText,
                promptResult);
        }

        [TestCaseSource(typeof(AlertsTests), nameof(AlertsTextsAndInputData))]
        public void JSConfirmTest(string alertMessage, string alertResult, string confirmMessage, string confirmResult, string promptMessage, string promptInputText, string promptResult)
        {
            AlertsPage alertsPage = new AlertsPage();

            alertsPage.CallDefaultAlert();

            string alertText = alertsPage.HandleAlertAccept();
            
            Assert.AreEqual(alertMessage, alertText, "Text on alert is not what expected");
            
            Assert.AreEqual(alertsPage.ResultText(), alertResult, "Results are not equal");

            alertsPage.CallConfirmAlert();
            
            alertText = alertsPage.HandleAlertAccept();
            
            Assert.AreEqual(confirmMessage, alertText, "Text on alert is not what expected");
            
            Assert.AreEqual(alertsPage.ResultText(), confirmResult, "Results are not equal");
            
            alertsPage.CallPromptAlert();
            
            alertText = alertsPage.HandleAlertAccept(promptInputText);
             
            Assert.AreEqual(promptMessage, alertText, "Text on alert is not what expected");
            
            Assert.AreEqual(alertsPage.ResultText(), promptResult, "Results are not equal");

        }

        [TearDown]
        public void TearDown()
        {
            _browser.Quit();
        }
    }
}