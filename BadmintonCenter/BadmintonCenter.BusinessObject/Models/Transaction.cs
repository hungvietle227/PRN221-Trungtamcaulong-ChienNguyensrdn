using System.Transactions;

namespace BadmintonCenter.BusinessObject.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public TransactionStatus Status { get; set; }
        public int UserId { get; set; }
        public int PackageId { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = null!;
        public Package Package { get; set; } = null!;
        public User User { get; set; } = null!;
        public Booking Booking { get; set; } = null!;
    }
}
