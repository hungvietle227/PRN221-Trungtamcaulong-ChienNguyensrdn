using BadmintonCenter.Common.DTO.Booking;

namespace BadmintonCenter.Common.DTO.Payment
{
    public class PaymentResponseDTO
    {
        public string TransactionInfo { get; set; }
        public string TransactionId { get; set; }
        public string PaymentMethod { get; set; }
        public string Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public BookingCreateDTO BookingCreate { get; set; }
    }
}
