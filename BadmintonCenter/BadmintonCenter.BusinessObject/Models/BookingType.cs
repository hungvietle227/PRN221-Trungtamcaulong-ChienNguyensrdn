using BadmintonCenter.Common.Enum.Booking;

namespace BadmintonCenter.BusinessObject.Models
{
    public class BookingType
    {
        public int BookingTypeId { get; set; }
        public BookingByType Type { get; set; }
        public ICollection<Booking> Bookings { get; set; } = null!;
    }
}
