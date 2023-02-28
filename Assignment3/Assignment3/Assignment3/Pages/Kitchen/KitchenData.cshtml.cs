using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Assignment3.Data;
using Assignment3.Hub;

namespace Assignment3.Pages.Kitchen
{
    public class KitchenDataModel : PageModel
    {
        private readonly IHubContext<KitchenReport, IKitchenReport> _kitchenContext;
        private readonly ApplicationDbContext _context;
        public int _adultNumber;
        public int _childrenNumber;
        public int _totalNumber;
        public int _adultsCheckedIn;
        public int _childrenCheckedIn;
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = DateTime.Now;
        }
        public KitchenDataModel(ApplicationDbContext context,
            IHubContext<KitchenReport, IKitchenReport> kitchenContext)
        {
            _context = context;
            _kitchenContext = kitchenContext;
            Input = new InputModel();
        }
        public async Task OnGet()
        {
            var dbExpected = await _context.BreakfastGuestsExpecteds
                .Where(b => b.Date.Day == Input.Date.Day && b.Date.Month == Input.Date.Month)
                .ToListAsync();

            if (dbExpected == null)
            {
                ModelState.AddModelError("Input.Date", "No guest on this date");
                return;
            }

            foreach (var item in dbExpected)
            {
                _adultNumber = item.Adults;
                _childrenNumber = item.Children;
                _totalNumber = _adultNumber + _childrenNumber;
            }

            var dbCheckedIn = await _context.BreakfastCheckIns
               .Where(b => b.Date.Day == Input.Date.Day && b.Date.Month == Input.Date.Month)
               .ToListAsync();

            if (dbCheckedIn == null)
            {
                ModelState.AddModelError("Input.Date", "No guest on this date");
                return;
            }

            foreach (var item in dbCheckedIn)
            {
                _adultsCheckedIn += item.Adults;
                _childrenCheckedIn += item.Children;
            }
        }
        public async Task OnPost()
        {
            var dbExpected = await _context.BreakfastGuestsExpecteds
                .Where(b => b.Date.Day == Input.Date.Day && b.Date.Month == Input.Date.Month)
                .ToListAsync();
            
            if (dbExpected == null)
            {
                ModelState.AddModelError("Input.Date", "No guest on this date");
                return;
            }

            foreach (var item in dbExpected)
            {
                _adultNumber = item.Adults;
                _childrenNumber = item.Children;
                _totalNumber = _adultNumber + _childrenNumber;
            }

            var dbCheckedIn = await _context.BreakfastCheckIns
               .Where(b => b.Date.Day == Input.Date.Day && b.Date.Month == Input.Date.Month)
               .ToListAsync();

            if (dbCheckedIn == null)
            {
                ModelState.AddModelError("Input.Date", "No guest on this date");
                return;
            }

            foreach (var item in dbCheckedIn)
            {
                _adultsCheckedIn += item.Adults;
                _childrenCheckedIn += item.Children;
            }
        }
    }
}