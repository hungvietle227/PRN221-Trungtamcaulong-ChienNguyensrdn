using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Common.DTO.TimeSlot;

namespace DemoSchedule.Services.Interfaces
{
    public interface ICommonService
    {
        Task<List<SlotTimeDTO>> GetAvailableTimeOfCourt(int courtId, DateTime date);
        Task<IEnumerable<UserPackage>> GetTimeRemainingOfUser(int userId);
        Task<IEnumerable<SlotTimeDTO>> GetAvailableTimeInMonth(int month, List<string> daysOfWeek);
        Task<IEnumerable<Court>> GetAvailableCourt(int month, List<string> daysOfWeek, List<int> slots);
    }
}
