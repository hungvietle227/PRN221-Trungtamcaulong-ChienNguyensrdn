using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Service.Interface;
using DemoSchedule.DTO;

namespace BadmintonCenter.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingDetailRepository _bookingDetailRepository;

        public BookingService(IBookingRepository bookingRepository, IBookingDetailRepository bookingDetailRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingDetailRepository = bookingDetailRepository;
        }

        public async Task<Booking?> AddNewBooking(Booking booking, List<BookingDetailDTO> bookingDetails)
        {
            var newBooking = await _bookingRepository.AddBookingAsync(booking);

            if (newBooking != null && bookingDetails.Any())
            {
                foreach(var item in bookingDetails)
                {
                    await _bookingDetailRepository.AddBookingDetailAsync(new BookingDetail()
                    {
                        BookingId = newBooking.BookingId,
                        CourtId = item.CourtId,
                        TimeSlotId = item.SlotTimeId
                    });
                }
            }

            return newBooking;
        }

        public Task<IEnumerable<Booking>> GetAllBookings()
        {
            throw new NotImplementedException();
        }

        public Task<Booking?> GetBookingById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> UpdateBooking(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
