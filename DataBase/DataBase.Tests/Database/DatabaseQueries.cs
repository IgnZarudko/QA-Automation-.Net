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

        public static string SELECT_PROJECT_WITH_UNIQUE_TEST = "SELECT p.name projectName " +
                                                               ", COUNT(DISTINCT test.name) testsAmount " +
                                                               "FROM test " +
                                                               "INNER JOIN project p on test.project_id = p.id " +
                                                               "GROUP BY projectName";

        public static string SELECT_TEST_WITH_PROJECT_EXECUTED_AFTER_DATE = "SELECT p.name projectName " +
                                                                            ", test.name testName " +
                                                                            ", test.start_time startDate " +
                                                                            "FROM test " +
                                                                            "INNER JOIN project p on test.project_id = p.id " +
                                                                            "WHERE DATEDIFF(test.end_time, DATE('your_date')) >= 0 " +
                                                                            "ORDER BY projectName, testName;";

    }
}