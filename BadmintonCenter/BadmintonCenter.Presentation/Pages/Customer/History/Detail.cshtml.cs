using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCenter.Presentation.Pages.Customer.History
{
    [Authorize(Roles = "Customer")]
    public class DetailModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public DetailModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public Booking Booking { get; set; }
        public IEnumerable<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

        public async Task<IActionResult> OnGet(int id)
        {
            var booking = await _bookingService.GetBookingById(id);

            if (booking == null)
            {
                return NotFound();
            }

            Booking = booking;

            var bookingDetails = await _bookingService.GetBookingDetailsByBookingId(id);

            if (bookingDetails != null && bookingDetails.Any())
            {
                BookingDetails = bookingDetails;
            }

            return Page();
        }
    }
}
