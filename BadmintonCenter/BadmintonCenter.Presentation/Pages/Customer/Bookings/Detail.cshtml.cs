using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.Constant.Payment;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.Service.Interface;
using DemoSchedule.DTO;
using DemoSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BadmintonCenter.Presentation.Pages.Bookings
{
    [Authorize(Roles = "Customer")]
    [BindProperties]
    public class DetailModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;
        private readonly ICommonService _commonService;

        public DetailModel(IBookingService bookingService, 
                           IUserService userService,
                           IPaymentService paymentService,
                           ICommonService commonService)
        {
            _bookingService = bookingService;
            _userService = userService;
            _paymentService = paymentService;
            _commonService = commonService;
        }

        public Booking Booking { get; set; }
        public IEnumerable<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
        public User Customer { get; set; }
        public IEnumerable<UserPackage> Packages { get; set; } = new List<UserPackage>();
        public string PaymentMethod { get; set; } = PaymentMethodConst.VNPAY;
        

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
                Packages = await _commonService.GetTimeRemainingOfUser(Customer!.UserId);

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

                if (booking.DateOfWeek == null)
                {
                    return RedirectToPage("/Customer/Booking/ByDay");
                } else
                {
                    return RedirectToPage("/Customer/Booking/Stable");
                }

            }
            return Page();
        }

        public async Task<IActionResult> OnPostPaymentAsync()
        {
            Booking = await _bookingService.GetBookingById(Booking!.BookingId);
            Customer = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)!);
            Packages = await _commonService.GetTimeRemainingOfUser(Customer!.UserId);
            BookingDetails = await _bookingService.GetBookingDetailsByBookingId(Booking!.BookingId);
            var details = new List<BookingDetailDTO>();

            if (BookingDetails != null && BookingDetails.Any())
            {
                foreach(var bookingDetail in BookingDetails)
                {
                    details.Add(new BookingDetailDTO
                    {
                        CourtId = bookingDetail.CourtId,
                        SlotTimeId = bookingDetail.TimeSlotId
                    });
                }
            }

            var newBookingCreate = new BookingCreateDTO()
            {
                FullName = Customer.FullName,
                PhoneNumber = Customer.PhoneNumber,
                StartDate = Booking.BookingDate,
                TotalPrice = Booking.TotalPrice,
                Details = details,
                ItemType = ItemType.RENT_COURT
            };

            var paymentUrl = _paymentService.CreatePaymentRequest(PaymentMethod, newBookingCreate, HttpContext);

            if (!string.IsNullOrEmpty(paymentUrl))
            {
                return Redirect(paymentUrl);
            }

            return Page();

        }
    }
}
