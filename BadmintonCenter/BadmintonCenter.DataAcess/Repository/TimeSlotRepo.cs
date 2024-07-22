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
    public class TimeSlotRepo : ITimeSlotRepository
    {
        private readonly ITimeSlotDAO _timeSlotDAO;

        public TimeSlotRepo(ITimeSlotDAO timeSlotDAO)
        {
            _timeSlotDAO = timeSlotDAO;
        }

        public async Task<TimeSlot> GetTimeSlotByIdAsync(int SlotTimeId)
        {
            return await _timeSlotDAO.GetTimeSlotByIdAsync(SlotTimeId);
        }

        public async Task<List<TimeSlot>> GetAllTimeSlotAsync()
        {
            return await _timeSlotDAO.GetAllTimeSlotAsync();
        }

        public async Task AddTimeSlotAsync(TimeSlot timeSlot)
        {
            await _timeSlotDAO.AddTimeSlotAsync(timeSlot);
        }

        public async Task UpdateTimeSlotAsync(TimeSlot timeSlot)
        {
            await _timeSlotDAO.UpdateTimeSlotAsync(timeSlot);
        }

        public async Task DeleteTimeSlotAsync(TimeSlot timeSlotId)
        {
            await _timeSlotDAO.DeleteTimeSlotAsync(timeSlotId);
        }
    }
}
