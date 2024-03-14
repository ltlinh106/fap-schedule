using FapSchedule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FapSchedule.Pages.Schedule
{
    public class IndexModel : PageModel
    {
        private FapScheduleContext _context;
        public IndexModel(FapScheduleContext context)
        {
            _context = context;

        }
        public List<Class> classes
         { get; set; }
    public List<Room> rooms { get; set; }
        // public string  { get; set; }
        public void OnGet()
        {
            classes=_context.Classes.Include(c=>c.Lecturer).
                Include(c=>c.Room).
                Include(c=>c.Subject).
                Include(c=>c.FirstSlotNavigation).
                Include(c=>c.SecondSlotNavigation).ToList();
            rooms=_context.Rooms.ToList();
        }
    }
}
