using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCenter.Presentation.Pages.Manager
{
    public class ViewPackageModel : PageModel
    {
        private readonly IPackageService _packageService;

        public ViewPackageModel(IPackageService packageService)
        {
            _packageService = packageService;
        }

        public List<Package> AllPackages { get; set; }
        public List<Package> Packages { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5; // Number of items per page

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            AllPackages = await _packageService.GetAllPackages();
            CurrentPage = id ?? 1;
            // Paginate the list of rooms
            Packages = PaginatedList<Package>.Create(AllPackages, CurrentPage, PageSize);

            return Page();
        }
    }
}
