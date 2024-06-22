using BadmintonCenter.Common.Constant.Payment;
using BadmintonCenter.Common.Enum.Status;

namespace BadmintonCenter.Common.DTO.Payment
{
    public class PaymentInfoDTO
    {
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TransactionStatus Status { get; set; }
        public int? BookingId { get; set; }
        public int? PackageId { get; set; }
        public int UserId { get; set; }
    }
}
