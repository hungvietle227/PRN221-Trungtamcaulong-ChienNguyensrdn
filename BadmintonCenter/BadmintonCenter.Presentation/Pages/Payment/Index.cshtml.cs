using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.Constant.Message;
using BadmintonCenter.Common.Constant.Payment;
using BadmintonCenter.Common.DTO.Payment;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BadmintonCenter.Presentation.Pages.Payment
{
    [Authorize(Roles = "Customer")]
    public class IndexModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;


        public IndexModel(IBookingService bookingService, 
                          IUserService userService, 
                          IPaymentService paymentService)
        {
            _bookingService = bookingService;
            _userService = userService;
            _paymentService = paymentService;
        }

        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;

        public async Task<IActionResult> OnGetAsync()
        {
            var requestQuery = HttpContext.Request.Query;

            if (requestQuery != null)
            {
                if (requestQuery.First(x => x.Key == "vnp_OrderInfo").Value == ItemType.RENT_COURT)
                {
                    var status = requestQuery.First(x => x.Key == "vnp_ResponseCode").Value;
                    var user = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)!);
                    Booking unPaidBooking = new Booking();
                    if (user != null)
                    {
                        unPaidBooking = (await _bookingService.GetUnPaidBookingByUserId(user.UserId))!;
                        if (!string.IsNullOrEmpty(status) && status == "00")
                        {
                            if (unPaidBooking != null)
                            {
                                await _paymentService.HandlePaymentResponse(new PaymentInfoDTO()
                                {
                                    BookingId = unPaidBooking.BookingId,
                                    UserId = user!.UserId,
                                    CreatedDate = DateTime.Now,
                                    Description = requestQuery.First(x => x.Key == "vnp_OrderInfo").Value!,
                                    Price = double.Parse(requestQuery.First(x => x.Key == "vnp_Amount").Value!),
                                    Status = TransactionStatus.Paid,
                                    PaymentMethod = requestQuery.First(x => x.Key == "vnp_CardType").Value! == "ATM" ? PaymentMethodConst.VNPAY : ""
                                });
                            }

                            Message = PaymentMessage.Success;
                            Success = true;
                        }
                        else
                        {
                            if (unPaidBooking != null)
                            {
                                unPaidBooking!.Status = BookingStatus.Cancel;
                                await _bookingService.UpdateBooking(unPaidBooking);
                            }

                            Message = PaymentMessage.Cancel;
                        }
                    }
                }
                else
                {
                    var status = requestQuery.First(x => x.Key == "vnp_ResponseCode").Value;
                    var user = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)!);

                    if (user != null && !string.IsNullOrEmpty(status) && status == "00")
                    {
                        var description = requestQuery.First(x => x.Key == "vnp_OrderInfo").Value!.ToString().Split(":");
                        var packageId = int.Parse(description[1]);

                        await _paymentService.HandlePaymentResponse(new PaymentInfoDTO()
                        {
                            PackageId = packageId,
                            UserId = user!.UserId,
                            CreatedDate = DateTime.Now,
                            Description = description[0],
                            Price = double.Parse(requestQuery.First(x => x.Key == "vnp_Amount").Value!),
                            Status = TransactionStatus.Paid,
                            PaymentMethod = requestQuery.First(x => x.Key == "vnp_CardType").Value! == "ATM" ? PaymentMethodConst.VNPAY : ""
                        });

                        Message = PaymentMessage.Success;
                        Success = true;
                    } else
                    {
                        Message = PaymentMessage.Cancel;
                    }
                }
            }
            else
            {
                Message = PaymentMessage.Error;
            }

            return Page();
        }
    }
}
