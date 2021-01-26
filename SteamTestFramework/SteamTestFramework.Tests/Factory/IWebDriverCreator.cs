using OpenQA.Selenium;
using SteamTestFramework.Tests.Util;

namespace SteamTestFramework.Tests.Factory
{
    public interface IWebDriverCreator
    {
        public IWebDriver CreateDriver(TestConfig config);
    }
}