using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.Constant.Payment;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Common.DTO.Payment;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace BadmintonCenter.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IVnPayService _vnpPayService;
        private readonly IBookingService _bookingService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly ITransactionService _transactionService;

        public PaymentService(IVnPayService vpnPayService, 
                                IBookingService bookingService, 
                                IPaymentMethodService paymentMethodService,
                                ITransactionService transactionService)
        {
            _vnpPayService = vpnPayService;
            _bookingService = bookingService;
            _paymentMethodService = paymentMethodService;
            _transactionService = transactionService;
        }

        public string CreatePaymentRequest(string paymentMethod, BookingCreateDTO booking, HttpContext context)
        {
            if (!string.IsNullOrEmpty(paymentMethod) && booking != null)
            {
                string requestUrl = string.Empty;
                switch(paymentMethod)
                {
                    case PaymentMethodConst.VNPAY:
                        requestUrl = _vnpPayService.CreatePaymentUrl(booking, context);
                        break;
                    case PaymentMethodConst.HOURS:
                        break;
                    default:
                        break;
                }

                return requestUrl;
            }

            return string.Empty;
        }

        public async Task HandlePaymentResponse(PaymentInfoDTO info)
        {
            var paymentMethod = await _paymentMethodService.GetPaymentMethodByName(info.PaymentMethod);
            if (info.BookingId != null)
            {
                var booking = await _bookingService.GetBookingById((int)info.BookingId);
                if (booking != null)
                {
                    booking.Status = BookingStatus.Paid;
                    await _bookingService.UpdateBooking(booking);

                    var transaction = new Transaction()
                    {
                        UserId = info.UserId,
                        Status = info.Status,
                        BookingId = booking.BookingId,
                        CreatedAt = info.CreatedDate,
                        Description = info.Description,
                        PaymentMethodId = paymentMethod!.PaymentMethodId,
                        Price = info.Price / 100
                    };

                    await _transactionService.AddNewTransaction(transaction);
                }
            } else
            {
                // handle package later...
                return;
            }
        }
    }
}
