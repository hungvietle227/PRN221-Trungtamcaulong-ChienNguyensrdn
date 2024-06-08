using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Service.Interface;

namespace BadmintonCenter.Service
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _timeSlotRepository;


        public TimeSlotService(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<IEnumerable<TimeSlot>> GetAllTimeSlots()
        {
            return await _timeSlotRepository.GetAllTimeSlotAsync();
        }

        public async Task<TimeSlot?> GetTimeSlotById(int id)
        {
            return await _timeSlotRepository.GetTimeSlotByIdAsync(id);
        }
    }
}
