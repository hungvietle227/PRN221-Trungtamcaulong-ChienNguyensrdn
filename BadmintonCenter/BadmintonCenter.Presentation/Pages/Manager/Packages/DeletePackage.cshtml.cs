using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCenter.Presentation.Pages.Manager.Packages
{
    public class DeletePackageModel : PageModel
    {
        private readonly IPackageService _packageService;
        public DeletePackageModel(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [BindProperty]
        public Package PackageInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var packageinformation = await _packageService.GetPackageById(id);

            if (packageinformation == null)
            {
                return NotFound();
            }
            else
            {
                PackageInformation = packageinformation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var packageinformation = await _packageService.GetPackageById(id);

            if (packageinformation != null)
            {
                PackageInformation = packageinformation;
                await _packageService.DeletePackageAsync(packageinformation);
                return RedirectToPage("/Manager/ViewPackage");
            }
            return NotFound();
        }
    }
}
