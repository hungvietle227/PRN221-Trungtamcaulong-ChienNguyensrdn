using BadmintonCenter.Common.Constant.Message;
using BadmintonCenter.Common.DTO.Auth;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCenter.Presentation.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public LoginDTO LoginRequest { get; set; } = null!;
        public string Message { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity?.Name != null)
            {
                return Redirect("/");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // login with role customer
            var user = await _authService.Login(LoginRequest.Username, LoginRequest.Password);

            if (user != null)
            {
                return Redirect("/");
            }
            else
            {
                Message = AuthMessage.LoginFailed;
                return Page();
            }

        }
    }
}
