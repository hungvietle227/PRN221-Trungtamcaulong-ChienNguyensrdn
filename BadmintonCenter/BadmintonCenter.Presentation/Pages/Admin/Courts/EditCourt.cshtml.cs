using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BadmintonCenter.Presentation.Pages.Admin.Courts
{
    public class EditCourtModel : PageModel
    {
        private readonly ICourtService _courtService;
        public EditCourtModel(ICourtService courtService)
        {
            _courtService = courtService;
        }
        [BindProperty]
        public Court Court { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var courtInformation = await _courtService.GetCourtById(id);
            ; if (courtInformation == null)
            {
                return NotFound();
            }
            Court = courtInformation;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _courtService.UpdateCourtAsync(Court);
                return RedirectToPage("/Admin/ViewCourt");
            }
            return Page();
        }
    }
}
