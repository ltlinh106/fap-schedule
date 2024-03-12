using System;
using System.Collections.Generic;

namespace FapSchedule.Models
{
    public partial class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;
        public int SubjectId { get; set; }
        public int LecturerId { get; set; }
        public int FirstSlot { get; set; }
        public int SecondSlot { get; set; }
        public int RoomId { get; set; }

        public virtual TimeSlot FirstSlotNavigation { get; set; } = null!;
        public virtual Lecturer Lecturer { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        public virtual TimeSlot SecondSlotNavigation { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
    }
}
