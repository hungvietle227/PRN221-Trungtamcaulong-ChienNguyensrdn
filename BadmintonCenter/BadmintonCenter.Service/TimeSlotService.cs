using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.DataAcess.Repository;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Service.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BadmintonCenter.Service
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _timeSlotRepository;


        public TimeSlotService(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task AddTimeSlotAsync(TimeSlot timeSlot)
        {
            await _timeSlotRepository.AddTimeSlotAsync(timeSlot);
        }

        public async Task DeleteTimeSlotAsync(TimeSlot timeSlot)
        {
            await _timeSlotRepository.DeleteTimeSlotAsync(timeSlot);
        }

        public async Task<IEnumerable<TimeSlot>> GetAllTimeSlots()
        {
            return await _timeSlotRepository.GetAllTimeSlotAsync();
        }

        public async Task<IEnumerable<SlotTimeDTO>> GetAllTimeSlotsDTO()
        {
            // get all slot time
            var allSlotTime = await _timeSlotRepository.GetAllTimeSlotAsync();

            // get available slot time of court
            var availableTime = allSlotTime.Select(x => new SlotTimeDTO
                                                        {
                                                            Id = x.SlotTimeId,
                                                            StartTime = x.StartTime,
                                                            EndTime = x.EndTime,
                                                            Price = x.Price,
                                                        }).ToList();

            return availableTime;
        }

        public async Task<TimeSlot?> GetTimeSlotById(int id)
        {
            return await _timeSlotRepository.GetTimeSlotByIdAsync(id);
        }

        public async Task UpdateTimeSlotAsync(TimeSlot timeSlot)
        {
            await _timeSlotRepository.UpdateTimeSlotAsync(timeSlot);
        }
    }
}
