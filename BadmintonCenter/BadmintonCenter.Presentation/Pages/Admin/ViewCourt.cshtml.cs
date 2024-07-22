using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Buffers;

namespace BadmintonCenter.Presentation.Pages.Admin
{
    public class ViewCourtModel : PageModel
    {
        private readonly ICourtService _courtService;

        public ViewCourtModel(ICourtService courtService)
        {
            _courtService = courtService;
        }

        public IEnumerable<Court> AllCourt { get; set; }
        public IEnumerable<Court> Courts { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5; // Number of items per page

        [BindProperty(SupportsGet = true)]
        public string? SearchValue { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string? searchValue)
        {
            SearchValue = searchValue;
            if (!SearchValue.IsNullOrEmpty())
            {
                AllCourt = await _courtService.GetCourtByName(SearchValue);
                CurrentPage = id ?? 1;
                // Paginate the list of rooms
                Courts = PaginatedList<Court>.Create(AllCourt.ToList(), CurrentPage, PageSize);
                return Page();
            }
            AllCourt = await _courtService.GetAllCourts();
            CurrentPage = id ?? 1;
            // Paginate the list of rooms
            Courts = PaginatedList<Court>.Create(AllCourt.ToList(), CurrentPage, PageSize);

            return Page();
        }
        public static class PaginatedList<T>
        {
            public static List<T> Create(List<T> source, int currentPage, int pageSize)
            {
                return source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}
