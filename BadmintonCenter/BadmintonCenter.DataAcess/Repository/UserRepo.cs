using BadmintonCenter.DataAcess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadmintonCenter.DataAcess.DAO;
using BadmintonCenter.BusinessObject.Models;

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

        public async Task DeleteUserAsync(int userId)
        {
            await _userDAO.DeleteUserAsync(userId);
        }



    }
}
