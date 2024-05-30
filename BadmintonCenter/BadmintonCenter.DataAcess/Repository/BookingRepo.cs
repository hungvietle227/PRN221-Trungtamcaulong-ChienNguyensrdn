using BadmintonCenter.DataAcess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadmintonCenter.DataAcess.DAO;
using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.DataAcess.Repository
{
    internal class BookingRepo : IBookingRepository
    {
        private readonly IBookingDAO _bookingDAO;

        public BookingRepo(IBookingDAO bookingDAO)
        {
            _bookingDAO = bookingDAO;
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
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
