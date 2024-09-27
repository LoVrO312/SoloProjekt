using Introduction.Model;

namespace Introduction.WebAPI.Rest
{
    public class CreateSubjectRest
    {
        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public int EctsPoints { get; set; }
    }
}
