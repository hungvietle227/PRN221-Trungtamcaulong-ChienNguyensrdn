using Azure.Core;
using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.User;
using BadmintonCenter.Common.Enum.User;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Service.Interface;
using Microsoft.Extensions.Configuration;

namespace BadmintonCenter.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _config = configuration;
            _roleRepository = roleRepository;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _userRepository.DeleteUserAsync(user);
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<Role> GetRoleByUserId(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return (await _roleRepository.GetRoleByIdAsync(user.RoleId));
        }

        public async Task UpdateUserAsync(User user)
        {
            var thisuser = await GetUserByEmail(user.Email);

            user.PasswordHash = thisuser.PasswordHash;
            user.PasswordSalt = thisuser.PasswordSalt;
            user.Role = thisuser.Role;
            user.Transactions = thisuser.Transactions;
            user.UserPackages = thisuser.UserPackages;
            user.Bookings = thisuser.Bookings;
            user.Email = thisuser.Email;
            user.FullName = thisuser.FullName;
            user.PhoneNumber = thisuser.PhoneNumber;
            user.UserName = thisuser.UserName;
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task<UpdateUserDTO?> GetUpdateUserById(int id)
        {
            var userUp = await _userRepository.GetUserByIdAsync(id);
            var user = new UpdateUserDTO
            {
                UserName = userUp.UserName,
                FullName = userUp.FullName,
                Email = userUp.Email,
                PhoneNumber = userUp.PhoneNumber,
            };

            return user;
        }

        public async Task<List<User>> GetUserByName(string name)
        {
            return await _userRepository.GetUserByName(name);   
        }

        public async Task<User?> GetUserByUserName(string email)
        {
            return await _userRepository.GetUserByUserName(email);
        }

        private User? CheckAdminAccount(string username, string password)
        {
            var adminUsername = _config["AdminAccount:username"];
            var adminPass = _config["AdminAccount:password"];

            if (!string.IsNullOrEmpty(adminUsername) && !string.IsNullOrEmpty(adminPass))
            {
                if (adminUsername == username && adminPass == password)
                {
                    return new User
                    {
                        FullName = "Admin",
                        Email = "Admin@badminton.com",
                        RoleId = (int)UserRole.Admin
                    };
                }
            }

            return null;
        }
    }
}
