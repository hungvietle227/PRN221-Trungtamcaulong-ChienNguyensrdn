using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BadmintonCenter.Presentation.Pages.Customer.Packages
{
    [Authorize(Roles = "Customer")]
    public class IndexModel : PageModel
    {
        private readonly IPackageService _packageService;
        private readonly IUserService _userService;


        public IndexModel(IPackageService packageService, IUserService userService) { 
            _packageService = packageService;
            _userService = userService;
        }

        public ICollection<Package> Packages { get; set; } = null!;
        public UserPackage UserPackage { get; set; } = null!;
        public User? Users { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Packages = await _packageService.GetAllPackages();
            Users = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)!);

            return Page();
        }
    }
}
