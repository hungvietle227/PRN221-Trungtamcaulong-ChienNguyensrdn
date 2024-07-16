using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.Constant.Message;
using BadmintonCenter.Common.Constant.Payment;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Common.DTO.Payment;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.Service.Interface;
using DemoSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BadmintonCenter.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IVnPayService _vnpPayService;
        private readonly IBookingService _bookingService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IPackageService _packageService;
        private readonly ICommonService _commonService;
        private readonly ITransactionService _transactionService;

        public PaymentService(IVnPayService vpnPayService, 
                                IBookingService bookingService, 
                                IPaymentMethodService paymentMethodService,
                                ITransactionService transactionService,
                                IPackageService packageService,
                                ICommonService commonService)
        {
            _vnpPayService = vpnPayService;
            _bookingService = bookingService;
            _paymentMethodService = paymentMethodService;
            _transactionService = transactionService;
            _packageService = packageService;
            _commonService = commonService;
        }

        public async Task<string> CreatePaymentRequest(string paymentMethod, BookingCreateDTO booking, HttpContext context)
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
                        var updatedBooking = await _bookingService.GetBookingById(booking.BookingId);

                        if (updatedBooking != null)
                        {
                            var packages = await _commonService.GetTimeRemainingOfUser(booking.UserId);
                            if (packages.Sum(p => p.HourRemaining) == 0)
                            {
                                return PaymentMessage.Error;
                            } else
                            {
                                double hours = booking.TotalHours;
                                foreach (var package in packages)
                                {
                                    if (hours == 0) break;
                                    if (package.HourRemaining == 0) continue;
                                    if (package.HourRemaining >= hours)
                                    {
                                        package.HourRemaining = package.HourRemaining - hours;
                                        hours = 0;
                                    }
                                    else
                                    {
                                        hours = hours - package.HourRemaining;
                                        package.HourRemaining = 0;
                                    }
                                    await _packageService.UpdateUserPackage(package);
                                }

                                updatedBooking.Status = BookingStatus.Paid;
                                await _bookingService.UpdateBooking(updatedBooking);

                                return PaymentMessage.Success;
                            }
                        }
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
