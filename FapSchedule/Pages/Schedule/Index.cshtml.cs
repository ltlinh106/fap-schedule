using FapSchedule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FapSchedule.Pages.Schedule
{
    public class IndexModel : PageModel
    {
        private FapScheduleContext _context;
        [BindProperty]
        public int lecturerID { get; set; }
        [BindProperty]
        public string weekday  { get; set; }
        public IndexModel(FapScheduleContext context)
        {
            _context = context;

        }
        public List<Class> classes
         { get; set; }
    public List<Room> rooms { get; set; }
        public List<Lecturer> lecturers { get; set; }
        public string message { get; set; }

        public void OnGet()
        {
            weekday = "Mon";
            lecturerID = 0;
            message = "";
            GetData();
        }
        public void OnPostFilter()
        {
            GetData();
            message = "";
        }
        public void GetData()
        {
           
            classes = _context.Classes.Where(c=>((lecturerID==0||c.LecturerId==lecturerID)&&(c.FirstSlotNavigation.WeekDay.Equals(weekday)|| c.SecondSlotNavigation.WeekDay.Equals(weekday))))
            .Include(c => c.Lecturer).
                Include(c => c.Room).
                Include(c => c.Subject).
                Include(c => c.FirstSlotNavigation).
                Include(c => c.SecondSlotNavigation).ToList();
            rooms = _context.Rooms.ToList();
            lecturers=_context.Lecturers.ToList();

            
        }
        public void OnPostDelete(int classId)
        {
            Class c =  _context.Classes.FirstOrDefault(c => c.ClassId == classId);

            try
            {
                _context.Classes.Remove(c);
                _context.SaveChanges();
                message = "Delete successfully!";
            }
            catch (Exception ex)
            {
                message = "Delete failed!";
            }
            weekday = "Mon";
            lecturerID = 0;
            GetData();
           
        }
    }
}
