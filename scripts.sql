# ��� ������� ����� ������� ����������� ����� ������.
# ����������� �� ��������, � �� ������ ������ ��������
SELECT p.name projectName
     , test.name testName
     , MIN((TO_SECONDS(test.end_time) - TO_SECONDS(test.start_time))) timeDiff
FROM test
INNER JOIN project p on test.project_id = p.id
GROUP BY testName, projectName
ORDER BY testName, projectName;

# ������� � ��� ��� ������� � ��������� ���������� ���������� ������ �� �������
SELECT p.name projectName
     , COUNT(DISTINCT test.name) testsAmount
FROM test
INNER JOIN project p on test.project_id = p.id
GROUP BY projectName;


# ������� ����� ��� ������� ��o����, ������� ����������� �� ������ 7 ������ 2015.
# ����������� �� ��������, � �� ������ ������ ��������
SELECT p.name projectName
     , test.name testName
     , test.start_time startDate
FROM test
INNER JOIN project p on test.project_id = p.id
WHERE DATEDIFF(test.end_time, DATE('2015-11-07')) >= 0
ORDER BY projectName, testName;

# ������� ���������� ������, ������������� �� Firefox � �� Chrome
SELECT browser, COUNT(id) amountOfTests
FROM test
WHERE browser = 'firefox'
UNION
SELECT browser, COUNT(id) amountOfTests
FROM test
WHERE browser = 'chrome'