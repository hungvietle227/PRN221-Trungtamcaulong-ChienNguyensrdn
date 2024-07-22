using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Booking;
using DemoSchedule.DTO;

namespace BadmintonCenter.Service.Interface
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookings();
        Task<Booking?> AddNewBooking(Booking booking, List<BookingDetailDTO> bookingDetails);
        Task<Booking?> AddStableBooking(BookingStableDTO bookingData);
        Task<Booking?> GetBookingById(int id);
        Task UpdateBooking(Booking booking);
        Task<IEnumerable<BookingDetail>> GetBookingDetailsByBookingId(int id);
        Task<Booking?> GetUnPaidBookingByUserId(int userId);

        Task<IEnumerable<BookingDetail>> GetAllBookingDetails();
        Task<IEnumerable<Booking>> GetAllBookingOfUser(int userId);
    }
}
