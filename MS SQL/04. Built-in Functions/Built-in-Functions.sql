--Queries for SoftUni Database
--1 Find Names of All Employees by First Name
USE SoftUni

SELECT FirstName, LastName
FROM Employees
WHERE FirstName LIKE 'Sa%'

--2 Find Names of All Employees by Last Name
SELECT FirstName, LastName
FROM Employees
WHERE LastName LIKE '%ei%'

--3 Find First Names of All Employees
SELECT FirstName
FROM Employees
WHERE DepartmentID IN (3, 10)
	AND YEAR(HireDate) BETWEEN 1995 AND 2005

--4 Find All Employees Except Engineers
SELECT FirstName, LastName
FROM Employees
WHERE JobTitle NOT LIKE '%engineer%'

--Method 2
SELECT FirstName, LastName
FROM Employees
WHERE CHARINDEX('engineer', JobTitle) = 0

--5 Find Towns with Name Length
SELECT [Name] 
FROM Towns
WHERE LEN([Name]) BETWEEN 5 AND 6
ORDER BY [Name] ASC

--6 Find Towns Starting With
SELECT TownID, [Name] 
FROM Towns
WHERE LEFT([Name], 1) IN ('M', 'K', 'B', 'E')
ORDER BY [Name] ASC

--7 Find Towns Not Starting With
SELECT TownID, [Name] 
FROM Towns
WHERE LEFT([Name], 1) NOT IN ('R', 'B', 'D')
ORDER BY [Name] ASC

--METHOD 2
SELECT TownID, [Name] 
FROM Towns
WHERE [Name] NOT LIKE '[RBD]%'
ORDER BY [Name] ASC

--8 Create View Employees Hired After 2000 Year
GO
CREATE VIEW V_EmployeesHiredAfter2000 AS
SELECT FirstName, LastName 
FROM Employees
WHERE YEAR(HireDate)> 2000
GO

SELECT * FROM V_EmployeesHiredAfter2000
--9 Length of Last Name
SELECT FirstName, LastName 
FROM Employees
WHERE LEN(LastName) = 5

--10 Rank Employees by Salary
SELECT EmployeeID, FirstName, LastName, Salary,
	DENSE_RANK() OVER
	(PARTITION BY Salary ORDER BY EmployeeID) AS Rank
FROM Employees
WHERE Salary BETWEEN 10000 AND 50000
ORDER BY Salary DESC

--11 Find All Employees with Rank 2
SELECT * FROM 
(
	SELECT EmployeeID, FirstName, LastName, Salary,
		DENSE_RANK() OVER
		(PARTITION BY Salary ORDER BY EmployeeID) AS Rank
	FROM Employees
	WHERE Salary BETWEEN 10000 AND 50000
)	AS RankingSubquery
WHERE Rank = 2
ORDER BY Salary DESC

--12 Countries Holding 'A' 3 or More Times
USE Geography
SELECT CountryName, IsoCode 
FROM Countries
WHERE CountryName LIKE '%A%A%A%'
ORDER BY IsoCode

--13 Mix of Peak and River Names
SELECT p.PeakName,
		r.RiverName,
		LOWER(CONCAT(LEFT(p.PeakName, LEN(p.PeakName) - 1), r.RiverName)) AS Mix
FROM Peaks AS p, Rivers AS r
WHERE RIGHT(p.PeakName, 1) = LEFT(r.RiverName, 1)
ORDER BY Mix

USE Diablo
--14 Games From 2011 and 2012 Year
SELECT TOP(50) Name, FORMAT([Start], 'yyyy-MM-dd') AS [Start]
FROM GAMES
WHERE YEAR([Start]) IN (2011, 2012) 
ORDER BY Start, Name

--15 User Email Providers
SELECT Username,
		SUBSTRING(Email, CHARINDEX('@', Email) + 1, LEN(Email)) AS [Email Provider]
FROM Users
ORDER BY [Email Provider], Username

--16 Get Users with IP Address Like Pattern
SELECT Username, IpAddress
FROM Users
WHERE IpAddress LIKE '___.1%.%.___'
ORDER BY Username

--17 Show All Games with Duration & Part of the Day
SELECT [Name] AS Game,
		CASE
			WHEN DATEPART(HOUR, Start) BETWEEN 0 AND 11 THEN 'Morning'
			WHEN DATEPART(HOUR, Start) BETWEEN 12 AND 17 THEN 'Afternoon'
			ELSE 'Evening'
		END AS [Part of the Day],
		CASE
			WHEN Duration BETWEEN 0 AND 3 THEN 'Extra Short'
			WHEN Duration BETWEEN 4 AND 6 THEN 'Short'
			WHEN Duration > 6  THEN 'Long'
			ELSE 'Extra Long'
		END AS Duration
FROM Games
ORDER BY [Name], [Duration], [Part of the Day]

--18 Orders Table
USE Orders

SELECT ProductName, OrderDate,
		DATEADD(day, 3, OrderDate) AS [Pay Due],
		DATEADD(month, 1, OrderDate) AS [Deliver Due]
FROM Orders

--19 People Table
USE TableRelations

CREATE TABLE People(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	Birthdate DATETIME2 NOT NULL
)

INSERT INTO People([Name], Birthdate) 
	VALUES
('Victor', '2000-12-07 00:00:00.000'),
('Steven', '1992-09-10 00:00:00.000'),
('Stephen', '1910-09-19 00:00:00.000'),
('John', '2010-01-06 00:00:00.000')

SELECT [Name],
		DATEDIFF(year, Birthdate, GETDATE()) AS [Age in Years],
		DATEDIFF(month, Birthdate, GETDATE()) AS [Age in Months],
		DATEDIFF(day, Birthdate, GETDATE()) AS [Age in Days],
		DATEDIFF(minute, Birthdate, GETDATE()) AS [Age in Minutes]
FROM PEOPLE