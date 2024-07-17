using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.Constant.Message;
using BadmintonCenter.Common.Constant.Payment;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Service;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BadmintonCenter.Presentation.Pages.Customer.Packages
{
    [Authorize(Roles = "Customer")]
    [BindProperties]
    public class DetailModel : PageModel
    {
        private readonly IPackageService _packageService;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;

        public DetailModel(IPackageService packageService,
                           IUserService userService,
                           IPaymentService paymentService)
        {
            _packageService = packageService;
            _userService = userService;
            _paymentService = paymentService;
        }

        public Package Package { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var packages = await _packageService.GetPackageById(id);
            if (packages == null)
            {
                return NotFound();
            }
            Package = packages;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = (await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)!))!;

            var newBookingCreate = new BookingCreateDTO()
            {
                TotalPrice = Package.Price,
                ItemType = ItemType.BUY_PACKAGE + $":{Package.PackageId}",
                TotalHours = Package.HourIncluded,
                UserId = user.UserId,
            };

            var paymentUrl = await _paymentService.CreatePaymentRequest(PaymentMethodConst.VNPAY, newBookingCreate, HttpContext);

            if (!string.IsNullOrEmpty(paymentUrl))
            {
                return Redirect(paymentUrl);
            }

            return Page();
        }
    }
}
