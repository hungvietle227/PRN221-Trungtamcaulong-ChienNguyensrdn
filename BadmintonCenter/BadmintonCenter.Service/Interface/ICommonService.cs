﻿using BadmintonCenter.Common.DTO.Booking;

namespace DemoSchedule.Services.Interfaces
{
    public interface ICommonService
    {
        Task<List<SlotTimeDTO>> GetAvailableTimeOfCourt(int courtId, DateTime date);
    }
}