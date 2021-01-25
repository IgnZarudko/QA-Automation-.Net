using OpenQA.Selenium;

namespace SteamTestFramework.Tests.factory
{
    public interface IWebDriverCreator
    {
        public IWebDriver CreateDriver();
    }
}