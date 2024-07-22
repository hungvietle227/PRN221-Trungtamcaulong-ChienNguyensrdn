using BadmintonCenter.BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.DAO
{
    public interface IBookingDetailDAO
    {
        Task<IEnumerable<BookingDetail>> GetAllBookingDetailsAsync();
        Task AddBookingDetailAsync(BookingDetail bookingDetail);
        Task UpdateBookingDetailAsync(BookingDetail bookingDetail);
        Task DeleteBookingDetailAsync(BookingDetail bookingDetailId);
    }
    public class BookingDetailDAO : IBookingDetailDAO
    {
        private readonly BadmintonDbContext _context;

        public BookingDetailDAO(BadmintonDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<BookingDetail>> GetAllBookingDetailsAsync()
        {
            return await _context.BookingDetails
                .Include(p => p.Booking)
                    .ThenInclude(b => b.User)
                .Include(p => p.Court)
                .Include(p => p.TimeSlot)
                .ToListAsync();
        }

        public async Task AddBookingDetailAsync(BookingDetail bookingDetail)
        {
            var addedBookingDetail = await _context.BookingDetails.AddAsync(bookingDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookingDetailAsync(BookingDetail bookingDetail)
        {
            _context.Update(bookingDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingDetailAsync(BookingDetail bookingDetailId)
        {
            _context.Remove(bookingDetailId);
            await _context.SaveChangesAsync();
        }
    }
}
