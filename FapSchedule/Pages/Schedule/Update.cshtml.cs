using FapSchedule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FapSchedule.Pages.Schedule
{
    public class UpdateModel : PageModel
    {
        private FapScheduleContext _context;
        [BindProperty(SupportsGet = true)]
        public int ClassID { get; set; }
        public UpdateModel(FapScheduleContext context)
        {
            _context = context;

        }
        public List<Lecturer> lecturers { get; set; }
        public List<Room> rooms { get; set; }
        public List<Subject> subjects { get; set; }
        public List<TimeSlot> firstSlot { get; set; }
        public List<TimeSlot> secondSlot { get; set; }
        public Class updatedClass { get; set; }
        public string message { get; set; }



        public void OnGet()
        {
            rooms=_context.Rooms.ToList();
            lecturers=_context.Lecturers.ToList();
            subjects=_context.Subjects.ToList();
            firstSlot=_context.TimeSlots.Where(x=>x.TimeSlotNo==1||x.TimeSlotNo==3).ToList();
            secondSlot = _context.TimeSlots.Where(x => x.TimeSlotNo == 2 || x.TimeSlotNo == 4).ToList();
            updatedClass = _context.Classes.FirstOrDefault(c => c.ClassId == ClassID);
            message = "";






        }
        public void OnPost(Class newClass)
        {
            try
            {
                if (checkBeforeCreateAndUpdate(newClass))
                {
                    _context.Classes.Update(newClass);
                    _context.SaveChanges();
                    message = "success";
                }
                else
                {
                    message = "Cannot update!";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            rooms = _context.Rooms.ToList();
            lecturers = _context.Lecturers.ToList();
            subjects = _context.Subjects.ToList();
            firstSlot = _context.TimeSlots.Where(x => x.TimeSlotNo == 1 || x.TimeSlotNo == 3).ToList();
            secondSlot = _context.TimeSlots.Where(x => x.TimeSlotNo == 2 || x.TimeSlotNo == 4).ToList();
            updatedClass = _context.Classes.FirstOrDefault(c => c.ClassId == ClassID);
         






        }
        public bool checkBeforeCreateAndUpdate(Class c)
        {
            bool isValid= true;
            Class checkTimeSlotAndRoom=_context.Classes.FirstOrDefault(cl=> cl.RoomId ==c.RoomId && (cl.FirstSlot==c.FirstSlot||cl.SecondSlot==c.SecondSlot) );
            
            Class checkTimeSlotAndLecturer = _context.Classes.FirstOrDefault(cl => cl.LecturerId == c.LecturerId && (cl.FirstSlot == c.FirstSlot || cl.SecondSlot == c.SecondSlot));
            Class checkTimeSlotAndClassName = _context.Classes.FirstOrDefault(cl => cl.ClassName.Equals(c.ClassName) && (cl.FirstSlot == c.FirstSlot || cl.SecondSlot == c.SecondSlot));
            Class checkSubjectAndClassName = _context.Classes.FirstOrDefault(cl => cl.ClassName.Equals(c.ClassName) && (cl.SubjectId == c.SubjectId));
            if ((c.FirstSlot == 3 && c.SecondSlot == 2) || (c.FirstSlot == 1 && c.SecondSlot == 4))
            {
                return false;
            }
            if ((checkTimeSlotAndLecturer==null)&&(checkTimeSlotAndRoom==null)&&(checkTimeSlotAndClassName==null)&&(checkSubjectAndClassName==null))
                {
                return true;
            }
            return false;



        }
    }
}
