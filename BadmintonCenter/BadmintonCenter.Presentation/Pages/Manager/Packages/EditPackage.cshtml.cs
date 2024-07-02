using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCenter.Presentation.Pages.Manager.Packages
{
    public class EditPackageModel : PageModel
    {
        private readonly IPackageService _packageService;
        public EditPackageModel(IPackageService packageService)
        {
            _packageService = packageService;
        }
        [BindProperty]
        public Package Package { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var packageInformation = await _packageService.GetPackageById(id);
            ; if (packageInformation == null)
            {
                return NotFound();
            }
            Package = packageInformation;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _packageService.UpdatePackageAsync(Package);
                return RedirectToPage("/Manager/ViewPackage");
            }
            return Page();
        }
    }
}
