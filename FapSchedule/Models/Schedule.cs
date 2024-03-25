namespace FapSchedule.Models
{
    public class Schedule
    {
      
        public string ClassName { get; set; } = null!;
        public string SubjectCode { get; set; }
        public string LecturerCode { get; set; }
        public string Room { get; set; }
        public string TimeSlot { get; set; }
       
    }
}
