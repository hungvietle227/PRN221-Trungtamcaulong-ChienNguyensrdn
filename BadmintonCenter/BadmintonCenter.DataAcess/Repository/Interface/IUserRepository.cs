﻿using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.DataAcess.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<User?> GetUserByUserName(string username);
        Task<User?> GetUserByEmail(string email);
        Task<List<User>> GetUserByName(string name);
    }
}
