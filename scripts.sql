# ƒл€ каждого теста вывести минимальное врем€ работы.
# —ортировать по проектам, и по тестам внутри проектов
SELECT p.name projectName
     , test.name testName
     , MIN((TO_SECONDS(test.end_time) - TO_SECONDS(test.start_time))) timeDiff
FROM test
INNER JOIN project p on test.project_id = p.id
GROUP BY testName, projectName
ORDER BY testName, projectName;

# ¬ывести в лог все проекты с указанием количества уникальных тестов на проекте
SELECT p.name projectName
     , COUNT(DISTINCT test.name) testsAmount
FROM test
INNER JOIN project p on test.project_id = p.id
GROUP BY projectName;


# ¬ывести тесты дл€ каждого прoекта, которые выполн€лись не раньше 7 но€бр€ 2015.
# —ортировать по проектам, и по тестам внутри проектов
SELECT p.name projectName
     , test.name testName
     , test.start_time startDate
FROM test
INNER JOIN project p on test.project_id = p.id
WHERE DATEDIFF(test.end_time, DATE('2015-11-07')) >= 0
ORDER BY projectName, testName;

# ¬ывести количество тестов, выполн€вшихс€ на Firefox и на Chrome
SELECT browser, COUNT(id) amountOfTests
FROM test
WHERE browser = 'firefox'
UNION
SELECT browser, COUNT(id) amountOfTests
FROM test
WHERE browser = 'chrome'