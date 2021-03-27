using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DataBase.Tests.Config;
using DataBase.Tests.Database;
using log4net;
using log4net.Config;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using DataBase.Tests.Database;
using DataBase.Tests.Models;
using Newtonsoft.Json;

namespace DataBase.Tests
{
    public class DatabaseTests
    {
        private static string _configPath = "../../../Resources/databaseConfig.json";
        
        private static DatabaseConfig _config;
        
        private static DatabaseUtils _utils;
        
        [SetUp]
        public void Setup()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            
            string configString = File.ReadAllText(Path.GetFullPath(_configPath));
            _config = JsonConvert.DeserializeObject<DatabaseConfig>(configString);

            _utils = new DatabaseUtils();
            _utils.CreateConnectionFromConfig(_config);
            _utils.OpenConnection();
        }

        [Test]
        public void MinimalTimeTest()
        {
            MySqlDataReader reader = _utils.ExecuteQuery(DatabaseQueries.SELECT_TEST_WITH_MIN_TIME_AND_PROJECTS);

            List<TestWithProjectAndMinTime> testsList =
                DatabaseParser.ParseAllTestsWithProjectAndMinTime(reader, out var nullTests);
            
            LogManager.GetLogger(nameof(MinimalTimeTest)).Warn($"{nullTests} rows haven't been processed due to some fields were null");
            
            string jsonResult = JsonConvert.SerializeObject(testsList, Formatting.Indented);
            
            LogManager.GetLogger(nameof(MinimalTimeTest)).Info($"Got resulting JSON: {jsonResult}");
        }

        [Test]
        public void AllProjectsWithUniqueTestTest()
        {
            MySqlDataReader reader = _utils.ExecuteQuery(DatabaseQueries.SELECT_PROJECT_WITH_UNIQUE_TEST);
            
            List<ProjectAndTestsAmount> projectsList =
                DatabaseParser.ParseAllProjectsWithTestsAmount(reader);
            
            string jsonResult = JsonConvert.SerializeObject(projectsList, Formatting.Indented);
            
            LogManager.GetLogger(nameof(MinimalTimeTest)).Info($"Got resulting JSON: {jsonResult}");
        }

        [TestCase("2015-11-07")]
        public void AllTestsExecutedAfterDate(string date)
        {
            string sqlQuery =
                DatabaseQueries.SELECT_TEST_WITH_PROJECT_EXECUTED_AFTER_DATE.Replace("your_date", date);
            
            MySqlDataReader reader = _utils.ExecuteQuery(sqlQuery);

            List<TestWithProjectAndStartDate> testsList = DatabaseParser.ParseAllTestsWithProjectAndStartDate(reader);
            
            string jsonResult = JsonConvert.SerializeObject(testsList, Formatting.Indented);
            
            LogManager.GetLogger(nameof(MinimalTimeTest)).Info($"Got resulting JSON: {jsonResult}");
        }

        [Test]
        public void AmountOfTestsOnFirefoxAndChrome()
        {
            MySqlDataReader reader = _utils.ExecuteQuery(DatabaseQueries.SELECT_AMOUNT_OF_TESTS_ON_FIREFOX_AND_CHROME);

            List<BrowserWithTestsExecuted> browsersList = DatabaseParser.ParseAllBrowsersWithTestsExecuted(reader);
            
            string jsonResult = JsonConvert.SerializeObject(browsersList, Formatting.Indented);
            
            LogManager.GetLogger(nameof(MinimalTimeTest)).Info($"Got resulting JSON: {jsonResult}");
        }

        [TearDown]
        public void TearDown()
        {
            _utils.CloseConnection();
        }
    }
}