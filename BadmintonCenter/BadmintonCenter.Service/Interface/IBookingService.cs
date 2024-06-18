﻿using BadmintonCenter.BusinessObject.Models;
using DemoSchedule.DTO;

namespace BadmintonCenter.Service.Interface
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookings();
        Task<Booking?> AddNewBooking(Booking booking, List<BookingDetailDTO> bookingDetails);
        Task<Booking?> GetBookingById(int id);
        Task<Booking> UpdateBooking(Booking booking);
    }
}
