using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCenter.Presentation.Pages.Manager.SlotTime
{
    public class DeleteSlotTimeModel : PageModel
    {
        private readonly ITimeSlotService _timeSlotService;
        public DeleteSlotTimeModel(ITimeSlotService timeSlotService)
        {
            _timeSlotService = timeSlotService;
        }

        [BindProperty]
        public TimeSlot TimeSlotInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var timeslotinformation = await _timeSlotService.GetTimeSlotById(id);

            if (timeslotinformation == null)
            {
                return NotFound();
            }
            else
            {
                TimeSlotInformation = timeslotinformation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var timeslotinformation = await _timeSlotService.GetTimeSlotById(id);

            if (timeslotinformation != null)
            {
                TimeSlotInformation = timeslotinformation;
                await _timeSlotService.DeleteTimeSlotAsync(timeslotinformation);
                return RedirectToPage("/Manager/ViewSlotTime");
            }
            return NotFound();
        }
    }
}
