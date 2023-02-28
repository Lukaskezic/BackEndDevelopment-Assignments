using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Assignment3.Data;
using Assignment3.Models;
using Assignment3.Hub;

namespace Assignment3.Pages.Waiter
{
    [Authorize("Waiter")]
    public class CheckinModel : PageModel
    {
        private ApplicationDbContext _context;
        private readonly IHubContext<KitchenReport, IKitchenReport> _kitchenReport;
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = DateTime.Today;
            public int Adults { get; set; } = 0;
            public int Children { get; set; } = 0;
            public int RoomNumber { get; set; }
        }
        public CheckinModel(ApplicationDbContext context, IHubContext<KitchenReport, IKitchenReport> kitchenReport)
        {
            _context = context;
            Input = new InputModel();
            _kitchenReport = kitchenReport;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var CheckIn = new CheckIn
            {
                Date = Input.Date,
                Adults = Input.Adults,
                Children = Input.Children,
                RoomNumber = Input.RoomNumber
            };
            _context.BreakfastCheckIns.Add(CheckIn);
            await _context.SaveChangesAsync();
            _kitchenReport.Clients.All.KitchenUpdate();
            return Page();
        }
        public void OnGet() { }
    }
}