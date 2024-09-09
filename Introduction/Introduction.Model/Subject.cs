namespace Introduction.Model
{
    public class Subject
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }  
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }

        public Subject() { }
    }
}