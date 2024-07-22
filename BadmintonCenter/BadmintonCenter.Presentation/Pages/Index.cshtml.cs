using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BadmintonCenter.Service.Interface;
using System.Security.Claims;

namespace BadmintonCenter.Presentation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            if (role == null)
            {
                return RedirectToPage("/Auth/Login");
            }
            else if (role == "Manager")
            {
                return RedirectToPage("/Manager/Index");
            }
            else if(role == "Admin")
            {
                return RedirectToPage("/Admin/Index");
            } else if(role == "Staff")
            {
                return RedirectToPage("/Staff/Index");
            } else
            {
                return Page();
            }
        }
    }
}
