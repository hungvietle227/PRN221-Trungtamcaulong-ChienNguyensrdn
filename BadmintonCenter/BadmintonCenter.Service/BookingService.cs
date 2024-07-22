using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Common.Enum.Booking;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Service.Interface;
using DemoSchedule.DTO;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BadmintonCenter.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public BookingService(IBookingRepository bookingRepository, IBookingDetailRepository bookingDetailRepository, ITimeSlotRepository timeSlotRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingDetailRepository = bookingDetailRepository;
            _timeSlotRepository = timeSlotRepository;
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
            // total days in month
            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, bookingData.Month);
            List<DateTime> availableDays = new List<DateTime>();

            for (int count = (DateTime.Today.Day + 1); count <= daysInMonth; count++)
            {
                DateTime dateTime = new DateTime(DateTime.Now.Year, bookingData.Month, count);
                if (bookingData.DayOfWeek.Contains(dateTime.DayOfWeek.ToString()))
                {
                    availableDays.Add(dateTime);
                }
            }

            if (availableDays.Any())
            {
                // take price of slot
                double slotsPrice = 0;
                for (int i = 0; i < bookingData.Slots.Count; i++)
                {
                    var slot = await _timeSlotRepository.GetTimeSlotByIdAsync(bookingData.Slots[i]);

                    slotsPrice += slot.Price;
                }

                // compute total
                double totalPrice = availableDays.Count * bookingData.Courts.Count * slotsPrice;
                double totalHours = availableDays.Count * bookingData.Courts.Count * bookingData.Slots.Count * 0.5;

                var booking = await _bookingRepository.AddBookingAsync(new Booking()
                {
                    BookingDate = DateTime.Now,
                    ExpiredDate = availableDays.Last(),
                    DateOfWeek = string.Join(",", bookingData.DayOfWeek),
                    TotalPrice = totalPrice,
                    ValidDate = availableDays.First(),
                    Status = BookingStatus.Wait,
                    UserId = bookingData.UserId,
                    TotalHour = totalHours,
                    BookingTypeId = (int)BookingByType.Stable,
                });

                if (booking != null )
                {
                    foreach (var court in bookingData.Courts)
                    {
                        foreach (var slot in bookingData.Slots)
                        {
                            await _bookingDetailRepository.AddBookingDetailAsync(new BookingDetail()
                            {
                                BookingId = booking.BookingId,
                                CourtId = court,
                                TimeSlotId = slot
                            });
                        }
                    }

                    return booking;
                }

                return null;

            }

            return null;
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


        //Check In Customer
        public async Task<IEnumerable<BookingDetail>> GetAllBookingDetails()
        {
            return await _bookingDetailRepository.GetAllBookingDetailsAsync();
        }
    }
}
