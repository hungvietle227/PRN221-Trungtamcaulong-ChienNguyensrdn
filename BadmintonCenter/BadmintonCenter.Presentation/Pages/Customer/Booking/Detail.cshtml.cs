using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.Service;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BadmintonCenter.Presentation.Pages.Booking
{
    [Authorize]
    [BindProperties]
    public class DetailModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;

        public DetailModel(IBookingService bookingService, IUserService userService)
        {
            _bookingService = bookingService;
            _userService = userService;

        }

        public BadmintonCenter.BusinessObject.Models.Booking? Booking { get; set; }
        public IEnumerable<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
        public User? Customer { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToPage("/Auth/Login");
            }

            if (id != null)
            {
                Booking = await _bookingService.GetBookingById((int)id);
                Customer = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)!);

                if (Booking != null)
                {
                    BookingDetails = await _bookingService.GetBookingDetailsByBookingId((int)id);
                    return Page();
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostCancel(int id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking != null)
            {
                booking.Status = BookingStatus.Cancel;
                await _bookingService.UpdateBooking(booking);

                return RedirectToPage("/Customer/Booking/ByDay");
            }
            return Page();
        }
    }
}
