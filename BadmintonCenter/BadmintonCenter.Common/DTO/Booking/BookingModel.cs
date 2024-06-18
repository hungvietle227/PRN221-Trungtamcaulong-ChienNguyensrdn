namespace DemoSchedule.DTO
{
    public class BookingModel
    {
        public List<BookingDetailDTO> Details { get; set; } = new List<BookingDetailDTO>();
        public double Price { get; set; }
        public DateTime ValidDate { get; set; }
        public int UserId { get; set; }
    }
}
