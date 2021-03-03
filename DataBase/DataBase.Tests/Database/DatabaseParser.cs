using System;
using System.Collections.Generic;
using DataBase.Tests.Models;
using MySql.Data.MySqlClient;
using NUnit.Framework.Internal;

namespace DataBase.Tests.Database
{
    public class DatabaseParser
    {
        public static List<TestWithProjectAndMinTime> ParseAllTestsWithProjectAndMinTime(MySqlDataReader reader, out int nullTests)
        {
            List<TestWithProjectAndMinTime> testsList = new List<TestWithProjectAndMinTime>();
            int nullTestsLocal = 0;
            while (reader.Read())
            {
                try
                {
                    TestWithProjectAndMinTime test = ParseTestWithProjectAndMinTime(reader);

                    testsList.Add(test);
                }
                catch (InvalidOperationException)
                {
                    nullTestsLocal++;
                }
            }
            nullTests = nullTestsLocal;

            return testsList;
        }
        private static TestWithProjectAndMinTime ParseTestWithProjectAndMinTime(MySqlDataReader reader)
        {
            string projectName = reader[0].ToString();
            string testName = reader[1].ToString();

            string timeEstimatesString = reader[2].ToString();
            int timeEstimated;
            
            if (!string.IsNullOrEmpty(timeEstimatesString))
            {
                timeEstimated = int.Parse(timeEstimatesString);
            }
            else
            {
                throw new InvalidOperationException();
            }
            
            return new TestWithProjectAndMinTime(projectName, testName, timeEstimated);
        }
    }
}