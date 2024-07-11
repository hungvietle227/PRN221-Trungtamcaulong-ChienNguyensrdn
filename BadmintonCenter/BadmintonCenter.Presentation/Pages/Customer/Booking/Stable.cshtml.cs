using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Common.DTO.Court;
using BadmintonCenter.Common.DTO.TimeSlot;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.Service.Interface;
using DemoSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BadmintonCenter.Presentation.Pages.Booking
{
    [Authorize(Roles = "Customer")]
    public class StableModel : PageModel
    {
        private readonly ICourtService _courtService;
        private readonly ICommonService _commonService;
        private readonly IBookingService _bookingService;
        private readonly ITimeSlotService _timeSlotService;
        private readonly IUserService _userService;

        public StableModel(ICourtService courtService,
                          ICommonService commonService,
                          IBookingService bookingService,
                          IUserService userService,
                          ITimeSlotService timeSlotService)
        {
            _courtService = courtService;
            _commonService = commonService;
            _bookingService = bookingService;
            _userService = userService;
            _timeSlotService = timeSlotService;
        }

        public IEnumerable<Court> Courts { get; set; } = new List<Court>();
        public IEnumerable<SlotTimeDTO> SlotTimes { get; set; } = new List<SlotTimeDTO>();
        public User Customer { get; set; } = null!;

        public async Task OnGet()
        {
            // Get user
            Customer = (await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)!))!;

            // get all courts
            Courts = await _courtService.GetAllCourts();

            // get available time at the present of first court
            SlotTimes = await _timeSlotService.GetAllTimeSlotsDTO();
        }

        public async Task<IActionResult> OnPostBookingAsync([FromBody] BookingStableDTO data)
        {
            if (data == null || data.Month < 0 || data.Month > 12 || data.DayOfWeek == null)
            {
                return new JsonResult(new
                {
                    isSuccess = false
                });
            }

            var booking = await _bookingService.AddStableBooking(data);

            if (booking != null)
            {
                SlotTimes = await _timeSlotService.GetAllTimeSlotsDTO();

                if (SlotTimes != null)
                {
                    // slot time list
                    var slotTimeData = new List<string>();

                    // convert new data to json
                    foreach (var slotTime in SlotTimes)
                    {
                        var jsonSlotTime = JsonConvert.SerializeObject(new
                        {
                            id = slotTime.Id,
                            startTime = slotTime.StartTime,
                            endTime = slotTime.EndTime,
                            price = slotTime.Price,
                        });

                        // add to list
                        slotTimeData.Add(jsonSlotTime);
                    }

                    // return status true and value if success
                    return new JsonResult(new
                    {
                        isSuccess = true,
                        data = slotTimeData,
                        id = booking.BookingId
                    });
                }
            }

            // return status true and value if fail
            return new JsonResult(new
            {
                isSuccess = false
            }); ;
        }

        public async Task<IActionResult> OnGetAvailableTime(UpdateSlotTimeDTO data)
        {
            if (data == null || data.Month < 0 || data.Month > 12 || string.IsNullOrEmpty(data.DaysOfWeek))
            {
                return new JsonResult(new
                {
                    isSuccess = false
                });
            }

            // handle selected days of week
            var daysOfWeek = JsonConvert.DeserializeObject<List<string>>(data.DaysOfWeek);

            SlotTimes = await _commonService.GetAvailableTimeInMonth(data.Month, daysOfWeek!);

            if (SlotTimes != null)
            {
                // slot time list
                var slotTimeData = new List<string>();

                // convert new data to json
                foreach (var slotTime in SlotTimes)
                {
                    var jsonSlotTime = JsonConvert.SerializeObject(new
                    {
                        id = slotTime.Id,
                        startTime = slotTime.StartTime,
                        endTime = slotTime.EndTime,
                        price = slotTime.Price,
                    });

                    // add to list
                    slotTimeData.Add(jsonSlotTime);
                }

                // return status true and value if success
                return new JsonResult(new
                {
                    isSuccess = true,
                    data = slotTimeData,
                });
            }

            // return status true and value if fail
            return new JsonResult(new
            {
                isSuccess = false
            }); ;
        }

        public async Task<IActionResult> OnGetUpdateCourts(UpdateCourtDTO data)
        {
            if (data == null || data.Month < 0 || data.Month > 12 || string.IsNullOrEmpty(data.DaysOfWeek) || string.IsNullOrEmpty(data.SlotTimes))
            {
                return new JsonResult(new
                {
                    isSuccess = false
                });
            }

            var daysOfWeek = JsonConvert.DeserializeObject<List<string>>(data.DaysOfWeek);
            var slots = JsonConvert.DeserializeObject<List<int>>(data.SlotTimes);

            Courts = await _commonService.GetAvailableCourt(data.Month, daysOfWeek!, slots!);

            if (Courts != null)
            {
                // court list
                var courtsData = new List<string>();

                // convert new data to json
                foreach (var court in Courts)
                {
                    var jsonCourt = JsonConvert.SerializeObject(new
                    {
                        id = court.CourtId,
                        name = court.CourtName
                    });

                    // add to list
                    courtsData.Add(jsonCourt);
                }

                // return status true and value if success
                return new JsonResult(new
                {
                    isSuccess = true,
                    data = courtsData,
                });
            }

            // return status true and value if fail
            return new JsonResult(new
            {
                isSuccess = false
            }); ;
        }
    }
}
