using BadmintonCenter.Common.Constant.Email;
using BadmintonCenter.Common.DTO.User;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.Common.Enum.User;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BadmintonCenter.Presentation.Pages.Admin.Users
{
    public class AddUserModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        public AddUserModel(IAuthService authService, IEmailService emailService, IUserService userService) 
        {
            _authService = authService;
            _emailService = emailService;
            _userService = userService;
        }
        [BindProperty]
        public CreateUserDTO UserDTO { get; set; } = null!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userService.GetUserByUserName(UserDTO.Username);
            if(user != null)
            {
                ModelState.AddModelError("UserDTO.Username", "This username is existed!");
            }

            if (!ModelState.IsValid)
            {
                return OnGet();
            }

            await _authService.CreateManager(UserDTO);
            _emailService.SendInfomationModeratorEmail(UserDTO.Email, Subject.Subjectmail, UserDTO);

            return Redirect("/Admin/ViewUser");
        }

        public static IEnumerable<SelectListItem> SelectStatus()
        {
            return new[]
            {
                new SelectListItem {Text = "Manager", Value = UserRole.Manager.ToString()},
                new SelectListItem {Text = "Staff", Value = UserRole.Staff.ToString()},
            };
        }
    }
}
