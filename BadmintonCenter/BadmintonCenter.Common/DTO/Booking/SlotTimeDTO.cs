namespace BadmintonCenter.Common.DTO.Booking
{
    public class SlotTimeDTO
    {
        public int Id { get; set; }
        public string StartTime { get; set; } = null!;
        public string EndTime { get; set; } = null!;
        public double Price { get; set; }
    }
}
