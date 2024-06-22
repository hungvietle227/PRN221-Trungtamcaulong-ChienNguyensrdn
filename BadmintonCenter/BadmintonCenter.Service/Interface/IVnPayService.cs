using BadmintonCenter.Common.DTO.Booking;
using Microsoft.AspNetCore.Http;

namespace BadmintonCenter.Service.Interface
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(BookingCreateDTO booking, HttpContext context);
    }
}
