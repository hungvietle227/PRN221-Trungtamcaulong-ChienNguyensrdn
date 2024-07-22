using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BadmintonCenter.Service.Interface;
using System.Security.Claims;

namespace BadmintonCenter.Presentation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAuthService _authService;

        public IndexModel(ILogger<IndexModel> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public IActionResult OnGet()
        {
            var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            if (role == null)
            {
                return Redirect("/Admin/Index");
            }
            else if (role == "Manager")
            {
                return Redirect("/Manager/Index");
            }
            else if(role == "Admin")
            {
                return Redirect("/");
            }
            return Page();
        }
    }
}
