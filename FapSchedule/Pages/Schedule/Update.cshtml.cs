using FapSchedule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FapSchedule.Pages.Schedule
{
    public class UpdateModel : PageModel
    {
        private FapScheduleContext _context;
        public UpdateModel(FapScheduleContext context)
        {
            _context = context;

        }
        public List<Lecturer> lecturers { get; set; }
        public List<Room> rooms { get; set; }
        

        public void OnGet()
        {
            rooms=_context.Rooms.ToList();
            lecturers=_context.Lecturers.ToList();




        }
    }
}
