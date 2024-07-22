using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadmintonCenter.BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace BadmintonCenter.DataAcess.DAO
{
    public interface IUserDAO
    {
        Task<User?> GetUserByIdAsync(int userId);
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<User?> GetUserByUserName(string username);
        Task<List<User>> GetUserByName(string name);
    }
    public class UserDAO : IUserDAO
    {
        private readonly BadmintonDbContext _context;

        public UserDAO(BadmintonDbContext context)
        {
            _context = context;
        }


        public async Task<User?> GetUserByIdAsync(int userId)
        {
            //
            return await _context.Users.FirstOrDefaultAsync(b => b.UserId == userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            //
            return await _context.Users.ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            //
            var addedUser = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            //
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            //
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByUserName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<List<User>> GetUserByName(string name)
        {
            List<User> courts = await GetAllUsersAsync();
            return courts.Where(a => a.FullName.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
