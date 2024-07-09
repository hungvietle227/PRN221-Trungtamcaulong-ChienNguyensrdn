using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.User;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BadmintonCenter.Presentation.Pages.Admin.Users
{
    public class EditUserModel : PageModel
    {
        private readonly IUserService _userService;
        public EditUserModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public User UserDTO { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userInformation = await _userService.GetUserById(id);
            if (userInformation == null)
            {
                return NotFound();
            }
            UserDTO = userInformation;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUserAsync(UserDTO);
                return RedirectToPage("/Admin/ViewUser");
            }
            return Page();
        }
    }
}
