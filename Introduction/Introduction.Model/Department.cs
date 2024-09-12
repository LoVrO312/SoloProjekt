namespace Introduction.Model
{
    public class Department
    {
        public Guid Id { get; set; }
        public int NumberOfStudents { get; set; }
        public string? Name { get; set; }
        public List<Subject> Subjects { get; set; }

        public Department() { }
    }
}
