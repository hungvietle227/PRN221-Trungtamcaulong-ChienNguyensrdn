using DemoSchedule.DTO;

namespace BadmintonCenter.Common.DTO.Booking
{
    public class BookingCreateDTO
    {
        public int? BookingId { get; set; }
        public int UserId { get; set; }
        public double TotalPrice { get; set; }
        public double TotalHours { get; set; }
        public string ItemType { get; set; } = null!;
    }
}
