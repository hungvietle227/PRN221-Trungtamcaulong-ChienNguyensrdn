using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Auth;
using BadmintonCenter.Common.Enum.User;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Service.Interface;
using System.Security.Cryptography;
using System.Text;

namespace BadmintonCenter.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;

        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User?> Login(string username, string password)
        {
            // check if username or password is empty or null.
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // get user by username
                var user = await _userRepo.GetUserByUserName(username);
                if (user != null)
                {
                    // hash password 
                    string hashPass = HashPassword(password, user.PasswordSalt);

                    // check password hash
                    if (hashPass.Equals(user.PasswordHash, StringComparison.CurrentCulture))
                    {
                        return user;
                    }
                    return null;
                }  
            }
            return null;
        }

        public async Task Register(RegisterRequestDTO request)
        {
            // generate byte arr salt
            byte[] salt;
            salt = GenerateRandomBytes(16);

            var user = new User
            {
                UserName = request.Username,
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordSalt = salt,
                PasswordHash = HashPassword(request.Password, salt),
                RoleId = (int)UserRole.Customer
            };

            await _userRepo.AddUserAsync(user);
        }

        private static string HashPassword(string password, byte[] salt)
        {
            using var hMac = new HMACSHA512(salt);
            byte[] hash = hMac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        private static byte[] GenerateRandomBytes(int length)
        {
            byte[] bytes = new byte[length];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            return bytes;
        }


    }
}
