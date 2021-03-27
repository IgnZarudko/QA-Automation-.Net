using System.Diagnostics;
using System.IO;
using Aquality.Selenium.Browsers;

namespace UserInyerface.Tests.Utils
{
    public class UploadManager
    {
        private static string _uploaderExecutablePath = "..\\..\\..\\Utils\\Uploader.exe";
        
        public static void UploadFile(string fileToUploadPath)
        {
            var browser = AqualityServices.Browser;
            switch(browser.BrowserName)
            {
                case BrowserName.Chrome:
                    Process.Start(Path.GetFullPath(_uploaderExecutablePath), "aaaa" + Path.GetFullPath(fileToUploadPath));
                    break;
                default:
                    Process.Start(Path.GetFullPath(_uploaderExecutablePath), Path.GetFullPath(fileToUploadPath));
                    break;
            }  
            
        }
    }
}