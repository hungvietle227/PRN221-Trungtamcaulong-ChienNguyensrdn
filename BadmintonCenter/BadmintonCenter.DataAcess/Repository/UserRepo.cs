using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.DAO;
using BadmintonCenter.DataAcess.Repository.Interface;

namespace BadmintonCenter.DataAcess.Repository
{
    public class UserRepo : IUserRepository
    {
        private readonly IUserDAO _userDAO;

        public UserRepo(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userDAO.GetUserByIdAsync(userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userDAO.GetAllUsersAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _userDAO.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userDAO.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _userDAO.DeleteUserAsync(user);
        }

        public async Task<User?> GetUserByUserName(string username)
        {
            return await _userDAO.GetUserByUserName(username);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return (await _userDAO.GetAllUsersAsync()).FirstOrDefault(u => u.Email == email);
        }
    }
}
