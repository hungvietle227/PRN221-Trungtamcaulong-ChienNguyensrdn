using DemoSchedule.DTO;

namespace BadmintonCenter.Common.DTO.Booking
{
    public class BookingCreateDTO
    {
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public double TotalPrice { get; set; }
        public List<BookingDetailDTO>? Details { get; set; }
        public string ItemType { get; set; } = null!;
    }
}
