using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BadmintonCenter.Presentation.Pages.Customer.Packages
{
    public class DetailModel : PageModel
    {
        private readonly IPackageService _packageService;
        private readonly IUserPackageService _userpackageService;
        private readonly IUserService _userService;
        public DetailModel(IPackageService packageService, IUserPackageService userpackageService, IUserService userService)
        {
            _packageService = packageService;
            _userpackageService = userpackageService;
            _userService = userService;
        }

        public Package? Package { get; set; }
        public User Users {  get; set; }

        public async Task OnGetAsync(int? id)
        {
            Package = await _packageService.GetPackageById((int)id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Users = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email));
                await _userpackageService.AddUserPackageAsync(new UserPackage
                {
                    PackageId = Package.PackageId,
                    UserId = Users.UserId,
                    HourRemaining = Package.HourIncluded,
                    ValidInMonth = DateTime.Now.Month,
                });
                return RedirectToPage("/Customer/Packages");
            }
            return Page();
        }
    }
}
