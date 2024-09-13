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
	EctsPoints INT NOT NULL,
	TimeCreated TIMESTAMP NOT NULL
);

INSERT INTO Department (Id, Name, NumberOfStudents)
VALUES 
    (gen_random_uuid(), 'Mathematics', 200),
    (gen_random_uuid(), 'Physics', 150),	
    (gen_random_uuid(), 'Chemistry', 180),
    (gen_random_uuid(), 'Computer Science', 220);

INSERT INTO Subject (Id, DepartmentId, Name, EctsPoints, TimeCreated)
VALUES 
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Mathematics'), 'Calculus I', 6,CURRENT_DATE - INTERVAL '1 day'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Physics'), 'Quantum Mechanics', 7, CURRENT_DATE - INTERVAL '25 hour'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Chemistry'), 'Organic Chemistry', 4, CURRENT_DATE + INTERVAL '1 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Computer Science'), 'Data Structures', 8, CURRENT_DATE + INTERVAL '2 month');