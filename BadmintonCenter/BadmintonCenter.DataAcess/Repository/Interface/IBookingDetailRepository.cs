using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.DataAcess.Repository.Interface
{
    public interface IBookingDetailRepository
    {
        Task<BookingDetail> GetBookingDetailByIdAsync(int bookingDetailId);
        Task<IEnumerable<BookingDetail>> GetAllBookingDetailsAsync();
        Task AddBookingDetailAsync(BookingDetail bookingDetail);
        Task UpdateBookingDetailAsync(BookingDetail bookingDetail);
        Task DeleteBookingDetailAsync(BookingDetail bookingDetail);
        Task<IEnumerable<BookingDetail>> GetBookingDetailsByBookingId(int id);
    }
}
