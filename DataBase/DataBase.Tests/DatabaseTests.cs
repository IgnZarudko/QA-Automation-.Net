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

            List<TestWithProjectAndMinTime> testsList = new List<TestWithProjectAndMinTime>();
            int nullTests = 0;
            while (reader.Read())
            {
                try
                {
                    TestWithProjectAndMinTime test = DatabaseParser.ParseTestWithProjectAndMinTime(reader);

                    testsList.Add(test);
                }
                catch (InvalidOperationException)
                {
                    nullTests++;
                }
            }
            LogManager.GetLogger(nameof(MinimalTimeTest)).Warn($"{nullTests} rows haven't been processed due to some fields were null");
            
            string jsonResult = JsonConvert.SerializeObject(testsList, Formatting.Indented);
            
            LogManager.GetLogger(nameof(MinimalTimeTest)).Info($"Got resulting JSON: {jsonResult}");
        }

        [TearDown]
        public void TearDown()
        {
            _utils.CloseConnection();
        }
    }
}