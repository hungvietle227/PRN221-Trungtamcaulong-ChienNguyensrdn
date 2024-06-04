using BadmintonCenter.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.Repository.Interface
{
    public interface IBookingDetailDetailRepository
    {
        Task<BookingDetail> GetBookingDetailByIdAsync(int bookingDetailId);
        Task<List<BookingDetail>> GetAllBookingDetailsAsync();
        Task AddBookingDetailAsync(BookingDetail bookingDetail);
        Task UpdateBookingDetailAsync(BookingDetail bookingDetail);
        Task DeleteBookingDetailAsync(BookingDetail bookingDetail);
    }
}
