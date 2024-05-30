using BadmintonCenter.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.Repository.Interface
{
    public interface ITimeSlotRepository
    {
        Task<TimeSlot> GetTimeSlotByIdAsync(int SlotTimeId);
        Task<List<TimeSlot>> GetAllTimeSlotAsync();
        Task AddTimeSlotAsync(TimeSlot timeSlot);
        Task UpdateTimeSlotAsync(TimeSlot timeSlot);
        Task DeleteTimeSlotAsync(TimeSlot timeSlot);
    }
}
