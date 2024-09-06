namespace Introduction.WebAPI
{
    public class Subject
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }  
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }

        public Subject() { }

        public Subject (string name, Guid departmentId, DateTime timeCreated)
        {
            Id = Guid.NewGuid();
            DepartmentId = departmentId;
            Name = name;
            TimeCreated = timeCreated;
            //TimeCreated = DateTime.Now;
        }
    }
}