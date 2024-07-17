using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.Enum.Status;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BadmintonCenter.Presentation.Pages.Manager.SlotTime
{
    public class AddSlotTimeModel : PageModel
    {
        private readonly ITimeSlotService _timeSlotService;

        public AddSlotTimeModel(ITimeSlotService timeSlotService)
        {
            _timeSlotService = timeSlotService;
        }
        [BindProperty]
        public TimeSlot TimeSlot { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _timeSlotService.AddTimeSlotAsync(TimeSlot);
                return RedirectToPage("/Manager/ViewSlotTime");
            }
            return Page();
        }
        public async Task<IEnumerable<SelectListItem>> SelectTime()
        {
            
            var times = new List<SelectListItem>();

            // Tạo danh sách các giờ từ 8:00 sáng đến 11:00 đêm
            for (int hour = 8; hour <= 23; hour++)
            {
                for (int minute = 0; minute <= 30; minute += 30)
                {
                    var timeString = $"{hour.ToString().PadLeft(2, '0')}:{minute.ToString().PadLeft(2, '0')}";
                    times.Add(new SelectListItem { Text = timeString, Value = timeString });
                }
            }
            IEnumerable<TimeSlot> databaseTimes = await _timeSlotService.GetAllTimeSlots();

            //times = times.Where(time => !databaseTimes.Any(dbTime => dbTime.StartTime == time.Value)).ToList();
            times = times.Where(time =>
            {
                var timeValue = TimeSpan.Parse(time.Value);
                return !databaseTimes.Any(dbTime =>
                    timeValue >= TimeSpan.Parse(dbTime.StartTime) &&
                    timeValue < TimeSpan.Parse(dbTime.EndTime)
                );
            }).ToList();

            return times;
        }
    }
}
