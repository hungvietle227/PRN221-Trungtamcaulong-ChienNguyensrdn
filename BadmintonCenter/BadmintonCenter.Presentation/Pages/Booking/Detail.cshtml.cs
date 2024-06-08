using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCenter.Presentation.Pages.Booking
{
    [Authorize]
    public class DetailModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
