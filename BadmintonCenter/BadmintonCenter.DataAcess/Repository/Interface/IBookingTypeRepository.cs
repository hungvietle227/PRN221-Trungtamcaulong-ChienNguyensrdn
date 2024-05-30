using BadmintonCenter.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.Repository.Interface
{
    public interface IBookingTypeRepository
    {
        Task<BookingType> GetBookingTypeByIdAsync(int bookingTypeId);
        Task<List<BookingType>> GetAllBookingTypesAsync();
        Task AddBookingTypeAsync(BookingType bookingType);
        Task UpdateBookingTypeAsync(BookingType bookingType);
        Task DeleteBookingTypeAsync(int bookingTypeId);
    }
}
