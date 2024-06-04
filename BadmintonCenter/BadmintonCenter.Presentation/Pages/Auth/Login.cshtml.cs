using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using BadmintonCenter.Service.Interface;
using BadmintonCenter.Common.DTO.Auth;
using BadmintonCenter.Common.Enum.User;
using BadmintonCenter.Common.Constant.Message;

namespace BadmintonCenter.Presentation.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public LoginModel(IAuthService authService,
                          IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;

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

            if (LoginRequest.Password.Length < 8)
            {
                ModelState.AddModelError("LoginRequest.Password", "Password must be at least 8 characters lon1g.");
                return Page();
            }

            // login with role customer
            var user = await _authService.Login(LoginRequest.Username, LoginRequest.Password);
            if (user != null)
            {
                //if found, create new cookie auth for user
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, LoginRequest.Username),
                    new(ClaimTypes.Role, Enum.GetName(typeof(UserRole), user.RoleId)!)
                };

                // claims identity
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //auth properties
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(double.Parse(_configuration["Cookie:ExpireTime"]!)),

                    IsPersistent = true,

                    IssuedUtc = DateTime.UtcNow,

                    RedirectUri = "/login"
                };

                //register cookie auth
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return Redirect("/");
            } else
            {
                Message = AuthMessage.LoginFailed;
            }
            return Page();
        }
    }
}
