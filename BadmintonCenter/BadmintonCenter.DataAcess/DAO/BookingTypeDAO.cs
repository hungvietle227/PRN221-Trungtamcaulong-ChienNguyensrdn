using BadmintonCenter.BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.DAO
{
    public interface IBookingTypeDAO
    {
        Task<BookingType> GetBookingTypeByIdAsync(int bookingTypeId);
        Task<List<BookingType>> GetAllBookingTypesAsync();
        Task AddBookingTypeAsync(BookingType bookingType);
        Task UpdateBookingTypeAsync(BookingType bookingType);
        Task DeleteBookingTypeAsync(int bookingTypeId);
    }

    public class BookingTypeDAO : IBookingTypeDAO
    {
        private readonly BadmintonDbContext _context;
        
        public BookingTypeDAO(BadmintonDbContext context) 
        {
            _context = context;
        }

        public async Task<BookingType> GetBookingTypeByIdAsync(int bookingTypeId)
        {
            return await _context.BookingTypes.FirstOrDefaultAsync(bt => bt.BookingTypeId == bookingTypeId);
        }

        public async Task<List<BookingType>> GetAllBookingTypesAsync()
        {
            return await _context.BookingTypes.ToListAsync();
        }

        public async Task AddBookingTypeAsync(BookingType bookingType)
        {
            await _context.BookingTypes.AddAsync(bookingType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookingTypeAsync(BookingType bookingType)
        {
            _context.Entry(bookingType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingTypeAsync(int bookingTypeId)
        {
            var bookingType = await _context.BookingTypes.FirstOrDefaultAsync(r => r.BookingTypeId == bookingTypeId);
            if (bookingType != null)
            {
                _context.BookingTypes.Remove(bookingType);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Booking Type is not found!");
            }
        }
    }


}
