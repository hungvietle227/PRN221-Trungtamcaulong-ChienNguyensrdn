using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BadmintonCenter.Presentation.Pages.Manager
{
    public class AddCourtModel : PageModel
    {
        private readonly ICourtService _courtService;
        public AddCourtModel(ICourtService courtService) 
        {
            _courtService = courtService;
        }
        [BindProperty]
        public Court Court { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.ClearValidationState("BookingDetails");
            if (ModelState.IsValid)
            {
                await _courtService.AddCourtAsync(Court);
                return RedirectToPage("/Manager/Index"); 
            }
            return Page();
        }
        public static IEnumerable<SelectListItem> SelectStatus()
        {
            return new[]
            {
                new SelectListItem {Text = "Normal", Value = CourtStatus.Normal.ToString()},
                new SelectListItem {Text = "Fixing", Value = CourtStatus.Fixing.ToString()},
                new SelectListItem {Text = "Not Available", Value = CourtStatus.NotAvailable.ToString()}
            };
        }
    }
}
