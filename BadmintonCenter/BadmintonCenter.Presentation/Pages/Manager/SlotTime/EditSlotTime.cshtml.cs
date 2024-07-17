using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BadmintonCenter.Presentation.Pages.Manager.SlotTime
{
    public class EditSlotTimeModel : PageModel
    {
        private readonly ITimeSlotService _timeSlotService;
        public EditSlotTimeModel(ITimeSlotService timeSlotService)
        {
            _timeSlotService = timeSlotService;
        }
        [BindProperty]
        public TimeSlot TimeSlot { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var timeslotInformation = await _timeSlotService.GetTimeSlotById(id);
            if (timeslotInformation == null)
            {
                return NotFound();
            }
            TimeSlot = timeslotInformation;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _timeSlotService.UpdateTimeSlotAsync(TimeSlot);
                return RedirectToPage("/Manager/ViewSlotTime");
            }
            return Page();
        }
        public IEnumerable<SelectListItem> SelectTime()
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

            return times;
        }
    }
}
