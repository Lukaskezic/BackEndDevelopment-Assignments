using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Assignment3.Data;
using Assignment3.Models;
using Assignment3.Hub;

namespace Assignment3.Pages.Reception
{
    [Authorize("Reception")]
    public class ExpectedGuestsModel : PageModel
    {
        private ApplicationDbContext _context;
        private readonly IHubContext<KitchenReport, IKitchenReport> _kitchenReport;
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = DateTime.Today;
            public int Children { get; set; } = 0;
            public int Adults { get; set; } = 0;
        }
        public ExpectedGuestsModel(ApplicationDbContext context, IHubContext<KitchenReport, IKitchenReport> kitchenReport)
        {
            _context = context;
            Input = new InputModel();
            _kitchenReport = kitchenReport;
        }
        public IActionResult OnPostRedirect()
        {
            return RedirectToPage("BreakfastData");
        }
        public async Task<IActionResult> OnPostAsync()
        {
           var Expected = new Expected
           {
                Adults = Input.Adults,
                Children = Input.Children,
                Date = Input.Date
           };
            _context.BreakfastGuestsExpecteds.Add(Expected);
            await _context.SaveChangesAsync();
            _kitchenReport.Clients.All.KitchenUpdate();
            return Page();
        }
        public void OnGet() {}
    }
}