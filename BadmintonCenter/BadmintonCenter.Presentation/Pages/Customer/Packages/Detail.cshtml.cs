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

        [BindProperty]
        public Package Package { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var packages = await _packageService.GetPackageById(id);
            if(packages == null)
            {
                return NotFound();
            }
            Package = packages;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email));
                var package = await _userpackageService.GetUserPackagesByUserIdAndPackageId(user.UserId, Package.PackageId);

                if (package != null)
                {
                    await _userpackageService.UpdateUserPackageAsync(package);
                }
                else
                {
                    await _userpackageService.AddUserPackageAsync(new UserPackage
                    {
                        PackageId = Package.PackageId,
                        UserId = user.UserId,
                        HourRemaining = Package.HourIncluded,
                        ValidInMonth = DateTime.Now.Month,
                    });
                }

                
                
                
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
