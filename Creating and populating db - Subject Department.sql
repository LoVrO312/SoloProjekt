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

-- Insert into Subject table using a subquery to select DepartmentId
INSERT INTO Subject (Id, DepartmentId, Name, EctsPoints, TimeCreated)
VALUES 
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Mathematics'), 'Calculus I', 6,CURRENT_DATE - INTERVAL '1 day'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Physics'), 'Quantum Mechanics', 7, CURRENT_DATE - INTERVAL '25 hour'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Chemistry'), 'Organic Chemistry', 4, CURRENT_DATE + INTERVAL '1 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Computer Science'), 'Data Structures', 8, CURRENT_DATE + INTERVAL '2 month');


SELECT * FROM subject;
SELECT * FROM department;

SELECT s.id as sid, s.name as sname, s.ectspoints as sectspoints, s.timecreated as stimecreated, d.id as did, d.name as dname
FROM subject s JOIN department d ON s.departmentid = d.id
WHERE d.id == '2af2f137-5428-4224-aae0-110f9abcef54';

WHERE s.timecreated >= '2024-09-11'::DATE