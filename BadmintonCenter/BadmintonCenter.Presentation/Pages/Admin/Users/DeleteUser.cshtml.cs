using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCenter.Presentation.Pages.Admin.Users
{
    public class DeleteUserModel : PageModel
    {
        private readonly IUserService _userService;
        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User UserInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userInfor = await _userService.GetUserById(id);

            if (userInfor == null)
            {
                return NotFound();
            }
            else
            {
                UserInformation = userInfor;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userinformation = await _userService.GetUserById(id);

            if (userinformation != null)
            {
                UserInformation = userinformation;
                await _userService.DeleteUserAsync(userinformation);
                return RedirectToPage("/Admin/ViewUser");
            }
            return NotFound();
        }
    }
}
