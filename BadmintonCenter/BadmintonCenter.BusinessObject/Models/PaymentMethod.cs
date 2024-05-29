namespace BadmintonCenter.BusinessObject.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string MethodName { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = null!;
    }
}
