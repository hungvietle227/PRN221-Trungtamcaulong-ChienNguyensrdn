using Microsoft.AspNetCore.Authentication.Cookies;
using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Auth;
using BadmintonCenter.Common.Enum.User;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;

namespace BadmintonCenter.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUserRepository userRepo, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _userRepo = userRepo;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User?> Login(string username, string password)
        {
            // check if username or password is empty or null.
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // check admin account
                var user = CheckAdminAccount(username, password);
                if (user == null)
                {
                    // get user by username
                    user = await _userRepo.GetUserByUserName(username);


                    if (user != null)
                    {
                        // hash password 
                        string hashPass = HashPassword(password, user.PasswordSalt);
                        if (!hashPass.Equals(user.PasswordHash, StringComparison.CurrentCulture))
                        {
                            return null;
                        }

                    }
                }

                //if found, create new cookie auth for user
                var claims = new List<Claim>
                        {
                            new(ClaimTypes.Name, user!.FullName),
                            new(ClaimTypes.Email, user!.Email!),
                            new(ClaimTypes.Role, Enum.GetName(typeof(UserRole), user.RoleId)!)
                        };

                // claims identity
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //auth properties
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(double.Parse(_config["Cookie:ExpireTime"]!)),

                    IsPersistent = true,

                    IssuedUtc = DateTime.UtcNow,

                    RedirectUri = "/auth/login"
                };

                //register cookie auth
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return user;
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
