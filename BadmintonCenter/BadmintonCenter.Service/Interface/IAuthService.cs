using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Auth;

namespace BadmintonCenter.Service.Interface
{
    public interface IAuthService
    {
        Task<User?> Login(string username, string password);
        Task Register(RegisterRequestDTO request);
    }
}
