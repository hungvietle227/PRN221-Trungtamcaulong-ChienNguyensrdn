using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Auth;
using BadmintonCenter.Common.DTO.User;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCenter.Presentation.Pages.Admin.Users
{
    public class AddUserModel : PageModel
    {
        private readonly IAuthService _authService;
        public AddUserModel(IAuthService authService) 
        {
            _authService = authService;
        }
        [BindProperty]
        public CreateUserDTO UserDTO { get; set; } = null!;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _authService.CreateManager(UserDTO);
            return Redirect("/Admin/ViewUser");
        }
    }
}
