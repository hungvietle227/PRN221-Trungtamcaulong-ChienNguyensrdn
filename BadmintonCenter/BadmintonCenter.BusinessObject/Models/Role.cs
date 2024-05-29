using BadmintonCenter.Common.Enum.User;

namespace BadmintonCenter.BusinessObject.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public UserRole RoleName { get; set; }
        public ICollection<User> Users { get; set; } = null!;
    }
}
