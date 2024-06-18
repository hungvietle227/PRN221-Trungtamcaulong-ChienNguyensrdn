using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.Service.Interface
{
    public interface IUserService
    {
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByEmail(string email);
    }
}
