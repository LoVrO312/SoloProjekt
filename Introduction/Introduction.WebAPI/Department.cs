namespace Introduction.WebAPI
{
    public class Department
    {
        private static int _IdCounter = 1;
        public int Id { get; set; }
        public int NumberOfStudents { get; set; }
        public string? Name { get; set; }
        public List<Subject> Subjects { get; set; }

        public Department(int numberOfStudents, string name)
        {
            Id = _IdCounter++;
            NumberOfStudents = numberOfStudents;
            Name = name;
            Subjects = new List<Subject>();
        }


        // create
        public bool AddSubject(Subject subject)
        {
            Subjects.Add(subject);
            return true;
        }

        // read
        public Subject? FindSubject(Guid id)
        {
            return Subjects.FirstOrDefault(x => x.Id == id);
        }

        // update
        public bool ChangeSubjectName(Guid id, string newName)
        {
            foreach(var subject in Subjects)
            {
                if(subject.Id == id)
                {
                    subject.Name = newName;
                    return true;
                }
            }
            return false;
        }

        // delete
        public bool RemoveSubject(Guid id)
        {
            Subject? currentSubject = Subjects.FirstOrDefault(x => x.Id == id);
            if (currentSubject != null)
            {
                Subjects.Remove(currentSubject);
                return true;
            }
            return false;
        }
    }
}
