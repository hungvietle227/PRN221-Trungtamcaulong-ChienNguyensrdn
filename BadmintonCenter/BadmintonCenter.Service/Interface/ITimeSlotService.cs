using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Booking;

namespace BadmintonCenter.Service.Interface
{
    public interface ITimeSlotService
    {
        Task<IEnumerable<SlotTimeDTO>> GetAllTimeSlotsDTO();
        Task<IEnumerable<TimeSlot>> GetAllTimeSlots();
        Task<TimeSlot?> GetTimeSlotById(int id);
        Task AddTimeSlotAsync(TimeSlot timeSlot);
        Task UpdateTimeSlotAsync(TimeSlot timeSlot);
        Task DeleteTimeSlotAsync(TimeSlot timeSlot);
    }
}
