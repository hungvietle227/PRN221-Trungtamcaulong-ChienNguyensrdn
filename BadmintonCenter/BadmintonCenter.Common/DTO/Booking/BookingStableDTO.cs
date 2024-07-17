namespace BadmintonCenter.Common.DTO.Booking
{
    public class BookingStableDTO
    {
        public List<int> Courts { get; set; }
        public List<int> Slots { get; set; }
        public List<string> DayOfWeek { get; set; }
        public int Month { get; set; }
        public int UserId { get; set; }
    }
}
