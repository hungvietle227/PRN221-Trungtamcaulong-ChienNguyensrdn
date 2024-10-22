using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Service;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace BadmintonCenter.Presentation.Pages.Admin
{
    public class ViewUserModel : PageModel
    {
        private readonly IUserService _userService;

        public ViewUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<User> AllUsers { get; set; }
        public IEnumerable<User> Users { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5; // Number of items per page
        [BindProperty(SupportsGet = true)]
        public string? SearchValue { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string? searchValue)
        {
            SearchValue = searchValue;

            if (searchValue != null)
            {

                AllUsers = await _userService.GetUserByName(SearchValue);
                foreach (var user in AllUsers)
                {
                    user.Role = await _userService.GetRoleByUserId(user.UserId);
                }
                CurrentPage = id ?? 1;
                // Paginate the list of rooms
                Users = PaginatedList<User>.Create(AllUsers.ToList(), CurrentPage, PageSize);
                return Page();

            }
            AllUsers = await _userService.GetAllUsers();
            foreach (var user in AllUsers)
            {
                user.Role = await _userService.GetRoleByUserId(user.UserId);
            }
            CurrentPage = id ?? 1;
            // Paginate the list of rooms
            Users = PaginatedList<User>.Create(AllUsers.ToList(), CurrentPage, PageSize);

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
