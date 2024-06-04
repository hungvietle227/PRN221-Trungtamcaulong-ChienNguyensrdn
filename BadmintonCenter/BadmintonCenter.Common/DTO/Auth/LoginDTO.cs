using System.ComponentModel.DataAnnotations;

namespace BadmintonCenter.Common.DTO.Auth
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; } = null!;
    }
}
