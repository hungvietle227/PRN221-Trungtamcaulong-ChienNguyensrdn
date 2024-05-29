using BadmintonCenter.Common.Enum.Status;

namespace BadmintonCenter.BusinessObject.Models
{
    public class Court
    {
        public int CourtId { get; set; }
        public string CourtName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public CourtStatus Status { get; set; }
        public ICollection<BookingDetail> BookingDetails { get; set; } = null!;
    }
}
