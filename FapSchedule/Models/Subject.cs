using System;
using System.Collections.Generic;

namespace FapSchedule.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Classes = new HashSet<Class>();
        }

        public int SubjectId { get; set; }
        public string SubjectCode { get; set; } = null!;
        public string SubjectName { get; set; } = null!;

        public virtual ICollection<Class> Classes { get; set; }
    }
}
