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

        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity?.Name != null)
            {
                return Redirect("/");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            returnUrl ??= Url.Content("~/");

            var user = await _authService.Login(LoginRequest.Username, LoginRequest.Password);

            if (user != null)
            {
                TempData["success"] = "Login successfully";
                return LocalRedirect(returnUrl);
            }
            else
            {
                TempData["error"] = "Invalid username or password";
                return Page();
            }

        }
    }
}
