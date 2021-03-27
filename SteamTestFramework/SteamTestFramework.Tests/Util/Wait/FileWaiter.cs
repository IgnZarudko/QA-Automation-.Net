using System.IO;
using System.Threading;
using SteamTestFramework.Tests.Util.Config;

namespace SteamTestFramework.Tests.Util.Wait
{
    public static class FileWaiter
    {
        public static void WaitUntilFileDownloads(TestConfig config)
        {
            int timeIntervalsInMillis = config.TimeForTimeoutSeconds * 50;
            int summaryTime = 0;
            DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath(config.DownloadDirectory));
            while ((dir.GetFiles().Length == 0 || dir.GetFiles()[0].Extension != "exe") &&
                   summaryTime != config.TimeForTimeoutSeconds * 1000)
            {
                Thread.Sleep(timeIntervalsInMillis);
                summaryTime += timeIntervalsInMillis;
            }
        }
    }
}