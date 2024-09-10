namespace Introduction.Model
{
    public class Department
    {
        public Guid Id { get; set; }
        public int NumberOfStudents { get; set; }
        public string? Name { get; set; }
        public List<Subject> Subjects { get; set; }
        public Department() { }


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
