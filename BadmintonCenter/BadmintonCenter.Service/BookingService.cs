using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Service.Interface;
using DemoSchedule.DTO;
using Microsoft.EntityFrameworkCore.Storage.Json;

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

        public async Task<Booking?> AddStableBooking(BookingStableDTO bookingData)
        {
            return new Booking();
            //// current date
            //DateTime today = DateTime.Now;

            //// get remaining day in month of specific dayOfWeek
            //int daysInMonth = DateTime.DaysInMonth(today.Year, bookingData.Month);
            //List<DateTime> remainingDays = new List<DateTime>();
            //for (int i = today.Day; i <= daysInMonth; i++)
            //{
            //    DateTime day = new DateTime(today.Year, bookingData.Month, i);
            //    if (bookingData.DayOfWeek.Contains(day.DayOfWeek.ToString()))
            //    {
            //        remainingDays.Add(day);
            //    }
            //}

            //// get booking in specific month
            //var bookings = await _bookingRepository.GetAllBookingsAsync();
            //var bookingInMonth = bookings.Where(p => p.ValidDate.Month == bookingData.Month
            //                                         && p.ExpiredDate.Month == bookingData.Month
            //                                         && (!bookingData.DayOfWeek.Contains(p.DateOfWeek)));

            //// get detail of booking
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
