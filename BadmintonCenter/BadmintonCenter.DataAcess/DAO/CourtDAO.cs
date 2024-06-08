using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadmintonCenter.BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace BadmintonCenter.DataAcess.DAO
{
    public interface ICourtDAO
    {
        Task<Court?> GetCourtByIdAsync(int courtId);
        Task<List<Court>> GetAllCourtsAsync();
        Task AddCourtAsync(Court court);
        Task UpdateCourtAsync(Court court);
        Task DeleteCourtAsync(int courtId);
    }
    public class CourtDAO : ICourtDAO
    {
        private readonly BadmintonDbContext _context;

        public CourtDAO(BadmintonDbContext context)
        {
            _context = context;
        }
        public async Task<Court?> GetCourtByIdAsync(int courtId)
        {
            //
            return await _context.Courts.FirstOrDefaultAsync(b => b.CourtId == courtId);
        }

        public async Task<List<Court>> GetAllCourtsAsync()
        {
            //
            return await _context.Courts.ToListAsync();
        }

        public async Task AddCourtAsync(Court court)
        {
            //
            var addedCourt = await _context.Courts.AddAsync(court);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourtAsync(Court court) 
        {
            //
            _context.Update(court);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourtAsync(int courtId)
        {
            //
            _context.Remove(courtId);
            await _context.SaveChangesAsync();
        }
    }
}
