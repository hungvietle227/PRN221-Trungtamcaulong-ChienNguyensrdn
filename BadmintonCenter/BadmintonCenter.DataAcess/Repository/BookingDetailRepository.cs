using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.DAO;
using BadmintonCenter.DataAcess.Repository.Interface;

namespace BadmintonCenter.DataAcess.Repository
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        private readonly IBookingDetailDAO _bookingDetailDAO;

        public BookingDetailRepository(IBookingDetailDAO bookingDetailDAO)
        {
            _bookingDetailDAO = bookingDetailDAO;
        }

        public async Task AddBookingDetailAsync(BookingDetail bookingDetail)
        {
            await _bookingDetailDAO.AddBookingDetailAsync(bookingDetail);
        }

        public async Task DeleteBookingDetailAsync(BookingDetail bookingDetail)
        {
            await _bookingDetailDAO.DeleteBookingDetailAsync(bookingDetail);
        }

        public async Task<IEnumerable<BookingDetail>> GetAllBookingDetailsAsync()
        {
            return await _bookingDetailDAO.GetAllBookingDetailsAsync();
        }

        public async Task<BookingDetail> GetBookingDetailByIdAsync(int bookingDetailId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookingDetail>> GetBookingDetailsByBookingId(int id)
        {
            var bookingDetails = await _bookingDetailDAO.GetAllBookingDetailsAsync();
            return bookingDetails.Where(p => p.BookingId == id).AsEnumerable();
        }

        public async Task UpdateBookingDetailAsync(BookingDetail bookingDetail)
        {
            await _bookingDetailDAO.UpdateBookingDetailAsync(bookingDetail);
        }
    }
}
