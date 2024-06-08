using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.DataAcess.Repository.Interface;
using DemoSchedule.Services.Interfaces;

namespace DemoSchedule.Services
{
    public class CommonService : ICommonService
    {
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly ITimeSlotRepository _slotTimeRepository;
        private readonly IBookingRepository _bookingRepository;

        public CommonService(IBookingDetailRepository courtTimeBookingRepository,  
                             ITimeSlotRepository slotTimeRepository, 
                             IBookingRepository bookingRepository)
        {
            _bookingDetailRepository = courtTimeBookingRepository;
            _slotTimeRepository = slotTimeRepository;
            _bookingRepository = bookingRepository;

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
            var bookingInDate = allBooking.Where(p => p.BookingDate.Date == date.Date).Select(x => x.BookingId).ToList();

            // get slot time of court booking in selected date
            var timeOfCourtBooking = allCourtTimeBooking.Where(p => p.CourtId == courtId && bookingInDate.Contains(p.BookingId)).Select(x => x.TimeSlotId).ToList();

            // get current hour
            DateTime currentTime = DateTime.Now;

            // get available slot time of court
            var availableTime = allSlotTime.Where(p => !timeOfCourtBooking.Contains(p.SlotTimeId) && DateTime.Parse(p.StartTime).Hour >= currentTime.Hour)
                                                .Select(x => new SlotTimeDTO
                                                {
                                                    Id = x.SlotTimeId,
                                                    StartTime = x.StartTime, 
                                                    EndTime = x.EndTime,
                                                    Price = x.Price,
                                                }).ToList();

            return availableTime;
        }
    }
}
