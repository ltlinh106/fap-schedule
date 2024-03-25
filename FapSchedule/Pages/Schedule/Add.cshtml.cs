using FapSchedule.Models;
using FapSchedule.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FapSchedule.Pages.Schedule
{
    public class AddModel : PageModel
    {
        private FapScheduleContext _context;
        private ICSVService _csvService;

        public AddModel(FapScheduleContext context, ICSVService cSVService)
        {
            _context = context;
            _csvService = cSVService;

        }
        public List<Lecturer> lecturers { get; set; }
        public List<Room> rooms { get; set; }
        public List<Subject> subjects { get; set; }
        public List<TimeSlot> firstSlot { get; set; }
        public List<TimeSlot> secondSlot { get; set; }

        public string message { get; set; }

        public void OnGet()
        {
            rooms = _context.Rooms.ToList();
            lecturers = _context.Lecturers.ToList();
            subjects = _context.Subjects.ToList();
            firstSlot = _context.TimeSlots.Where(x => x.TimeSlotNo == 1 || x.TimeSlotNo == 3).ToList();
            secondSlot = _context.TimeSlots.Where(x => x.TimeSlotNo == 2 || x.TimeSlotNo == 4).ToList();
            message = "";
        }
        public void OnPost(Class c)
        {
            rooms = _context.Rooms.ToList();
            lecturers = _context.Lecturers.ToList();
            subjects = _context.Subjects.ToList();
            firstSlot = _context.TimeSlots.Where(x => x.TimeSlotNo == 1 || x.TimeSlotNo == 3).ToList();
            secondSlot = _context.TimeSlots.Where(x => x.TimeSlotNo == 2 || x.TimeSlotNo == 4).ToList();
            try
            {
                if (checkBeforeCreateAndUpdate(c))
                {


                    _context.Classes.Add(c);
                    _context.SaveChanges();
                    message = "suscess";
                }
                else
                {
                    message = "cannot added!";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
        }

        public void OnPostImportFromFile([FromForm] IFormFileCollection file)
        {
            rooms = _context.Rooms.ToList();
            lecturers = _context.Lecturers.ToList();
            subjects = _context.Subjects.ToList();
            firstSlot = _context.TimeSlots.Where(x => x.TimeSlotNo == 1 || x.TimeSlotNo == 3).ToList();
            secondSlot = _context.TimeSlots.Where(x => x.TimeSlotNo == 2 || x.TimeSlotNo == 4).ToList();
            var schedules = _csvService.ReadCSV<FapSchedule.Models.Schedule>(file[0].OpenReadStream()).ToList();

            foreach (var s in schedules)

            {
                Room r = _context.Rooms.FirstOrDefault(r => r.RoomName.Equals(s.Room));
                Lecturer l = _context.Lecturers.FirstOrDefault(l => l.LecturerCode.Equals(s.LecturerCode));
                Subject su = _context.Subjects.FirstOrDefault(sub => sub.SubjectCode.Equals(s.SubjectCode));
                TimeSlot fl = new TimeSlot();
                TimeSlot sl = new TimeSlot();
                char[] tl = s.TimeSlot.ToCharArray();
                if (s.TimeSlot.StartsWith("A"))
                {
                    switch (tl[1])
                    {
                        case '2':
                            fl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 1 && t.WeekDay.Equals("Mon"));
                            break;
                        case '3':
                            fl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 1 && t.WeekDay.Equals("Tue"));
                            break;
                        case '4':
                            fl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 1 && t.WeekDay.Equals("Wed"));
                            break;
                        case '5':
                            fl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 1 && t.WeekDay.Equals("Thu"));
                            break;
                        case '6':
                            fl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 1 && t.WeekDay.Equals("Fri"));
                            break;
                        case '7':
                            fl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 1 && t.WeekDay.Equals("Sat"));
                            break;

                    }
                    switch (tl[2])
                    {
                        case '2':
                            sl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 2 && t.WeekDay.Equals("Mon"));
                            break;
                        case '3':
                            sl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 2 && t.WeekDay.Equals("Tue"));
                            break;
                        case '4':
                            sl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 2 && t.WeekDay.Equals("Wed"));
                            break;
                        case '5':
                            sl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 2 && t.WeekDay.Equals("Thu"));
                            break;
                        case '6':
                            sl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 2 && t.WeekDay.Equals("Fri"));
                            break;
                        case '7':
                            sl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 2 && t.WeekDay.Equals("Sat"));
                            break;

                    }


                }

                else

                {
                    switch (tl[1])
                    {
                        case '2':
                            fl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 3 && t.WeekDay.Equals("Mon"));
                            break;
                        case '3':
                            fl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 3 && t.WeekDay.Equals("Tue"));
                            break;
                        case '4':
                            fl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 3 && t.WeekDay.Equals("Wed"));
                            break;
                        case '5':
                            fl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 3 && t.WeekDay.Equals("Thu"));
                            break;
                        case '6':
                            fl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 3 && t.WeekDay.Equals("Fri"));
                            break;
                        case '7':
                            fl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 3 && t.WeekDay.Equals("Sat"));
                            break;

                    }
                    switch (tl[2])
                    {
                        case '2':
                            sl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 4 && t.WeekDay.Equals("Mon"));
                            break;
                        case '3':
                            sl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 4 && t.WeekDay.Equals("Tue"));
                            break;
                        case '4':
                            sl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 4 && t.WeekDay.Equals("Wed"));
                            break;
                        case '5':
                            sl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 4 && t.WeekDay.Equals("Thu"));
                            break;
                        case '6':
                            sl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 4 && t.WeekDay.Equals("Fri"));
                            break;
                        case '7':
                            sl = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotNo == 4 && t.WeekDay.Equals("Sat"));
                            break;

                    }

                }
                firstSlot = _context.TimeSlots.Where(x => x.TimeSlotNo == 1 || x.TimeSlotNo == 3).ToList();
                secondSlot = _context.TimeSlots.Where(x => x.TimeSlotNo == 2 || x.TimeSlotNo == 4).ToList();
                Class c = new Class();
                message +=s.Room;


                try
                {

                    c.ClassName = s.ClassName;
                    c.RoomId = r.RoomId;
                    c.SubjectId = su.SubjectId;
                    c.LecturerId = l.LecturerId;
                    c.FirstSlot = fl.TimeSlotId;
                    c.SecondSlot = sl.TimeSlotId;
                    if (checkBeforeCreateAndUpdate(c))
                    {
                        _context.Classes.Add(c);
                        _context.SaveChanges();
                        message = "suscess";
                    }
                }
                catch (Exception ex)
                {
                    message = c.ClassName + " " + c.RoomId + "  " + c.LecturerId + " " + c.SubjectId + c.FirstSlot + "  " + c.SecondSlot + tl[0] + " " + tl[1] + " " + tl[2];
                }
            }

        }
        public bool checkBeforeCreateAndUpdate(Class c)
        {
            bool isValid = true;
            Class checkTimeSlotAndRoom = _context.Classes.FirstOrDefault(cl => cl.RoomId == c.RoomId && (cl.FirstSlot == c.FirstSlot || cl.SecondSlot == c.SecondSlot));

            Class checkTimeSlotAndLecturer = _context.Classes.FirstOrDefault(cl => cl.LecturerId == c.LecturerId && (cl.FirstSlot == c.FirstSlot || cl.SecondSlot == c.SecondSlot));
            Class checkTimeSlotAndClassName = _context.Classes.FirstOrDefault(cl => cl.ClassName.Equals(c.ClassName) && (cl.FirstSlot == c.FirstSlot || cl.SecondSlot == c.SecondSlot));
            Class checkSubjectAndClassName = _context.Classes.FirstOrDefault(cl => cl.ClassName.Equals(c.ClassName) && (cl.SubjectId == c.SubjectId));
            if ((c.FirstSlot == 3 && c.SecondSlot == 2) || (c.FirstSlot == 1 && c.SecondSlot == 4))
            {
                return false;
            }
            if ((checkTimeSlotAndLecturer == null) && (checkTimeSlotAndRoom == null) && (checkTimeSlotAndClassName == null) && (checkSubjectAndClassName == null))
                {
                return true;
            }
            return false;



        }

    }
}


