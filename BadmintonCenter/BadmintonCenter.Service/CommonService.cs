using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Common.DTO.TimeSlot;
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
        private readonly ICourtRepository _courtRepository;
        private readonly IUserPackageRepository _userPackageRepository;

        public CommonService(IBookingDetailRepository courtTimeBookingRepository,  
                             ITimeSlotRepository slotTimeRepository, 
                             IBookingRepository bookingRepository,
                             ICourtRepository courtRepository,
                             IUserPackageRepository userPackageRepository)
        {
            _bookingDetailRepository = courtTimeBookingRepository;
            _slotTimeRepository = slotTimeRepository;
            _bookingRepository = bookingRepository;
            _userPackageRepository = userPackageRepository;
            _courtRepository = courtRepository;
        }

        public async Task<IEnumerable<Court>> GetAvailableCourt(int month, List<string> daysOfWeek, List<int> slots)
        {
            // get all booking
            var bookings = await _bookingRepository.GetAllBookingsAsync();

            // get all slot time
            var allSlotTime = await _slotTimeRepository.GetAllTimeSlotAsync();

            // all court
            var allCourts = await _courtRepository.GetAllCourtsAsync();

            if (bookings != null && bookings.Any())
            {
                // get bookings in month of selected day of week
                var bookingsInMonth = bookings.Where(p => p.ValidDate.Month == month && p.ExpiredDate.Month == month && p.ValidDate > DateTime.Now && p.Status != BookingStatus.Cancel 
                                                            && ((p.DateOfWeek == null || !p.DateOfWeek.Any()) && daysOfWeek.Contains(p.ValidDate.DayOfWeek.ToString())
                                                            || (p.DateOfWeek != null && p.DateOfWeek.Any() && daysOfWeek.Contains(p.DateOfWeek))));

                if (bookingsInMonth.Any())
                {
                    // get all court time booking
                    var allCourtTimeBooking = await _bookingDetailRepository.GetAllBookingDetailsAsync();

                    // if booking in month exists
                    var courtTimeBookings = allCourtTimeBooking.Where(p => bookingsInMonth.Select(p => p.BookingId).Contains(p.BookingId));

                    var selectedSlotsInBooking = courtTimeBookings.Where(p => slots.Contains(p.TimeSlotId)).Select(p => new
                    {
                        p.TimeSlotId,
                        p.CourtId
                    }).GroupBy(p => p.TimeSlotId).ToList();

                    if (!selectedSlotsInBooking.Any())
                    {
                        return allCourts;
                    } else
                    {
                        List<List<int>> availableCourtsOfTime = new List<List<int>>();

                        foreach (var slot in selectedSlotsInBooking)
                        {
                            var availableCourt = allCourts.Select(p => p.CourtId).Where(p => !slot.Select(x => x.CourtId).Contains(p)).ToList();
                            availableCourtsOfTime.Add(availableCourt);
                        }

                        var commonAvailable = GetCommonAvailableCourt(availableCourtsOfTime);

                        return allCourts.Where(p => commonAvailable.Contains(p.CourtId)).ToList();
                    }
                }
            }

            return allCourts;
        }

        public async Task<IEnumerable<SlotTimeDTO>> GetAvailableTimeInMonth(int month, List<string> daysOfWeek)
        {
            // get all booking
            var bookings = await _bookingRepository.GetAllBookingsAsync();

            // get all slot time
            var allSlotTime = await _slotTimeRepository.GetAllTimeSlotAsync();

            if (!daysOfWeek.Any())
            {
                // if no booking exists
                return allSlotTime.Select(x => new SlotTimeDTO
                {
                    Id = x.SlotTimeId,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Price = x.Price,
                }).ToList();
            }

            if (bookings != null && bookings.Any())
            {
                // get bookings in month of selected day of week
                var bookingsInMonth = bookings.Where(p => p.ValidDate.Month == month && p.ExpiredDate.Month == month && p.ValidDate > DateTime.Now && p.Status != BookingStatus.Cancel
                                                            && ((p.DateOfWeek == null || !p.DateOfWeek.Any()) && daysOfWeek.Contains(p.ValidDate.DayOfWeek.ToString())
                                                            || (p.DateOfWeek != null && p.DateOfWeek.Any() && daysOfWeek.Contains(p.DateOfWeek))));

                if (bookingsInMonth.Any())
                {
                    // get all court time booking
                    var allCourtTimeBooking = await _bookingDetailRepository.GetAllBookingDetailsAsync();

                    // all court
                    var allCourts = await _courtRepository.GetAllCourtsAsync();

                    // if booking in month exists
                    var courtTimeBookings = allCourtTimeBooking.Where(p => bookingsInMonth.Select(p => p.BookingId).Contains(p.BookingId));

                    var timeByBookings = courtTimeBookings.GroupBy(p => p.TimeSlotId).ToList();

                    var slotList = new List<int>();

                    foreach (var time in timeByBookings)
                    {
                        // check if time is busy for all courts
                        var isUnable = time.Count() == allCourts.Count;

                        if (isUnable)
                        {
                            // add to list
                            slotList.Add(time.Key);
                        }
                    }

                    return allSlotTime.Where(p => !slotList.Contains(p.SlotTimeId)).Select(x => new SlotTimeDTO
                    {
                        Id = x.SlotTimeId,
                        StartTime = x.StartTime,
                        EndTime = x.EndTime,
                        Price = x.Price,
                    }).ToList();

                }
            }

            return allSlotTime.Select(x => new SlotTimeDTO
            {
                Id = x.SlotTimeId,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Price = x.Price,
            }).ToList();
        }

        public async Task<List<SlotTimeDTO>> GetAvailableTimeOfCourt(int courtId, DateTime date)
        {
            // get all court time booking
            var allCourtTimeBooking = await _bookingDetailRepository.GetAllBookingDetailsAsync();

            // get all slot time
            var allSlotTime = await _slotTimeRepository.GetAllTimeSlotAsync();

            // get all booking
            var allBooking = await _bookingRepository.GetAllBookingsAsync();

            // get booking in selected date
            var bookingInDate = allBooking.Where(p => p.ValidDate.Month == date.Month && (p.ValidDate.Date == date.Date
                                                    || (p.DateOfWeek != null && p.DateOfWeek.Contains(date.DayOfWeek.ToString()))) && p.Status != BookingStatus.Cancel)
                                                    .Select(x => x.BookingId).ToList();

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
        
        private IEnumerable<int> GetCommonAvailableCourt(List<List<int>> availableCourt)
        {
            if (availableCourt.Count == 0)
                return Enumerable.Empty<int>();

            var commonElements = new HashSet<int>(availableCourt[0]);

            for (int i = 1; i < availableCourt.Count; i++)
            {
                commonElements.IntersectWith(availableCourt[i]);
            }

            return commonElements;
        }
    }
}
