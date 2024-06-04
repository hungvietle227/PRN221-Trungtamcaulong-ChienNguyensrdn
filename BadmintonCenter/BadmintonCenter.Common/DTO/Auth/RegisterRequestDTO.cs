using System.ComponentModel.DataAnnotations;

namespace BadmintonCenter.Common.DTO.Auth
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Full name is required")]
        [MaxLength(30, ErrorMessage = "Full name cannot exceed 30 characters")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "Username cannot exceed 20 characters")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Confirm password does not match the password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
