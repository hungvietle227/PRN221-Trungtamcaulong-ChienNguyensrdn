using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Common.Enum.Booking;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.Service;
using BadmintonCenter.Service.Interface;
using DemoSchedule.DTO;
using DemoSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BadmintonCenter.Presentation.Pages.Booking
{
    public class ByDayModel : PageModel
    {
        private readonly ICourtService _courtService;
        private readonly ICommonService _commonService;
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;

        public ByDayModel(ICourtService courtService,
                          ICommonService commonService,
                          IBookingService bookingService,
                          IUserService userService)
        {
            _courtService = courtService;
            _commonService = commonService;
            _bookingService = bookingService;
            _userService = userService;
        }

        public IEnumerable<Court> Courts { get; set; } = new List<Court>();
        public IEnumerable<SlotTimeDTO> SlotTimes { get; set; } = new List<SlotTimeDTO>();
        public User? Customer { get; set; } = null!;


        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToPage("/Auth/Login");
            }

            // Get user
            Customer = await _userService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)!);

            // get all courts
            Courts = await _courtService.GetAllCourts();

            // get available time at the present of first court
            SlotTimes = await _commonService.GetAvailableTimeOfCourt(1, DateTime.Now);

            return Page();
        }

        public async Task<IActionResult> OnGetUpdateSlotTime(int courtId, DateTime date)
        {
            // update available slot time of new court
            SlotTimes = await _commonService.GetAvailableTimeOfCourt(courtId, date);

            if (SlotTimes != null && SlotTimes.Count() > 0)
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
                    data = slotTimeData
                });
            }

            // return status false if slot time is empty
            return new JsonResult(new
            {
                isSuccess = false
            });
        }

        public async Task<IActionResult> OnPostBookingAsync([FromBody] BookingModel data)
        {
            BadmintonCenter.BusinessObject.Models.Booking newBooking = new BadmintonCenter.BusinessObject.Models.Booking()
            {
                BookingDate = DateTime.Now,
                ExpiredDate = data.ValidDate,
                TotalPrice = data.Price,
                ValidDate = data.ValidDate,
                Status = BookingStatus.Wait,
                UserId = 3,
                TotalHour = data.Details.Count() / 2,
                BookingTypeId = (int)BookingByType.NormalByDate,
            };

            // add booking
            await _bookingService.AddNewBooking(newBooking, data.Details);

            // update available slot time of first court
            SlotTimes = await _commonService.GetAvailableTimeOfCourt(1, DateTime.Now);

            if (SlotTimes != null && SlotTimes.Count() > 0)
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
                    data = slotTimeData
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
