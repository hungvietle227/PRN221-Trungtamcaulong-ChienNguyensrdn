using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BadmintonCenter.BusinessObject.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BadmintonCenter.Common.Enum.Status;
using Microsoft.EntityFrameworkCore;
using BadmintonCenter.Service.Interface;

namespace BadmintonCenter.Presentation.Pages.Staff
{
    public class CheckInUserModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public CheckInUserModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IEnumerable<BadmintonCenter.BusinessObject.Models.Booking> Bookings { get; set; } = default!;
        public IEnumerable<BookingDetail> BookingDetails { get; set; } = default!;

        public async Task<IActionResult> OnGet()
        {
            BookingDetails = await _bookingService.GetAllBookingDetails();
            return Page();
        }
    }
}
