namespace DataBase.Tests.Database
{
    public class DatabaseQueries
    {
        public static string SELECT_TEST_WITH_MIN_TIME_AND_PROJECTS = "SELECT p.name projectName " +
                                                               ", test.name testName " +
                                                               ", MIN((TO_SECONDS(test.end_time) - TO_SECONDS(test.start_time))) timeDiff " +
                                                               "FROM test " +
                                                               "INNER JOIN project p on test.project_id = p.id " +
                                                               "GROUP BY testName, projectName " +
                                                               "ORDER BY testName, projectName";
        
        
    }
}