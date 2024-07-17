namespace BadmintonCenter.BusinessObject.Models
{
    public class TimeSlot
    {
        public int SlotTimeId { get; set; }
        public string StartTime { get; set; } = null!;
        public string EndTime { get; set; } = null!;
        public double Price { get; set; }
        public double Time { get; set; }
        public ICollection<BookingDetail>? BookingDetails { get; set; }
    }
}
