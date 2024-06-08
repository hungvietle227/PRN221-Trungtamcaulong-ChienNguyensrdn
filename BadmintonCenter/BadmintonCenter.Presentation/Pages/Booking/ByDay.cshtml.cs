using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Service.Interface;
using DemoSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BadmintonCenter.Presentation.Pages.Booking
{
    public class ByDayModel : PageModel
    {
        private readonly ICourtService _courtService;
        private readonly ICommonService _commonService;

        public ByDayModel(ICourtService courtService,
                          ICommonService commonService)
        {
            _courtService = courtService;
            _commonService = commonService;
        }

        public IEnumerable<Court> Courts { get; set; } = new List<Court>();
        public IEnumerable<SlotTimeDTO> SlotTimes { get; set; } = new List<SlotTimeDTO>();

        public async Task OnGetAsync()
        {
            // get all courts
            Courts = await _courtService.GetAllCourts();

            // get available time at the present of first court
            SlotTimes = await _commonService.GetAvailableTimeOfCourt(1, DateTime.Now);
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
    }
}
