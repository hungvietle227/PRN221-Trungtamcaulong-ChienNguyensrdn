using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadmintonCenter.Presentation.Pages.Manager
{
    public class ViewSlotTimeModel : PageModel
    {
        private readonly ITimeSlotService _timeSlotService;

        public ViewSlotTimeModel(ITimeSlotService timeSlotService)
        {
            _timeSlotService = timeSlotService;
        }

        public IEnumerable<TimeSlot> AllTimeSlot { get; set; }
        public IEnumerable<TimeSlot> TimeSlots { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5; // Number of items per page

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            AllTimeSlot = await _timeSlotService.GetAllTimeSlots();
            CurrentPage = id ?? 1;
            // Paginate the list of rooms
            TimeSlots = PaginatedList<TimeSlot>.Create(AllTimeSlot.ToList(), CurrentPage, PageSize);

            return Page();
        }
    }
}
