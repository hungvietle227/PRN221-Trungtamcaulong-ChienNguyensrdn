using BadmintonCenter.Common.Enum.Status;

namespace BadmintonCenter.BusinessObject.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ValidDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string? DateOfWeek { get; set; }
        public BookingStatus Status { get; set; }
        public double TotalPrice { get; set; }
        public double TotalHour { get; set; }
        public int BookingTypeId { get; set; }
        public int UserId { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; } = null!;
        public BookingType BookingType { get; set; } = null!;
        public User User { get; set; } = null!;
        public ICollection<BookingDetail> BookingDetails { get; set; } = null!;
    }
}
