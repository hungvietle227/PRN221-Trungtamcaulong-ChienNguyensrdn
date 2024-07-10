using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCenter.Presentation.Pages.Admin.Courts
{
    public class DeleteCourtModel : PageModel
    {
        private readonly ICourtService _courtService;
        public DeleteCourtModel(ICourtService courtService)
        {
            _courtService = courtService;
        }

        [BindProperty]
        public Court CourtInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var courtinformation = await _courtService.GetCourtById(id);

            if (courtinformation == null)
            {
                return NotFound();
            }
            else
            {
                CourtInformation = courtinformation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var courtinformation = await _courtService.GetCourtById(id);

            if (courtinformation != null)
            {
                CourtInformation = courtinformation;
                await _courtService.DeleteCourtAsync(courtinformation);
                return RedirectToPage("/Admin/ViewCourt");
            }
            return NotFound();
        }
    }
}
