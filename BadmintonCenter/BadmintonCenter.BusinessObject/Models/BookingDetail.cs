namespace BadmintonCenter.BusinessObject.Models
{
    public class BookingDetail
    {
        public int BookingId { get; set; }
        public int CourtId { get; set; }
        public int TimeSlotId { get; set; }
        public Booking Booking { get; set; } = null!;
        public Court Court { get; set; } = null!;
        public TimeSlot TimeSlot { get; set; } = null!;
    }
}
