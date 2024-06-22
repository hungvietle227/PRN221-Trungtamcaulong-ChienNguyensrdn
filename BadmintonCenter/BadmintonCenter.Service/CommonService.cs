using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.DataAcess.Repository.Interface;
using DemoSchedule.Services.Interfaces;

namespace DemoSchedule.Services
{
    public class CommonService : ICommonService
    {
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly ITimeSlotRepository _slotTimeRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserPackageRepository _userPackageRepository;

        public CommonService(IBookingDetailRepository courtTimeBookingRepository,  
                             ITimeSlotRepository slotTimeRepository, 
                             IBookingRepository bookingRepository,
                             IUserPackageRepository userPackageRepository)
        {
            _bookingDetailRepository = courtTimeBookingRepository;
            _slotTimeRepository = slotTimeRepository;
            _bookingRepository = bookingRepository;
            _userPackageRepository = userPackageRepository;
        }

        /// <summary>
        /// Get Available Slot Time Of A Court
        /// </summary>
        /// <param name="courtId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<List<SlotTimeDTO>> GetAvailableTimeOfCourt(int courtId, DateTime date)
        {
            // get all court time booking
            var allCourtTimeBooking = await _bookingDetailRepository.GetAllBookingDetailsAsync();

            // get all slot time
            var allSlotTime = await _slotTimeRepository.GetAllTimeSlotAsync();

            // get all booking
            var allBooking = await _bookingRepository.GetAllBookingsAsync();

            // get booking in selected date
            var bookingInDate = allBooking.Where(p => p.ValidDate.Date == date.Date && p.Status != BookingStatus.Cancel).Select(x => x.BookingId).ToList();

            // get slot time of court booking in selected date
            var timeOfCourtBooking = allCourtTimeBooking.Where(p => p.CourtId == courtId && bookingInDate.Contains(p.BookingId)).Select(x => x.TimeSlotId).ToList();

            // get current hour
            DateTime currentTime = DateTime.Now;

            // get available slot time of court
            var availableTime = allSlotTime.Where(p => !timeOfCourtBooking.Contains(p.SlotTimeId)
                                                        && (date > currentTime
                                                        || (DateTime.Parse(p.StartTime).Hour > currentTime.Hour 
                                                        || (DateTime.Parse(p.StartTime).Hour == currentTime.Hour && DateTime.Parse(p.StartTime).Minute > currentTime.Minute))))
                                                        .Select(x => new SlotTimeDTO
                                                        {
                                                            Id = x.SlotTimeId,
                                                            StartTime = x.StartTime, 
                                                            EndTime = x.EndTime,
                                                            Price = x.Price,
                                                        }).ToList();

            return availableTime;
        }

        public async Task<IEnumerable<UserPackage>> GetTimeRemainingOfUser(int userId)
        {
            var userPackages = await _userPackageRepository.GetUserPackageByUserIdAsync(userId);
            return userPackages.Where(p => p.ValidInMonth == DateTime.Now.Month).ToList();
        }
    }
}
