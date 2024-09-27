-- Drop tables if they exist
DROP TABLE IF EXISTS Department CASCADE;
DROP TABLE IF EXISTS Subject CASCADE;

-- Create Department Table
CREATE TABLE Department
(
	Id UUID PRIMARY KEY,
	Name VARCHAR(100) NOT NULL,
	NumberOfStudents INT
);

-- Create Subject Table
CREATE TABLE Subject
(
	Id UUID PRIMARY KEY,
	DepartmentId UUID REFERENCES Department(Id) NOT NULL,
	Name VARCHAR(100) NOT NULL,
	EctsPoints INT NOT NULL,
	TimeCreated TIMESTAMP NOT NULL
);

-- Insert Departments
INSERT INTO Department (Id, Name, NumberOfStudents)
VALUES 
    (gen_random_uuid(), 'Mathematics', 200),
    (gen_random_uuid(), 'Physics', 150),	
    (gen_random_uuid(), 'Chemistry', 180),
    (gen_random_uuid(), 'Computer Science', 220),
    (gen_random_uuid(), 'Biology', 160),	
    (gen_random_uuid(), 'Mechanical Engineering', 140),	
    (gen_random_uuid(), 'Economics', 210),
    (gen_random_uuid(), 'History', 110),
    (gen_random_uuid(), 'Philosophy', 90);

-- Insert Subjects
INSERT INTO Subject (Id, DepartmentId, Name, EctsPoints, TimeCreated)
VALUES 
    -- Mathematics
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Mathematics'), 'Calculus I', 6, CURRENT_DATE - INTERVAL '1 day'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Mathematics'), 'Algebra', 5, CURRENT_DATE - INTERVAL '2 day'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Mathematics'), 'Geometry', 4, CURRENT_DATE - INTERVAL '3 day'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Mathematics'), 'Probability', 6, CURRENT_DATE - INTERVAL '4 day'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Mathematics'), 'Linear Algebra', 5, CURRENT_DATE - INTERVAL '5 day'),

    -- Physics
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Physics'), 'Quantum Mechanics', 7, CURRENT_DATE - INTERVAL '25 hour'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Physics'), 'Classical Mechanics', 6, CURRENT_DATE - INTERVAL '2 day'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Physics'), 'Electromagnetism', 6, CURRENT_DATE - INTERVAL '3 day'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Physics'), 'Thermodynamics', 5, CURRENT_DATE - INTERVAL '4 day'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Physics'), 'Optics', 5, CURRENT_DATE - INTERVAL '5 day'),

    -- Chemistry
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Chemistry'), 'Organic Chemistry', 4, CURRENT_DATE + INTERVAL '1 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Chemistry'), 'Inorganic Chemistry', 5, CURRENT_DATE + INTERVAL '2 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Chemistry'), 'Physical Chemistry', 6, CURRENT_DATE + INTERVAL '3 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Chemistry'), 'Analytical Chemistry', 6, CURRENT_DATE + INTERVAL '4 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Chemistry'), 'Biochemistry', 7, CURRENT_DATE + INTERVAL '5 month'),

    -- Computer Science
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Computer Science'), 'Data Structures', 8, CURRENT_DATE + INTERVAL '2 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Computer Science'), 'Algorithms', 6, CURRENT_DATE + INTERVAL '3 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Computer Science'), 'Operating Systems', 7, CURRENT_DATE + INTERVAL '4 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Computer Science'), 'Database Systems', 7, CURRENT_DATE + INTERVAL '5 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Computer Science'), 'Artificial Intelligence', 8, CURRENT_DATE + INTERVAL '6 month'),

    -- Biology
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Biology'), 'Cell Biology', 5, CURRENT_DATE + INTERVAL '1 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Biology'), 'Genetics', 6, CURRENT_DATE + INTERVAL '2 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Biology'), 'Microbiology', 5, CURRENT_DATE + INTERVAL '3 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Biology'), 'Ecology', 6, CURRENT_DATE + INTERVAL '4 month'),
    (gen_random_uuid(), (SELECT Id FROM Department WHERE Name = 'Biology'), 'Evolutionary Biology', 7, CURRENT_DATE + INTERVAL '5 month');
