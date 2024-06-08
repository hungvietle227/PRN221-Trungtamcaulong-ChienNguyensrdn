using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.DataAcess.Repository.Interface
{
    public interface IBookingRepository
    {
        Task<Booking?> GetBookingByIdAsync(int bookingId);
        Task<List<Booking>> GetAllBookingsAsync();
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(Booking booking);

    }
}
