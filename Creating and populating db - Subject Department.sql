DROP TABLE IF EXISTS Department CASCADE;
DROP TABLE IF EXISTS Subject CASCADE;

CREATE TABLE Department
(
	Id UUID PRIMARY KEY,
	Name VARCHAR(100) NOT NULL,
	NumberOfStudents INT
);

CREATE TABLE Subject
(
	Id UUID PRIMARY KEY,
	DepartmentId UUID REFERENCES Department(Id) NOT NULL,
	Name VARCHAR(100) NOT NULL,
	TimeCreated TIMESTAMP NOT NULL
);

INSERT INTO Department (Id, Name, NumberOfStudents)
VALUES 
    (gen_random_uuid(), 'Mathematics', 200),
    (gen_random_uuid(), 'Physics', 150),	
    (gen_random_uuid(), 'Chemistry', 180),
    (gen_random_uuid(), 'Computer Science', 220);

-- Insert into Subject table using a subquery to select DepartmentId
INSERT INTO Subject (Id, DepartmentId, Name, TimeCreated)
VALUES 
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Mathematics'), 'Calculus I', CURRENT_DATE),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Physics'), 'Quantum Mechanics', CURRENT_DATE),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Chemistry'), 'Organic Chemistry', CURRENT_DATE),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Computer Science'), 'Data Structures', CURRENT_DATE);


UPDATE subject SET name = 'matiša' WHERE id = '55c80f7c-c211-475d-b098-ba38e3698b6c';

SELECT * FROM subject;
SELECT * FROM department;