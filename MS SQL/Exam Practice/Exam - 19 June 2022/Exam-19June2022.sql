CREATE DATABASE Zoo
GO
USE Zoo
GO

--Section 1. DDL (30 pts)
--01. DDL
CREATE TABLE Owners
(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	PhoneNumber VARCHAR(15) NOT NULL,
	[Address] VARCHAR(50)
)

CREATE TABLE AnimalTypes
(
	Id INT PRIMARY KEY IDENTITY,
	AnimalType VARCHAR(30) NOT NULL
)

CREATE TABLE Cages
(
	Id INT PRIMARY KEY IDENTITY,
	AnimalTypeId INT FOREIGN KEY REFERENCES AnimalTypes(Id) NOT NULL
)

CREATE TABLE Animals
(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(30) NOT NULL,
	BirthDate DATE NOT NULL,
	OwnerId INT FOREIGN KEY REFERENCES Owners(Id),
	AnimalTypeId INT FOREIGN KEY REFERENCES AnimalTypes(Id) NOT NULL
)

CREATE TABLE AnimalsCages
(
	CageId INT FOREIGN KEY REFERENCES Cages(Id) NOT NULL,
	AnimalId INT FOREIGN KEY REFERENCES Animals(Id) NOT NULL,
	PRIMARY KEY(CageId, AnimalId)
)

CREATE TABLE VolunteersDepartments
(
	Id INT PRIMARY KEY IDENTITY,
	DepartmentName VARCHAR(30) NOT NULL
)

CREATE TABLE Volunteers
(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	PhoneNumber VARCHAR(15) NOT NULL,
	[Address] VARCHAR(50),
	AnimalId INT FOREIGN KEY REFERENCES Animals(Id),
	DepartmentId INT FOREIGN KEY REFERENCES VolunteersDepartments(Id) NOT NULL
)

--Section 2. DML (10 pts)
--02. Insert
INSERT INTO Animals([Name], BirthDate, OwnerId, AnimalTypeId)
	VALUES
('Giraffe', '2018-09-21', 21, 1),
('Harpy Eagle', '2015-04-17', 15, 3),
('Hamadryas Baboon', '2017-11-02', NULL, 1),
('Tuatara', '2021-06-30', 2, 4)

INSERT INTO Volunteers([Name], PhoneNumber, [Address], AnimalId, DepartmentId)
	VALUES
('Anita Kostova', '0896365412', 'Sofia, 5 Rosa str.', 15, 1),
('Dimitur Stoev', '0877564223', NULL, 42, 4),
('Kalina Evtimova', '0896321112', 'Silistra, 21 Breza str.', 9, 7),
('Stoyan Tomov', '0898564100', 'Montana, 1 Bor str.', 18, 8),
('Boryana Mileva', '0888112233', NULL, 31, 5)

--03. Update
SELECT * FROM Owners
WHERE Name = 'Kaloqn Stoqnov'
UPDATE Animals 
SET OwnerId = 4
WHERE OwnerId IS NULL

--04. Delete
SELECT * 
FROM VolunteersDepartments
WHERE DepartmentName = 'Education program assistant'

SELECT * 
FROM Volunteers 
WHERE DepartmentId = 2

DELETE FROM Volunteers
WHERE DepartmentId = 2

DELETE FROM VolunteersDepartments
WHERE DepartmentName = 'Education program assistant'


--Section 3. Querying (40 pts)
--05. Volunteers
SELECT [Name],
		PhoneNumber,
		[Address],
		AnimalId,
		DepartmentId
FROM Volunteers
ORDER BY [Name], AnimalId, DepartmentId

--06. Animals data
SELECT a.[Name],
		[at].AnimalType,
		FORMAT(a.BirthDate, 'dd.MM.yyyy') AS BirthDate
FROM Animals AS a
JOIN AnimalTypes AS [at]
ON a.AnimalTypeId = [at].Id
ORDER BY a.[Name]

--07. Owners and Their Animals
SELECT TOP(5)
		o.[Name] AS [Owner],
		COUNT(a.Id) AS CountOfAnimals
FROM Owners AS o
LEFT JOIN Animals AS a
ON o.Id = a.OwnerId
GROUP BY o.[Name]
ORDER BY CountOfAnimals DESC, o.[Name]

--08. Owners, Animals and Cages
SELECT CONCAT(o.[Name], '-', a.[Name]) AS OwnersAnimals,
		o.PhoneNumber,
		ac.CageId
FROM Owners AS o
JOIN Animals AS a
ON o.Id = a.OwnerId
JOIN AnimalTypes AS [at]
ON a.AnimalTypeId = [at].Id
JOIN AnimalsCages AS ac
ON ac.AnimalId = a.Id
WHERE at.AnimalType = 'Mammals'
ORDER BY o.[Name], a.[Name] DESC

--09. Volunteers in Sofia
SELECT v.[Name],
		v.PhoneNumber,
		LTRIM(REPLACE(REPLACE(v.[Address], 'Sofia', ''), ',', '')) AS [Address]
FROM Volunteers AS v
JOIN VolunteersDepartments AS vd
ON v.DepartmentId = vd.Id
WHERE vd.DepartmentName = 'Education program assistant' AND v.[Address] LIKE '%Sofia%'
ORDER BY v.[Name]

--10. Animals for Adoption
SELECT 	a.[Name],
		DATEPART(year, a.BirthDate) AS BirthYear,
		[at].AnimalType
FROM Animals AS a
JOIN AnimalTypes AS [at]
ON a.AnimalTypeId = [at].Id
WHERE OwnerId IS NULL
AND DATEDIFF(year, a.BirthDate, '01/01/2022') < 5
AND [at].AnimalType != 'Birds'
ORDER BY a.[Name]

--Section 4. Programmability (20 pts)
--11. All Volunteers in a Department
GO
CREATE FUNCTION udf_GetVolunteersCountFromADepartment (@VolunteersDepartment VARCHAR(30))
RETURNS INT 
AS
BEGIN
	RETURN
	(
		SELECT COUNT(*) 
		FROM Volunteers AS v
		JOIN VolunteersDepartments AS vd
		ON v.DepartmentId = vd.Id
		WHERE vd.DepartmentName = @VolunteersDepartment
	)
END
GO

SELECT dbo.udf_GetVolunteersCountFromADepartment ('Education program assistant')
SELECT dbo.udf_GetVolunteersCountFromADepartment ('Guest engagement')
SELECT dbo.udf_GetVolunteersCountFromADepartment ('Zoo events')

--12. Animals with Owner or Not
GO
CREATE PROCEDURE usp_AnimalsWithOwnersOrNot(@AnimalName VARCHAR(30))
AS
BEGIN 
	SELECT a.[Name],
			ISNULL(o.[Name], 'For adoption') AS OwnersName
	FROM Animals AS a
	LEFT JOIN Owners AS o
	ON a.OwnerId = o.Id
	WHERE a.[Name] = @AnimalName
END
GO

EXEC usp_AnimalsWithOwnersOrNot 'Pumpkinseed Sunfish'
EXEC usp_AnimalsWithOwnersOrNot 'Hippo'
EXEC usp_AnimalsWithOwnersOrNot 'Brown bear'