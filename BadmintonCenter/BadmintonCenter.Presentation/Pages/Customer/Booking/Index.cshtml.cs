using BadmintonCenter.Service;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BadmintonCenter.Presentation.Pages.Booking
{
    [Authorize(Roles = "Customer")]
    public class IndexModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;

        public IndexModel(IBookingService bookingService, IUserService userService)
        {
            _bookingService = bookingService;
            _userService = userService;
        }

        public BadmintonCenter.BusinessObject.Models.Booking? UnPaidBooking { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToPage("/Auth/Login");
            }
            
            var user = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)!);

            if (user != null)
            {
                UnPaidBooking = await _bookingService.GetUnPaidBookingByUserId(user.UserId);
            }

            return Page();
        }
    }
}
