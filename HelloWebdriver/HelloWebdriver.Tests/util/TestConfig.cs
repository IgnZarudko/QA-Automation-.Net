using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HelloWebdriver.Tests
{
    public class TestConfig
    {
        public string Browser { get; set; }
        public string StartUrl { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        
        public string Username { get; set; }
        
        public string UserEmail { get; set; }
        
        public string CsvFilePath { get; set; }

    }
}