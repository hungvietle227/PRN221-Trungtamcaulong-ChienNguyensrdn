using BadmintonCenter.Common.Enum.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.Common.DTO.User
{
    public class CreateUserDTO
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
        public UserRole Role { get; set; }
    }
}
