using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.User;
using BadmintonCenter.DataAcess.Repository.Interface;

namespace BadmintonCenter.Service.Interface
{
    public interface IUserService
    {
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByUserName(string email);
        Task DeleteUserAsync(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<Role> GetRoleByUserId(int userId);
        Task UpdateUserAsync(User user);
        Task<UpdateUserDTO?> GetUpdateUserById(int id);
        Task<List<User>> GetUserByName(string name);
    }
}
