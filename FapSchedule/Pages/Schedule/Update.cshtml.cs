using FapSchedule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
                    message = "Updated failed";
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
        public string errorMessageAddedFailed(Class c)
        {
            string message = "";
            Class checkTimeSlotAndRoom = _context.Classes.Include(c => c.Room).Include(c => c.FirstSlotNavigation).Include(c => c.SecondSlotNavigation).FirstOrDefault(cl => cl.RoomId == c.RoomId && (cl.FirstSlot == c.FirstSlot || cl.SecondSlot == c.SecondSlot));
            if (checkTimeSlotAndRoom != null)
            {
                message = "There is a available class at the room " + c.Room.RoomName + " at slot " + c.FirstSlotNavigation.TimeSlotNo + " - " + c.FirstSlotNavigation.WeekDay + " and " + c.FirstSlotNavigation.TimeSlotNo + " - " + c.FirstSlotNavigation.WeekDay;
            }

            Class checkTimeSlotAndLecturer = _context.Classes.Include(c => c.Lecturer).Include(c => c.FirstSlotNavigation).Include(c => c.SecondSlotNavigation).FirstOrDefault(cl => cl.LecturerId == c.LecturerId && (cl.FirstSlot == c.FirstSlot || cl.SecondSlot == c.SecondSlot));
            if (checkTimeSlotAndLecturer != null)
            {
                message = "The lecturer " + c.Lecturer.LecturerCode + " has a class on " + c.FirstSlotNavigation.TimeSlotNo + " - " + c.FirstSlotNavigation.WeekDay + " and " + c.FirstSlotNavigation.TimeSlotNo + " - " + c.FirstSlotNavigation.WeekDay;
            }
            Class checkTimeSlotAndClassName = _context.Classes.Include(c => c.FirstSlotNavigation).Include(c => c.SecondSlotNavigation).FirstOrDefault(cl => cl.ClassName.Equals(c.ClassName) && (cl.FirstSlot == c.FirstSlot || cl.SecondSlot == c.SecondSlot));
            if (checkTimeSlotAndClassName != null)
            {
                message = "The class " + c.ClassName + " has a session on " + c.FirstSlotNavigation.TimeSlotNo + " - " + c.FirstSlotNavigation.WeekDay + " and " + c.FirstSlotNavigation.TimeSlotNo + " - " + c.FirstSlotNavigation.WeekDay;
            }
            Class checkSubjectAndClassName = _context.Classes.Include(c => c.Subject).FirstOrDefault(cl => cl.ClassName.Equals(c.ClassName) && (cl.SubjectId == c.SubjectId));
            if (checkSubjectAndClassName != null)
            {
                message = "The class " + c.ClassName + " has a session of the subject " + c.Subject.SubjectName + " in the database. ";
            }
            if ((c.FirstSlot == 3 && c.SecondSlot == 2) || (c.FirstSlot == 1 && c.SecondSlot == 4))
            {
                message = "The 2 slots must be both in the morning or in the afternoon.";
            }

            return message;



        }
    }
}
