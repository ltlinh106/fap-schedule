using System;
using System.Collections.Generic;

namespace FapSchedule.Models
{
    public partial class TimeSlot
    {
        public TimeSlot()
        {
            ClassFirstSlotNavigations = new HashSet<Class>();
            ClassSecondSlotNavigations = new HashSet<Class>();
        }

        public int TimeSlotId { get; set; }
        public int? TimeSlotNo { get; set; }
        public string? WeekDay { get; set; }

        public virtual ICollection<Class> ClassFirstSlotNavigations { get; set; }
        public virtual ICollection<Class> ClassSecondSlotNavigations { get; set; }
    }
}
