using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.DAO;
using BadmintonCenter.DataAcess.Repository.Interface;

namespace BadmintonCenter.DataAcess.Repository
{
    public class BookingRepo : IBookingRepository
    {
        private readonly IBookingDAO _bookingDAO;

        public BookingRepo(IBookingDAO bookingDAO)
        {
            _bookingDAO = bookingDAO;
        }

        public async Task<Booking?> GetBookingByIdAsync(int bookingId)
        {
            return await _bookingDAO.GetBookingByIdAsync(bookingId);
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _bookingDAO.GetAllBookingsAsync();
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await _bookingDAO.AddBookingAsync(booking);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            await _bookingDAO.UpdateBookingAsync(booking);
        }

        public async Task DeleteBookingAsync(Booking bookingId)
        {
            await _bookingDAO.DeleteBookingAsync(bookingId);
        }
    }
}
