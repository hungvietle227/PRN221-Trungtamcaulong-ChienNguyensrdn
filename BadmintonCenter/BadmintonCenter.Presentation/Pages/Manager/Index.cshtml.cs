using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace BadmintonCenter.Presentation.Pages.Manager
{
    public class IndexModel : PageModel
    {
        private readonly ICourtService _courtService;

        public IndexModel(ICourtService courtService)
        {
            _courtService = courtService;
        }

        public IEnumerable<Court> AllCourt { get; set; }
        public IEnumerable<Court> Courts { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5; // Number of items per page

        [BindProperty]
        public string SearchValue { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!SearchValue.IsNullOrEmpty())
            {

            }
            AllCourt = await _courtService.GetAllCourts();
            CurrentPage = id ?? 1;
            // Paginate the list of rooms
            Courts = PaginatedList<Court>.Create(AllCourt.ToList(), CurrentPage, PageSize);

            return Page();
        }
    }

    public static class PaginatedList<T>
    {
        public static List<T> Create(List<T> source, int currentPage, int pageSize)
        {
            return source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
