﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadmintonCenter.BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace BadmintonCenter.DataAcess.DAO
{
    public interface IBookingDAO
    {
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task<List<Booking>> GetAllBookingsAsync();
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(Booking bookingId);
    }
    public class BookingDAO : IBookingDAO
    {
        private readonly BadmintonDbContext _context;

        public BookingDAO(BadmintonDbContext context)
        {
            _context = context;
        }
        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            //
            return _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
        }
        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            //
            return await _context.Bookings.ToListAsync();
        }

        public async Task AddBookingAsync(Booking booking)
        {
            //
            var addedBooking = await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task  UpdateBookingAsync(Booking booking) 
        {
            //
            _context.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(Booking bookingId)
        {
            //
            _context.Remove(bookingId);
            await _context.SaveChangesAsync();
        }


    }
}