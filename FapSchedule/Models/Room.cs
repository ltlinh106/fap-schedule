using System;
using System.Collections.Generic;

namespace FapSchedule.Models
{
    public partial class Room
    {
        public Room()
        {
            Classes = new HashSet<Class>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;
        public int Capacity { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<Class> Classes { get; set; }
    }
}
