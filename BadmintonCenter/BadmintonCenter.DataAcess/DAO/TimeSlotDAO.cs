using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadmintonCenter.BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace BadmintonCenter.DataAcess.DAO
{
    public interface ITimeSlotDAO
    {
        Task<TimeSlot?> GetTimeSlotByIdAsync(int SlotTimeId);
        Task<List<TimeSlot>> GetAllTimeSlotAsync();
        Task AddTimeSlotAsync(TimeSlot timeSlot);
        Task UpdateTimeSlotAsync(TimeSlot timeSlotId);
        Task DeleteTimeSlotAsync(TimeSlot timeSlotId);
    }
    public class TimeSlotDAO : ITimeSlotDAO
    {
        private BadmintonDbContext _context;

        public TimeSlotDAO(BadmintonDbContext context)
        {
            _context = context;
        }
        public async Task<TimeSlot?> GetTimeSlotByIdAsync(int SlotTimeId)
        {
            //
            return await _context.TimeSlots.FirstOrDefaultAsync(b => b.SlotTimeId == SlotTimeId);
        }

        public async Task<List<TimeSlot>> GetAllTimeSlotAsync()
        {
            return await _context.TimeSlots.ToListAsync();
        }

        public async Task AddTimeSlotAsync(TimeSlot timeSlot) 
        {
            //
            var addedTimeSlot = await _context.TimeSlots.AddAsync(timeSlot);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTimeSlotAsync(TimeSlot timeSlot)
        {
            //
            _context.Update(timeSlot);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTimeSlotAsync(TimeSlot timeSlotId) 
        {
            //
            _context.Remove(timeSlotId);
            await _context.SaveChangesAsync();
        }
    }
}
