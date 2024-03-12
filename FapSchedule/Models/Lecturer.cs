using System;
using System.Collections.Generic;

namespace FapSchedule.Models
{
    public partial class Lecturer
    {
        public Lecturer()
        {
            Classes = new HashSet<Class>();
        }

        public int LecturerId { get; set; }
        public string LecturerCode { get; set; } = null!;
        public string LecturerName { get; set; } = null!;

        public virtual ICollection<Class> Classes { get; set; }
    }
}
