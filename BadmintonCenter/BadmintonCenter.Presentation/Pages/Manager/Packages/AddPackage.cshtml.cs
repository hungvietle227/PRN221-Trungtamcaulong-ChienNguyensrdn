using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.Service;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BadmintonCenter.Presentation.Pages.Manager.Packages
{
    public class AddPackageModel : PageModel
    {
        private readonly IPackageService _packageService;
        public AddPackageModel(IPackageService packageService)
        {
            _packageService = packageService;
        }
        [BindProperty]
        public Package Package { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _packageService.AddPackageAsync(Package);
                return RedirectToPage("/Manager/ViewPackage");
            }
            return Page();
        }
        public static IEnumerable<SelectListItem> SelectHours()
        {
            var items = new[]
            {
        new SelectListItem {Text = "1 hour included", Value = "1"},
        new SelectListItem {Text = "2 hours included", Value = "2"},
        new SelectListItem {Text = "3 hours included", Value = "3"},
        new SelectListItem {Text = "4 hours included", Value = "4"},
        new SelectListItem {Text = "5 hours included", Value = "5"},
        new SelectListItem {Text = "6 hours included", Value = "6"},
        new SelectListItem {Text = "7 hours included", Value = "7"},
        new SelectListItem {Text = "8 hours included", Value = "8"},
        new SelectListItem {Text = "9 hours included", Value = "9"}
    };

            return items;
        }
    }
}
