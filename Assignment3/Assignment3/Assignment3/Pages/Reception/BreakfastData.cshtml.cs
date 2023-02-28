using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Assignment3.Data;
using Assignment3.Models;

namespace Assignment3.Pages.Reception
{
    [Authorize("Reception")]
    public class BreakfastDataModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<CheckIn> CheckedIn { get; set; } = new List<CheckIn>();
        public DisplayModel Display { get; set; }
        public string DateNow { get; set; }
        public class DisplayModel
        {
            public int Children { get; set; } = 0;
            public int Adults { get; set; } = 0;
            public int RoomNumber { get; set; } = 0;
        }
        public BreakfastDataModel(ApplicationDbContext context)
        {
            _context = context;
            Display = new DisplayModel();
            DateNow = DateTime.Now.Day + "/" + DateTime.Now.Month;
        }
        public async Task OnGetAsync()
        {
            var dbBreakfastCheckIns = await _context.BreakfastCheckIns
                .Where(b => b.Date.Day == DateTime.Now.Day && b.Date.Month == DateTime.Now.Month)
                .ToListAsync();
            
            if (dbBreakfastCheckIns == null) { RedirectToPage("Error"); return; }
            CheckedIn = dbBreakfastCheckIns;
        }
    }
}