using Introduction.Model;

namespace Introduction.WebAPI.Rest
{
    public class DepartmentRest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Subject> Subjects {  get; set; }
    }
}
