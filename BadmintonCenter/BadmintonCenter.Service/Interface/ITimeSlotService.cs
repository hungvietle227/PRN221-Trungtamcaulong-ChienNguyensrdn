using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.Service.Interface
{
    public interface ITimeSlotService
    {
        Task<IEnumerable<TimeSlot>> GetAllTimeSlots();
        Task<TimeSlot?> GetTimeSlotById(int id);
    }
}
