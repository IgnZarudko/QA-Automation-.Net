namespace SteamTestFramework.Tests.Util.Config
{
    public class TestConfig
    {
        public string Browser { get; set; }
        public string StartUrl { get; set; }
        public string Language { get; set; }
        public string DownloadDirectory { get; set; }
        
        public int TimeForTimeoutSeconds { get; set; }
    }
}