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

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            return await _bookingRepository.GetAllBookingsAsync();
        }

        public async Task<Booking?> GetBookingById(int id)
        {
            return await _bookingRepository.GetBookingByIdAsync(id);
        }

        public async Task<IEnumerable<BookingDetail>> GetBookingDetailsByBookingId(int id)
        {
            return await _bookingDetailRepository.GetBookingDetailsByBookingId(id);
        }

        public async Task<Booking?> GetUnPaidBookingByUserId(int userId)
        {
            return await _bookingRepository.GetUnPaidBookingByUserId(userId);
        }

        public async Task UpdateBooking(Booking booking)
        {
            await _bookingRepository.UpdateBookingAsync(booking);
        }
    }
}
