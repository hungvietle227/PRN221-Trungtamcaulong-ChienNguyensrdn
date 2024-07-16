using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Auth;
using BadmintonCenter.Common.DTO.User;

namespace BadmintonCenter.Service.Interface
{
    public interface IAuthService
    {
        Task<User?> Login(string username, string password);
        Task Register(RegisterRequestDTO request);
        Task CreateManager(CreateUserDTO request);
    }
}
