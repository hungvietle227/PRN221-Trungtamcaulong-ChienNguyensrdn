using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.DAO;
using BadmintonCenter.DataAcess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.Repository
{
    public class BookingTypeRepository : IBookingTypeRepository
    {
        private readonly IBookingTypeDAO _bookingTypeDao;

        public BookingTypeRepository(IBookingTypeDAO bookingTypeDao)
        {
            _bookingTypeDao = bookingTypeDao;
        }

        public async Task<BookingType> GetBookingTypeByIdAsync(int bookingTypeId)
        {
            return await _bookingTypeDao.GetBookingTypeByIdAsync(bookingTypeId);
        }

        public async Task<List<BookingType>> GetAllBookingTypesAsync()
        {
            return await _bookingTypeDao.GetAllBookingTypesAsync();
        }

        public async Task AddBookingTypeAsync(BookingType bookingType)
        {
            await _bookingTypeDao.AddBookingTypeAsync(bookingType);
        }

        public async Task UpdateBookingTypeAsync(BookingType bookingType)
        {
            await _bookingTypeDao.UpdateBookingTypeAsync(bookingType);
        }

        public async Task DeleteBookingTypeAsync(int bookingTypeId)
        {
            await _bookingTypeDao.DeleteBookingTypeAsync(bookingTypeId);
        }
    }
}
