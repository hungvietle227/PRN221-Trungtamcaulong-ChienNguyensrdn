using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Common.DTO.Payment;
using Microsoft.AspNetCore.Http;

namespace BadmintonCenter.Service.Interface
{
    public interface IPaymentService
    {
        string CreatePaymentRequest(string paymentMethod ,BookingCreateDTO booking, HttpContext context);
        Task HandlePaymentResponse(PaymentInfoDTO info);
    }
}
