using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Common
{
    public class SubjectFilter
    {
        public string SearchQuery{ get; set; }
        public Guid? DepartmentId { get; set; }
        public int? MinEctsPoints { get; set; }
        public int? MaxEctsPoints { get; set; }
        public DateTime? FromTimeCreated { get; set; }
        public DateTime? ToTimeCreated { get; set; }

        public SubjectFilter() { }
    }
}
