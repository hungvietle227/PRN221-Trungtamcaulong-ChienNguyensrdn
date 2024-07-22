using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.Presentation.Pages.Admin;
using BadmintonCenter.Service;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BadmintonCenter.Presentation.Pages.Customer.History
{
    [Authorize(Roles = "Customer")]
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;

        public IndexModel(IBookingService bookingService, IUserService userService)
        {
            _bookingService = bookingService;
            _userService = userService;
        }

        public IEnumerable<Booking> AllBookings { get; set; } = new List<Booking>();
        public IEnumerable<Booking> Bookings { get; set; } = new List<Booking>();
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5; // Number of items per page
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string StatusSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentSort { get; set; }

        public async Task<IActionResult> OnGet(int? num, DateTime startDate, DateTime endDate, string sortOrder)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToPage("/Auth/Login");
            }

            var user = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)!);

            if (user != null)
            {
                CurrentSort = sortOrder;
                DateSort = sortOrder == "Date" ? "date_desc" : "Date";
                DateTime minDate = new DateTime(2000, 01, 01);
                if (startDate > minDate && endDate > minDate)
                {
                    StartTime = startDate;
                    EndTime = endDate;
                } else
                {
                    StartTime = DateTime.Now.Date;
                    EndTime = DateTime.Now.Date;
                }
                CurrentPage = num ?? 1;

                var bookings = await _bookingService.GetAllBookingOfUser(user.UserId);
                AllBookings = bookings.Where(p => p.ValidDate >= StartTime && p.ValidDate <= EndTime && p.Status != BookingStatus.Cancel);
                switch (sortOrder)
                {
                    case "Date":
                        AllBookings = AllBookings.OrderBy(s => s.BookingDate);
                        break;
                    case "date_desc":
                        AllBookings = AllBookings.OrderByDescending(s => s.BookingDate);
                        break;
                    default:
                        AllBookings = AllBookings.OrderBy(s => s.BookingId);
                        break;
                }
                Bookings = PaginatedList<Booking>.Create(AllBookings.OrderByDescending(p => p.BookingDate).ToList(), CurrentPage, PageSize);
            }

            return Page();
        }
    }
}
