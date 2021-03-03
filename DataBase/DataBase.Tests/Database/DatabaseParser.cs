using System;
using DataBase.Tests.Models;
using MySql.Data.MySqlClient;

namespace DataBase.Tests.Database
{
    public class DatabaseParser
    {
        public static TestWithProjectAndMinTime ParseTestWithProjectAndMinTime(MySqlDataReader reader)
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