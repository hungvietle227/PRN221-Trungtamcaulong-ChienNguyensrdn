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
        [BindProperty(SupportsGet = true)]
        public string? searchValue { get; set; }
        public List<Package> AllPackages { get; set; }
        public List<Package> Packages { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5; // Number of items per page

        public async Task<IActionResult> OnGetAsync(int? id, string? search)
        {
            search = searchValue ?? string.Empty;
            if (!string.IsNullOrEmpty(search))
            {
                AllPackages = await _packageService.GetPackageByCondition(search);
                CurrentPage = id ?? 1;
                Packages = PaginatedList<Package>.Create(AllPackages, CurrentPage, PageSize);
            }
            else
            {
                AllPackages = await _packageService.GetAllPackages();
                CurrentPage = id ?? 1;
                Packages = PaginatedList<Package>.Create(AllPackages, CurrentPage, PageSize);
            }
            return Page();
        }
    }
}
