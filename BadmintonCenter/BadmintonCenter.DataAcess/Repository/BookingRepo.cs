﻿using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.DataAcess.DAO;
using BadmintonCenter.DataAcess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            return await _bookingDAO.AddBookingAsync(booking);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            await _bookingDAO.UpdateBookingAsync(booking);
        }

        public async Task DeleteBookingAsync(Booking bookingId)
        {
            await _bookingDAO.DeleteBookingAsync(bookingId);
        }

        public async Task<Booking?> GetUnPaidBookingByUserId(int userId)
        {
            var bookings = await _bookingDAO.GetAllBookingsAsync();
            return bookings.FirstOrDefault(bookings => bookings.UserId == userId && bookings.Status == BookingStatus.Wait);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingOfUser(int userId)
        {
            var bookings = await _bookingDAO.GetAllBookingsAsync();

            return bookings.AsQueryable().Include(p => p.User).Include(p => p.BookingType).Where(p => p.UserId == userId).ToList();
        }
    }
}
